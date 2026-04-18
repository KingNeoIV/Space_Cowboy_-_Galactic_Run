using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;
using GalacticRun.Systems;
using GalacticRun.Entities;

namespace GalacticRun.Screens
{
    /// <summary>
    /// Primary gameplay screen for Level 1.
    ///
    /// Responsible for loading level systems (background, planets,
    /// parallax), initializing the player, updating gameplay logic,
    /// and rendering all level elements. This screen represents the
    /// active gameplay state and can be paused via the PauseMenuScreen.
    /// </summary>
    public class Level1Screen : IScreen
    {
        // Player instance created during content loading.
        private Player player = null!;

        // Player animation textures (idle + movement frames).
        private Texture2D idleFrame = new Texture2D();
        private Texture2D[] moveFrames = System.Array.Empty<Texture2D>();

        // Shared services and screen dimensions.
        private readonly ServiceProvider services;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // Level systems for background scrolling and parallax effects.
        private BackgroundScroller? background;
        private PlanetSystem? planets;
        private ParallaxSystem? parallax;

        /// <summary>
        /// Creates a new Level 1 gameplay screen.
        /// </summary>
        public Level1Screen(int width, int height, ServiceProvider services)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.services = services;
        }

        /// <summary>
        /// Called once when the screen is created.
        /// Currently unused but reserved for future initialization logic.
        /// </summary>
        public void Initialize()
        {
            // Nothing needed here yet
        }

        /// <summary>
        /// Loads all assets and systems required for Level 1.
        /// Initializes background layers, parallax effects, and the player.
        /// </summary>
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

        /// <summary>
        /// Updates gameplay logic once per frame.
        /// Handles pause input, parallax motion, and player movement.
        /// </summary>
        public void Update()
        {
            // Pause the game and push the pause menu screen.
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

        /// <summary>
        /// Draws all level elements in the correct order:
        /// background → parallax layers → player.
        /// </summary>
        public void Draw()
        {
            parallax!.Draw();
            player.Draw();
        }

        /// <summary>
        /// Unloads level-specific content.
        /// Most cleanup is handled by the AssetLoader.
        /// </summary>
        public void UnloadContent()
        {
            // AssetLoader handles unloading
        }
    }
}
