using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Systems
{
    /*  
        Handles infinite vertical scrolling of the level background.

        This system draws two stacked copies of the background texture and
        moves them downward over time to create a seamless "flying through
        space" effect. Once the scroll offset exceeds the screen height,
        it wraps back to zero to maintain continuous motion.
    */
    public class BackgroundScroller
    {
        private readonly AssetLoader assets;
        private Texture2D background;

        // Current vertical scroll offset
        private float scrollY = 0f;

        // Screen dimensions for scaling and wrap logic
        private int screenWidth;
        private int screenHeight;

        // Creates a new background scroller for the given screen size.
        public BackgroundScroller(AssetLoader assets, int screenWidth, int screenHeight)
        {
            this.assets = assets;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        // Loads the background texture used for the scrolling effect.
        public void LoadContent()
        {
            background = assets.LoadTexture("assets/level/level_1/background.png");
        }

        /*  
            Updates the scroll offset to move the background downward.

            Wraps back to zero when the texture has fully scrolled off-screen.
        */
        public void Update()
        {
            scrollY += 0.2f;

            if (scrollY >= screenHeight)
                scrollY = 0f;
        }

        /*  
            Draws two copies of the background texture to create a seamless
            infinite scrolling effect. One is drawn at the current offset,
            and the second is drawn directly above it.
        */
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

        // Unloads background content.
        public void UnloadContent() { }
    }
}
