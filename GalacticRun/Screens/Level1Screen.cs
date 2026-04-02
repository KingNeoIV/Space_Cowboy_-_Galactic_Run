using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;
using GalacticRun.Systems;
using GalacticRun.Entities;

namespace GalacticRun.Screens
{
    public class Level1Screen : IScreen
    {
        private Player player = null!;                    // will be set in LoadContent
        private Texture2D idleFrame = new Texture2D();    // struct default, overwritten in LoadContent
        private Texture2D[] moveFrames = System.Array.Empty<Texture2D>();

        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        private BackgroundScroller? background;
        private PlanetSystem? planets;
        private ParallaxSystem? parallax;

        public Level1Screen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        public void Initialize()
        {
            // Nothing needed here yet
        }

        public void LoadContent()
        {
            var assets = services.Get<AssetLoader>();

            // ------------------------------
            // Load Level Systems
            // ------------------------------
            background = new BackgroundScroller(assets, screenWidth, screenHeight);
            planets    = new PlanetSystem(assets, screenWidth, screenHeight);
            parallax   = new ParallaxSystem(background, planets);

            background.LoadContent();
            planets.LoadContent();
            parallax.LoadContent();

            // ------------------------------
            // Load Player Assets
            // ------------------------------
            idleFrame = assets.LoadTexture("assets/player/Idle.png");

            moveFrames = new Texture2D[]
            {
                assets.LoadTexture("assets/player/Move1.png"),
                assets.LoadTexture("assets/player/Move2.png"),
                assets.LoadTexture("assets/player/Move3.png"),
                assets.LoadTexture("assets/player/Move4.png"),
                assets.LoadTexture("assets/player/Move5.png"),
                assets.LoadTexture("assets/player/Move6.png")
            };

            // ------------------------------
            // Create Player
            // ------------------------------
            player = new Player(
                idleFrame,
                moveFrames,
                new Vector2(
                    screenWidth / 2 - idleFrame.Width / 2,
                    screenHeight - idleFrame.Height - 50
                ),
                screenWidth,
                screenHeight
            );

        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                var pause = new PauseMenuScreen(screenWidth, screenHeight, services);
                services.Get<ScreenManager>().PushScreen(pause);
                return;
            }

            float dt = Raylib.GetFrameTime();

            parallax!.Update();
            player.Update(dt);
        }

        public void Draw()
        {
            parallax!.Draw();
            player.Draw();
        }

        public void UnloadContent()
        {
            // AssetLoader handles unloading
        }
    }
}
