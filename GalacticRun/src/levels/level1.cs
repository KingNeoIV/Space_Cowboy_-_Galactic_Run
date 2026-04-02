using Raylib_cs;
using System.Numerics;

public class Level1
{
    // ------------------------------------------------------------
    // SCREEN DIMENSIONS
    // ------------------------------------------------------------
    // These values determine how the level scales and where objects
    // are positioned. They are passed in from Program.cs.
    private int screenWidth;
    private int screenHeight;

    // ------------------------------------------------------------
    // TEXTURES (Background + Parallax Planets)
    // ------------------------------------------------------------
    // These textures are loaded once and reused every frame.
    private Texture2D background;   // Scrolling starfield
    private Texture2D planet;       // Foreground planet (closest)
    private Texture2D planet1;      // Mid‑distance planet
    private Texture2D planet2;      // Far‑distance planet

    // ------------------------------------------------------------
    // BACKGROUND SCROLLING
    // ------------------------------------------------------------
    // scrollY moves downward to simulate flying upward through space.
    private float scrollY;

    // ------------------------------------------------------------
    // PLANET POSITIONS
    // ------------------------------------------------------------
    // Each planet scrolls downward at a different speed to create
    // a parallax depth effect.
    private float planetX, planetY;
    private float planet1X, planet1Y;
    private float planet2X, planet2Y;

    public Level1(int width, int height)
    {
        screenWidth = width;
        screenHeight = height;
    }

    public void Load()
    {
        // ------------------------------------------------------------
        // LOAD ALL LEVEL TEXTURES
        // ------------------------------------------------------------
        background = Raylib.LoadTexture("assets/level/level_1/background.png");
        planet     = Raylib.LoadTexture("assets/level/level_1/planet.png");
        planet1    = Raylib.LoadTexture("assets/level/level_1/planet_1.png");
        planet2    = Raylib.LoadTexture("assets/level/level_1/planet_2.png");

        // ------------------------------------------------------------
        // INITIAL SCROLL POSITION
        // ------------------------------------------------------------
        scrollY = 0;

        // ------------------------------------------------------------
        // INITIAL PLANET POSITIONS
        // ------------------------------------------------------------
        // Each planet starts above the screen at different heights.
        // They scroll downward and reset when they exit the screen.
        planetX  = screenWidth / 2 - planet.Width / 2; // Centered horizontally
        planetY  = -800;

        planet1X = screenWidth / 1.5f - planet1.Width / 2; // Slightly to the right
        planet1Y = -1200;

        planet2X = screenWidth * 0.05f; // Far left
        planet2Y = -1000;
    }

    public void Update()
    {
        // ------------------------------------------------------------
        // BACKGROUND SCROLLING
        // ------------------------------------------------------------
        scrollY += 0.2f;
        if (scrollY >= screenHeight)
            scrollY = 0; // Loop the background seamlessly

        // ------------------------------------------------------------
        // PARALLAX PLANET MOVEMENT
        // ------------------------------------------------------------
        // Each planet scrolls at a different speed to simulate depth.

        // Foreground planet (fastest)
        planetY += 0.4f;
        if (planetY > screenHeight)
            planetY = -800;

        // Mid‑distance planet
        planet1Y += 0.6f;
        if (planet1Y > screenHeight)
            planet1Y = -1200;

        // Far‑distance planet (slowest)
        planet2Y += 0.4f;
        if (planet2Y > screenHeight)
            planet2Y = -1000;
    }

    public void Draw()
    {
        // ------------------------------------------------------------
        // DRAW SCROLLING BACKGROUND
        // ------------------------------------------------------------
        // Two copies are drawn to create a continuous scrolling loop.
        Rectangle src = new Rectangle(0, 0, background.Width, background.Height);
        Rectangle dest1 = new Rectangle(0, scrollY, screenWidth, screenHeight);
        Rectangle dest2 = new Rectangle(0, scrollY - screenHeight, screenWidth, screenHeight);

        Raylib.DrawTexturePro(background, src, dest1, Vector2.Zero, 0f, Color.White);
        Raylib.DrawTexturePro(background, src, dest2, Vector2.Zero, 0f, Color.White);

        // ------------------------------------------------------------
        // DRAW PARALLAX PLANETS
        // ------------------------------------------------------------

        // Farthest planet (smallest, slowest)
        Raylib.DrawTexture(planet2, (int)planet2X, (int)planet2Y, Color.White);

        // Mid‑distance planet (scaled up slightly)
        Raylib.DrawTextureEx(
            planet1,
            new Vector2(planet1X, planet1Y),
            0f,
            1.6f, // scale factor
            Color.White
        );

        // Foreground planet (largest, fastest)
        Raylib.DrawTextureEx(
            planet,
            new Vector2(planetX, planetY),
            0f,
            1.4f, // scale factor
            Color.White
        );
    }
}
