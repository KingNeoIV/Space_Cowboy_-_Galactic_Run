using Raylib_cs;
using System.Numerics;
using GalacticRun.Core;

namespace GalacticRun.Systems
{
    public class BackgroundScroller
    {
        private readonly AssetLoader assets;
        private Texture2D background;

        private float scrollY = 0f;
        private int screenWidth;
        private int screenHeight;

        public BackgroundScroller(AssetLoader assets, int screenWidth, int screenHeight)
        {
            this.assets = assets;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void LoadContent()
        {
            background = assets.LoadTexture("assets/level/level_1/background.png");
        }

        public void Update()
        {
            scrollY += 0.2f;
            if (scrollY >= screenHeight)
                scrollY = 0f;
        }

        public void Draw()
        {
            Rectangle src = new Rectangle(0, 0, background.Width, background.Height);
            Rectangle dest1 = new Rectangle(0, scrollY, screenWidth, screenHeight);
            Rectangle dest2 = new Rectangle(0, scrollY - screenHeight, screenWidth, screenHeight);

            Raylib.DrawTexturePro(background, src, dest1, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTexturePro(background, src, dest2, Vector2.Zero, 0f, Color.White);
        }

        public void UnloadContent() { }
    }
}
