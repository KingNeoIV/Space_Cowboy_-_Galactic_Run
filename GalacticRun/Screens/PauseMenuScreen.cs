using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Screens
{
    /// <summary>
    /// Modal pause menu overlay.
    ///
    /// Displays a dimmed background, a centered pause window, and
    /// interactive buttons for resuming the game or exiting the
    /// application. This screen sits on top of the gameplay screen
    /// and blocks updates/draws beneath it.
    /// </summary>
    public class PauseMenuScreen : IScreen
    {
        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // UI textures
        private Texture2D windowPanel;
        private Texture2D header;
        private Texture2D playBtn;
        private Texture2D exitBtn;

        // UI element positions
        private Vector2 windowPos;
        private Vector2 headerPos;
        private Vector2 playPos;
        private Vector2 exitPos;

        // Button hitboxes
        private Rectangle playRect;
        private Rectangle exitRect;

        /// <summary>
        /// Creates a new pause menu overlay.
        /// </summary>
        public PauseMenuScreen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        /// <summary>
        /// Called once before content is loaded.
        /// Currently unused but reserved for future logic.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Loads pause menu textures and calculates UI layout positions.
        /// </summary>
        public void LoadContent()
        {
            windowPanel = Raylib.LoadTexture("assets/ui/esc_menu/Window.png");
            header     = Raylib.LoadTexture("assets/ui/esc_menu/Header.png");
            playBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Play_BTN.png");
            exitBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Exit_BTN.png");

            // Center window panel
            windowPos = new Vector2(
                screenWidth / 2 - windowPanel.Width / 2,
                screenHeight / 2 - windowPanel.Height / 2
            );

            // Header centered above buttons
            headerPos = new Vector2(
                screenWidth / 2 - header.Width / 2,
                windowPos.Y + 40
            );

            // Play button
            playPos = new Vector2(
                screenWidth / 2 - playBtn.Width / 2,
                headerPos.Y + header.Height + 120
            );

            // Exit button
            exitPos = new Vector2(
                screenWidth / 2 - exitBtn.Width / 2,
                playPos.Y + playBtn.Height + 40
            );

            // Button hitboxes
            playRect = new Rectangle(playPos.X, playPos.Y, playBtn.Width, playBtn.Height);
            exitRect = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
        }

        /// <summary>
        /// Handles button clicks and ESC key to resume or exit the game.
        /// </summary>
        public void Update()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            // Resume game
            if (Raylib.CheckCollisionPointRec(mouse, playRect) &&
                Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<ScreenManager>().PopScreen();
                return;
            }

            // Exit game
            if (Raylib.CheckCollisionPointRec(mouse, exitRect) &&
                Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<Game>().RequestExit();
                return;
            }

            // ESC also resumes
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                services.Get<ScreenManager>().PopScreen();
                return;
            }
        }

        /// <summary>
        /// Draws the dimmed overlay, pause window, header, and buttons.
        /// Hovered buttons brighten for visual feedback.
        /// </summary>
        public void Draw()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            // Dim background
            Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, new Color(0, 0, 0, 180));

            // Window + header
            Raylib.DrawTexture(windowPanel, (int)windowPos.X, (int)windowPos.Y, Color.White);
            Raylib.DrawTexture(header, (int)headerPos.X, (int)headerPos.Y, Color.White);

            // Play button
            bool hoverPlay = Raylib.CheckCollisionPointRec(mouse, playRect);
            Color playColor = hoverPlay ? Color.White : new Color(200, 200, 200, 255);
            Raylib.DrawTexture(playBtn, (int)playPos.X, (int)playPos.Y, playColor);

            // Exit button
            bool hoverExit = Raylib.CheckCollisionPointRec(mouse, exitRect);
            Color exitColor = hoverExit ? Color.White : new Color(200, 200, 200, 255);
            Raylib.DrawTexture(exitBtn, (int)exitPos.X, (int)exitPos.Y, exitColor);
        }

        /// <summary>
        /// Unloads pause menu content.
        /// Most cleanup is handled by the AssetLoader.
        /// </summary>
        public void UnloadContent()
        {
        }
    }
}
