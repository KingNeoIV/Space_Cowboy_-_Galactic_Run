using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Systems
{
    /*  
        Handles parallax planet rendering for Level 1.

        This system manages three planet layers positioned at different
        depths and scrolling speeds to create a sense of depth and motion.
        Each planet loops vertically once it scrolls past the bottom of
        the screen, maintaining a continuous parallax effect.
    */
    public class PlanetSystem
    {
        private readonly AssetLoader assets;

        // Planet textures at different depths
        private Texture2D planet;
        private Texture2D planet1;
        private Texture2D planet2;

        // Planet positions
        private float planetX, planetY;
        private float planet1X, planet1Y;
        private float planet2X, planet2Y;

        private readonly int screenWidth;
        private readonly int screenHeight;

        // Creates a new planet parallax system for the given screen size.
        public PlanetSystem(AssetLoader assets, int screenWidth, int screenHeight)
        {
            this.assets = assets;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        /*  
            Loads planet textures and initializes their starting positions
            above the visible screen area.
        */
        public void LoadContent()
        {
            planet  = assets.LoadTexture("assets/level/level_1/planet.png");
            planet1 = assets.LoadTexture("assets/level/level_1/planet_1.png");
            planet2 = assets.LoadTexture("assets/level/level_1/planet_2.png");

            // Initial positions (off-screen above)
            planetX  = screenWidth / 2 - planet.Width / 2;
            planetY  = -800;

            planet1X = screenWidth / 1.5f - planet1.Width / 2;
            planet1Y = -1200;

            planet2X = screenWidth * 0.05f;
            planet2Y = -1000;
        }

        /*  
            Updates vertical scrolling for each planet layer.

            Each planet scrolls at a different speed to simulate depth.
            When a planet moves off-screen, it resets above the screen.
        */
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

        /*  
            Draws the planet layers in depth order:
            farthest (planet2), mid (planet1), closest (planet).

            Scaling is applied to reinforce depth perception.
        */
        public void Draw()
        {
            // Farthest planet
            Raylib.DrawTexture(planet2, (int)planet2X, (int)planet2Y, Color.White);

            // Mid-depth planet (scaled larger)
            Raylib.DrawTextureEx(
                planet1,
                new Vector2(planet1X, planet1Y),
                0f,
                1.6f,
                Color.White
            );

            // Closest planet (scaled slightly)
            Raylib.DrawTextureEx(
                planet,
                new Vector2(planetX, planetY),
                0f,
                1.4f,
                Color.White
            );
        }

        // Unloads planet textures.
        public void UnloadContent() { }
    }
}
