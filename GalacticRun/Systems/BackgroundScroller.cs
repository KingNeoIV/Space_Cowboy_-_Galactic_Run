using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Systems
{
    /// <summary>
    /// Handles infinite vertical scrolling of the level background.
    ///
    /// This system draws two stacked copies of the background texture and
    /// moves them downward over time to create a seamless "flying through
    /// space" effect. Once the scroll offset exceeds the screen height,
    /// it wraps back to zero to maintain continuous motion.
    /// </summary>
    public class BackgroundScroller
    {
        private readonly AssetLoader assets;
        private Texture2D background;

        // Current vertical scroll offset
        private float scrollY = 0f;

        // Screen dimensions for scaling and wrap logic
        private int screenWidth;
        private int screenHeight;

        /// <summary>
        /// Creates a new background scroller for the given screen size.
        /// </summary>
        public BackgroundScroller(AssetLoader assets, int screenWidth, int screenHeight)
        {
            this.assets = assets;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        /// <summary>
        /// Loads the background texture used for the scrolling effect.
        /// </summary>
        public void LoadContent()
        {
            background = assets.LoadTexture("assets/level/level_1/background.png");
        }

        /// <summary>
        /// Updates the scroll offset to move the background downward.
        /// Wraps back to zero when the texture has fully scrolled off-screen.
        /// </summary>
        public void Update()
        {
            scrollY += 0.2f;

            if (scrollY >= screenHeight)
                scrollY = 0f;
        }

        /// <summary>
        /// Draws two copies of the background texture to create a seamless
        /// infinite scrolling effect. One is drawn at the current offset,
        /// and the second is drawn directly above it.
        /// </summary>
        public void Draw()
        {
            Rectangle src = new Rectangle(0, 0, background.Width, background.Height);

            Rectangle dest1 = new Rectangle(
                0,
                scrollY,
                screenWidth,
                screenHeight
            );

            Rectangle dest2 = new Rectangle(
                0,
                scrollY - screenHeight,
                screenWidth,
                screenHeight
            );

            Raylib.DrawTexturePro(background, src, dest1, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTexturePro(background, src, dest2, Vector2.Zero, 0f, Color.White);
        }

        /// <summary>
        /// Unloads background content. Most cleanup is handled by AssetLoader.
        /// </summary>
        public void UnloadContent() { }
    }
}
