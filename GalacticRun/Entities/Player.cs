using Raylib_cs;
using System.Numerics;

namespace GalacticRun.Entities
{
    /// <summary>
    /// Represents the player-controlled ship/entity.
    ///
    /// Handles movement, animation state, and rendering. The Player
    /// updates based on keyboard input (WASD) and switches between
    /// idle and movement animation frames depending on motion.
    /// </summary>
    public class Player
    {
        // Idle texture shown when the player is not moving.
        private Texture2D idleFrame;

        // Array of movement animation frames.
        private Texture2D[] moveFrames;

        // Animation state
        private int currentFrame = 0;
        private float frameTimer = 0f;
        private float frameSpeed = 0.005f;

        // Player world position
        public Vector2 Position;

        // Movement speed in pixels per second
        private float speed = 600f;

        // Screen bounds for clamping movement
        private int screenWidth;
        private int screenHeight;

        /// <summary>
        /// Creates a new Player instance with idle and movement frames.
        /// </summary>
        public Player(Texture2D idle, Texture2D[] moveFrames, Vector2 startPos, int screenWidth, int screenHeight)
        {
            this.idleFrame = idle;
            this.moveFrames = moveFrames;
            this.Position = startPos;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        /// <summary>
        /// Updates player movement, animation, and clamps position to screen bounds.
        /// </summary>
        public void Update(float dt)
        {
            Vector2 move = Vector2.Zero;

            // Movement input
            if (Raylib.IsKeyDown(KeyboardKey.W)) move.Y -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.S)) move.Y += 1;
            if (Raylib.IsKeyDown(KeyboardKey.A)) move.X -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.D)) move.X += 1;

            bool isMoving = move.LengthSquared() > 0;

            if (isMoving)
            {
                // Normalize to avoid faster diagonal movement
                move = Vector2.Normalize(move);
                Position += move * speed * dt;

                // Advance animation frames
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
                // Reset to idle frame
                currentFrame = 0;
            }

            // Clamp player inside the screen
            Position.X = Math.Clamp(Position.X, 0, screenWidth - idleFrame.Width);
            Position.Y = Math.Clamp(Position.Y, 0, screenHeight - idleFrame.Height);
        }

        /// <summary>
        /// Draws the current animation frame at the player's position.
        /// </summary>
        public void Draw()
        {
            Texture2D frame = (currentFrame == 0)
                ? idleFrame
                : moveFrames[currentFrame];

            Raylib.DrawTexture(frame, (int)Position.X, (int)Position.Y, Color.White);
        }
    }
}
