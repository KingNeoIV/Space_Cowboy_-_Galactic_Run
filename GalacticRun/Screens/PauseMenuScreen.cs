using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Screens
{
    public class PauseMenuScreen : IScreen
    {
        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        private Texture2D windowPanel;
        private Texture2D header;
        private Texture2D playBtn;
        private Texture2D exitBtn;

        private Vector2 windowPos;
        private Vector2 headerPos;
        private Vector2 playPos;
        private Vector2 exitPos;

        private Rectangle playRect;
        private Rectangle exitRect;

        public PauseMenuScreen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            windowPanel = Raylib.LoadTexture("assets/ui/esc_menu/Window.png");
            header     = Raylib.LoadTexture("assets/ui/esc_menu/Header.png");
            playBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Play_BTN.png");
            exitBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Exit_BTN.png");

            windowPos = new Vector2(
                screenWidth / 2 - windowPanel.Width / 2,
                screenHeight / 2 - windowPanel.Height / 2
            );

            headerPos = new Vector2(
                screenWidth / 2 - header.Width / 2,
                windowPos.Y + 40
            );

            playPos = new Vector2(
                screenWidth / 2 - playBtn.Width / 2,
                headerPos.Y + header.Height + 120
            );

            exitPos = new Vector2(
                screenWidth / 2 - exitBtn.Width / 2,
                playPos.Y + playBtn.Height + 40
            );

            playRect = new Rectangle(playPos.X, playPos.Y, playBtn.Width, playBtn.Height);
            exitRect = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
        }

        public void Update()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            if (Raylib.CheckCollisionPointRec(mouse, playRect) &&
                Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<ScreenManager>().PopScreen();
                return;
            }

            if (Raylib.CheckCollisionPointRec(mouse, exitRect) &&
                Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                services.Get<Game>().RequestExit();
                return;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                services.Get<ScreenManager>().PopScreen();
                return;
            }
        }

        public void Draw()
        {
            Vector2 mouse = Raylib.GetMousePosition();

            Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, new Color(0, 0, 0, 180));

            Raylib.DrawTexture(windowPanel, (int)windowPos.X, (int)windowPos.Y, Color.White);
            Raylib.DrawTexture(header, (int)headerPos.X, (int)headerPos.Y, Color.White);

            bool hoverPlay = Raylib.CheckCollisionPointRec(mouse, playRect);
            Color playColor = hoverPlay ? Color.White : new Color(200, 200, 200, 255);
            Raylib.DrawTexture(playBtn, (int)playPos.X, (int)playPos.Y, playColor);

            bool hoverExit = Raylib.CheckCollisionPointRec(mouse, exitRect);
            Color exitColor = hoverExit ? Color.White : new Color(200, 200, 200, 255);
            Raylib.DrawTexture(exitBtn, (int)exitPos.X, (int)exitPos.Y, exitColor);
        }

        public void UnloadContent()
        {
        }
    }
}
