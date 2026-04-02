using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        private Texture2D background;
        private Texture2D title;
        private Texture2D startBtn;
        private Texture2D exitBtn;

        private Rectangle startRect;
        private Rectangle exitRect;

        private bool startHover = false;
        private bool exitHover = false;

        public MainMenuScreen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        public void Initialize()
        {
            float btnWidth = 300;
            float btnHeight = 90;

            startRect = new Rectangle(
                screenWidth / 2 - btnWidth / 2,
                screenHeight * 0.55f,
                btnWidth,
                btnHeight
            );

            exitRect = new Rectangle(
                screenWidth / 2 - btnWidth / 2,
                screenHeight * 0.70f,
                btnWidth,
                btnHeight
            );
        }

        public void LoadContent()
        {
            var assets = services.Get<AssetLoader>();

            background = assets.LoadTexture("assets/ui/main_menu/mainMenu.png");
            title      = assets.LoadTexture("assets/ui/main_menu/Game_Title.png");
            startBtn   = assets.LoadTexture("assets/ui/main_menu/Start_BTN.png");
            exitBtn    = assets.LoadTexture("assets/ui/main_menu/Exit_BTN.png");
        }

        public void Update()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            startHover = Raylib.CheckCollisionPointRec(mouse, startRect);
            exitHover  = Raylib.CheckCollisionPointRec(mouse, exitRect);

            if (startHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var level1 = new Level1Screen(screenWidth, screenHeight, services);
                services.Get<ScreenManager>().ReplaceScreen(level1);
            }

            if (exitHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<Game>().RequestExit();
            }
        }

        public void Draw()
        {
            Raylib.DrawTexture(background, 0, 0, Color.White);

            Raylib.DrawTexture(
                title,
                screenWidth / 2 - title.Width / 2,
                (int)(screenHeight * 0.15f),
                Color.White
            );

            Color startTint = startHover ? Color.Yellow : Color.White;
            Raylib.DrawTextureEx(
                startBtn,
                new Vector2(startRect.X, startRect.Y),
                0f,
                startRect.Width / startBtn.Width,
                startTint
            );

            Color exitTint = exitHover ? Color.Yellow : Color.White;
            Raylib.DrawTextureEx(
                exitBtn,
                new Vector2(exitRect.X, exitRect.Y),
                0f,
                exitRect.Width / exitBtn.Width,
                exitTint
            );
        }

        public void UnloadContent()
        {
        }
    }
}
