using Raylib_cs;

namespace GalacticRun.Core
{
    public class Game
    {
        private readonly ScreenManager screenManager = new();
        private readonly ServiceProvider services = new();
        private bool fullscreenApplied = false;

        // Exit flag
        private bool exitRequested = false;

        public void RequestExit()
        {
            exitRequested = true;
        }

        public void Run()
        {
            int screenWidth = 1920;
            int screenHeight = 1080;

            Raylib.InitWindow(screenWidth, screenHeight, "Galactic Run");
            Raylib.SetTargetFPS(144);
            Raylib.SetExitKey(KeyboardKey.Null);

            screenWidth = Raylib.GetScreenWidth();
            screenHeight = Raylib.GetScreenHeight();

            // Register services
            services.AddService(new AssetLoader());
            services.AddService(screenManager);
            services.AddService(this); // Allow screens to request exit

            // Start at Main Menu
            screenManager.PushScreen(
                new Screens.MainMenuScreen(screenWidth, screenHeight, services)
            );

            // Main loop
            while (!Raylib.WindowShouldClose() && !exitRequested)
            {
                if (!fullscreenApplied)
                {
                    Raylib.ToggleFullscreen();
                    Raylib.SetExitKey(KeyboardKey.Null); // Disable default exit key in fullscreen
                    fullscreenApplied = true;
                }

                screenManager.Update();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                screenManager.Draw();

                Raylib.EndDrawing();
            }

            // Cleanup
            services.Get<AssetLoader>().UnloadAll();
            Raylib.CloseWindow();
        }
    }
}
