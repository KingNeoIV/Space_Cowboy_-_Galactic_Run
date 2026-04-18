using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Screens
{
    /// <summary>
    /// Main menu screen for Galactic Run.
    ///
    /// Handles loading UI assets, positioning interactive buttons,
    /// detecting hover/click input, and transitioning into gameplay
    /// or exiting the application. This screen represents the first
    /// user-facing interface of the game.
    /// </summary>
    public class MainMenuScreen : IScreen
    {
        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // UI textures
        private Texture2D background;
        private Texture2D title;
        private Texture2D startBtn;
        private Texture2D exitBtn;

        // Button hitboxes
        private Rectangle startRect;
        private Rectangle exitRect;

        // Hover states for visual feedback
        private bool startHover = false;
        private bool exitHover = false;

        /// <summary>
        /// Creates a new main menu screen with the given dimensions and services.
        /// </summary>
        public MainMenuScreen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        /// <summary>
        /// Initializes button layout and hitboxes.
        /// Called once before content is loaded.
        /// </summary>
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

        /// <summary>
        /// Loads all textures required for the main menu UI.
        /// </summary>
        public void LoadContent()
        {
            var assets = services.Get<AssetLoader>();

            background = assets.LoadTexture("assets/ui/main_menu/mainMenu.png");
            title      = assets.LoadTexture("assets/ui/main_menu/Game_Title.png");
            startBtn   = assets.LoadTexture("assets/ui/main_menu/Start_BTN.png");
            exitBtn    = assets.LoadTexture("assets/ui/main_menu/Exit_BTN.png");
        }

        /// <summary>
        /// Handles mouse hover and click interactions.
        /// Starts the game or exits the application based on user input.
        /// </summary>
        public void Update()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            startHover = Raylib.CheckCollisionPointRec(mouse, startRect);
            exitHover  = Raylib.CheckCollisionPointRec(mouse, exitRect);

            // Start game
            if (startHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var level1 = new Level1Screen(screenWidth, screenHeight, services);
                services.Get<ScreenManager>().ReplaceScreen(level1);
            }

            // Exit game
            if (exitHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<Game>().RequestExit();
            }
        }

        /// <summary>
        /// Draws the main menu background, title, and interactive buttons.
        /// Hovered buttons are tinted for visual feedback.
        /// </summary>
        public void Draw()
        {
            Raylib.DrawTexture(background, 0, 0, Color.White);

            Raylib.DrawTexture(
                title,
                screenWidth / 2 - title.Width / 2,
                (int)(screenHeight * 0.15f),
                Color.White
            );

            // Start button
            Color startTint = startHover ? Color.Yellow : Color.White;
            Raylib.DrawTextureEx(
                startBtn,
                new Vector2(startRect.X, startRect.Y),
                0f,
                startRect.Width / startBtn.Width,
                startTint
            );

            // Exit button
            Color exitTint = exitHover ? Color.Yellow : Color.White;
            Raylib.DrawTextureEx(
                exitBtn,
                new Vector2(exitRect.X, exitRect.Y),
                0f,
                exitRect.Width / exitBtn.Width,
                exitTint
            );
        }

        /// <summary>
        /// Unloads menu-specific content.
        /// Most cleanup is handled by the AssetLoader.
        /// </summary>
        public void UnloadContent()
        {
        }
    }
}
