using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Systems
{
    public class PlanetSystem
    {
        private readonly AssetLoader assets;

        private Texture2D planet;
        private Texture2D planet1;
        private Texture2D planet2;

        private float planetX, planetY;
        private float planet1X, planet1Y;
        private float planet2X, planet2Y;

        private readonly int screenWidth;
        private readonly int screenHeight;

        public PlanetSystem(AssetLoader assets, int screenWidth, int screenHeight)
        {
            this.assets = assets;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void LoadContent()
        {
            planet  = assets.LoadTexture("assets/level/level_1/planet.png");
            planet1 = assets.LoadTexture("assets/level/level_1/planet_1.png");
            planet2 = assets.LoadTexture("assets/level/level_1/planet_2.png");

            planetX  = screenWidth / 2 - planet.Width / 2;
            planetY  = -800;

            planet1X = screenWidth / 1.5f - planet1.Width / 2;
            planet1Y = -1200;

            planet2X = screenWidth * 0.05f;
            planet2Y = -1000;
        }

        public void Update()
        {
            planetY += 0.4f;
            if (planetY > screenHeight)
                planetY = -800;

            planet1Y += 0.6f;
            if (planet1Y > screenHeight)
                planet1Y = -1200;

            planet2Y += 0.4f;
            if (planet2Y > screenHeight)
                planet2Y = -1000;
        }

        public void Draw()
        {
            Raylib.DrawTexture(planet2, (int)planet2X, (int)planet2Y, Color.White);

            Raylib.DrawTextureEx(
                planet1,
                new Vector2(planet1X, planet1Y),
                0f,
                1.6f,
                Color.White
            );

            Raylib.DrawTextureEx(
                planet,
                new Vector2(planetX, planetY),
                0f,
                1.4f,
                Color.White
            );
        }

        public void UnloadContent() { }
    }
}
