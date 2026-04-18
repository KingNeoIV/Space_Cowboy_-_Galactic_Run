using Raylib_cs;

namespace GalacticRun.Core
{
    /// <summary>
    /// Core game controller responsible for initializing the window,
    /// managing global services, handling the screen stack, and running
    /// the main update/draw loop.
    ///
    /// The Game class acts as the central entry point for engine-level
    /// behavior, while individual screens encapsulate gameplay, menus,
    /// and UI logic.
    /// </summary>
    public class Game
    {
        // Manages active screens (Main Menu, Gameplay, Pause, etc.)
        private readonly ScreenManager screenManager = new();

        // Simple dependency container for shared services.
        private readonly ServiceProvider services = new();

        // Ensures fullscreen is applied only once after window creation.
        private bool fullscreenApplied = false;

        // Flag used to request a clean shutdown from any screen.
        private bool exitRequested = false;

        /// <summary>
        /// Allows screens or systems to request a controlled exit.
        /// The main loop will terminate on the next iteration.
        /// </summary>
        public void RequestExit()
        {
            exitRequested = true;
        }

        /// <summary>
        /// Initializes the game window, registers core services,
        /// loads the initial screen, and runs the main game loop.
        /// Handles fullscreen setup, update/draw sequencing,
        /// and final cleanup on shutdown.
        /// </summary>
        public void Run()
        {
            int screenWidth = 1920;
            int screenHeight = 1080;

            // Window initialization
            Raylib.InitWindow(screenWidth, screenHeight, "Galactic Run");
            Raylib.SetTargetFPS(144);
            Raylib.SetExitKey(KeyboardKey.Null); // Disable default ESC exit

            // Retrieve actual window size after creation
            screenWidth = Raylib.GetScreenWidth();
            screenHeight = Raylib.GetScreenHeight();

            // Register global services
            services.AddService(new AssetLoader());
            services.AddService(screenManager);
            services.AddService(this); // Allows screens to call RequestExit()

            // Load the initial screen (Main Menu)
            screenManager.PushScreen(
                new Screens.MainMenuScreen(screenWidth, screenHeight, services)
            );

            // Main game loop
            while (!Raylib.WindowShouldClose() && !exitRequested)
            {
                // Apply fullscreen once after the first frame
                if (!fullscreenApplied)
                {
                    Raylib.ToggleFullscreen();
                    Raylib.SetExitKey(KeyboardKey.Null); // Re-disable ESC in fullscreen
                    fullscreenApplied = true;
                }

                // Update active screen
                screenManager.Update();

                // Render active screen
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                screenManager.Draw();

                Raylib.EndDrawing();
            }

            // Cleanup resources before exiting
            services.Get<AssetLoader>().UnloadAll();
            Raylib.CloseWindow();
        }
    }
}
