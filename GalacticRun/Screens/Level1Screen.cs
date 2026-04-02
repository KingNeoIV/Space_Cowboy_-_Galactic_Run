using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;
using GalacticRun.Systems;

namespace GalacticRun.Screens
{
    public class Level1Screen : IScreen
    {
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

            // Create systems with required dependencies
            background = new BackgroundScroller(assets, screenWidth, screenHeight);
            planets    = new PlanetSystem(assets, screenWidth, screenHeight);
            parallax   = new ParallaxSystem(background, planets);

            // Load system content
            background.LoadContent();
            planets.LoadContent();
            parallax.LoadContent();
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                var pause = new PauseMenuScreen(screenWidth, screenHeight, services);
                services.Get<ScreenManager>().PushScreen(pause);
                return;
            }

            parallax!.Update();
        }

        public void Draw()
        {
            parallax!.Draw();
        }

        public void UnloadContent()
        {
            // Systems unload automatically through AssetLoader
        }
    }
}
