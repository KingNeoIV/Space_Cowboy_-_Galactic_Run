using Raylib_cs;
using System.Numerics;

namespace GalacticRun.Entities
{
    public class Player
    {
        private Texture2D idleFrame;
        private Texture2D[] moveFrames;

        private int currentFrame = 0;
        private float frameTimer = 0f;
        private float frameSpeed = 0.005f;

        public Vector2 Position;
        private float speed = 600f;

        private int screenWidth;
        private int screenHeight;

        public Player(Texture2D idle, Texture2D[] moveFrames, Vector2 startPos, int screenWidth, int screenHeight)
        {
            this.idleFrame = idle;
            this.moveFrames = moveFrames;
            this.Position = startPos;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void Update(float dt)
        {
            Vector2 move = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.W)) move.Y -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.S)) move.Y += 1;
            if (Raylib.IsKeyDown(KeyboardKey.A)) move.X -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.D)) move.X += 1;

            bool isMoving = move.LengthSquared() > 0;

            if (isMoving)
            {
                move = Vector2.Normalize(move);
                Position += move * speed * dt;

                // Animate movement
                frameTimer += dt;
                if (frameTimer >= frameSpeed)
                {
                    frameTimer = 0f;
                    currentFrame++;
                    if (currentFrame >= moveFrames.Length)
                        currentFrame = 0;
                }
            }
            else
            {
                currentFrame = 0;
            }

            // Clamp inside screen
            Position.X = Math.Clamp(Position.X, 0, screenWidth - idleFrame.Width);
            Position.Y = Math.Clamp(Position.Y, 0, screenHeight - idleFrame.Height);
        }

        public void Draw()
        {
            Texture2D frame = (currentFrame == 0)
                ? idleFrame
                : moveFrames[currentFrame];

            Raylib.DrawTexture(frame, (int)Position.X, (int)Position.Y, Color.White);
        }
    }
}
