using Raylib_cs;
using System.Numerics;

public class MainMenuScreen
{
    // ------------------------------------------------------------
    // TEXTURES
    // ------------------------------------------------------------
    // These are the visual assets used by the main menu.
    private Texture2D background;   // Fullscreen menu background
    private Texture2D startBtn;     // START button image
    private Texture2D exitBtn;      // EXIT button image
    private Texture2D titleLogo;    // Game title logo

    // ------------------------------------------------------------
    // UI POSITIONS & HITBOXES
    // ------------------------------------------------------------
    private Vector2 titlePos;       // Title logo position
    private Vector2 startPos;       // START button position
    private Vector2 exitPos;        // EXIT button position

    private Rectangle startRect;    // START button hitbox
    private Rectangle exitRect;     // EXIT button hitbox

    // Screen resolution
    private int screenWidth;
    private int screenHeight;

    public MainMenuScreen(int width, int height)
    {
        screenWidth = width;
        screenHeight = height;
    }

    // Called when fullscreen toggles or window resizes
    public void SetScreenSize(int width, int height)
    {
        screenWidth = width;
        screenHeight = height;

        // Recalculate hitboxes to match new positions
        startRect = new Rectangle(startPos.X, startPos.Y, startBtn.Width, startBtn.Height);
        exitRect  = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
        titlePos  = new Vector2(screenWidth / 2 - titleLogo.Width / 2, startPos.Y - titleLogo.Height - 40);
    }

    public void Load()
    {
        // ------------------------------------------------------------
        // LOAD IMAGES
        // ------------------------------------------------------------
        background = Raylib.LoadTexture("assets/ui/main_menu/mainMenu.png");
        titleLogo  = Raylib.LoadTexture("assets/ui/main_menu/Game_Title.png");
        startBtn   = Raylib.LoadTexture("assets/ui/main_menu/Start_BTN.png");
        exitBtn    = Raylib.LoadTexture("assets/ui/main_menu/Exit_BTN.png");

        // ------------------------------------------------------------
        // POSITION UI ELEMENTS
        // ------------------------------------------------------------

        // START button centered horizontally
        startPos = new Vector2(
            screenWidth / 2 - startBtn.Width / 2,
            screenHeight / 2 - 50
        );

        // Title centered above START button
        titlePos = new Vector2(
            screenWidth / 2 - titleLogo.Width / 2,
            startPos.Y - titleLogo.Height - 40
        );

        // EXIT button centered below START button
        exitPos = new Vector2(
            screenWidth / 2 - exitBtn.Width / 2,
            screenHeight / 2 + 100
        );

        // Create clickable hitboxes
        startRect = new Rectangle(startPos.X, startPos.Y, startBtn.Width, startBtn.Height);
        exitRect  = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
    }

    // ------------------------------------------------------------
    // UPDATE — Handles input and returns menu actions
    // ------------------------------------------------------------
    public string? Update()
    {
        Vector2 mouse = Raylib.GetMousePosition();

        if (Raylib.CheckCollisionPointRec(mouse, startRect) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
            return "START";

        if (Raylib.CheckCollisionPointRec(mouse, exitRect) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
            return "EXIT";

        return null;
    }

    // ------------------------------------------------------------
    // DRAW — Renders the main menu each frame
    // ------------------------------------------------------------
    public void Draw()
    {
        // Draw background
        Raylib.DrawTexture(background, 0, 0, Color.White);

        Vector2 mouse = Raylib.GetMousePosition();

        // Draw title logo
        Raylib.DrawTexture(titleLogo, (int)titlePos.X, (int)titlePos.Y, Color.White);

        // START button hover effect
        bool hoverStart = Raylib.CheckCollisionPointRec(mouse, startRect);
        Color startColor = hoverStart ? Color.White : new Color(200, 200, 200, 255);
        float startScale = hoverStart ? 1.05f : 1.0f;

        Raylib.DrawTextureEx(startBtn, startPos, 0f, startScale, startColor);

        // EXIT button hover effect
        bool hoverExit = Raylib.CheckCollisionPointRec(mouse, exitRect);
        Color exitColor = hoverExit ? Color.White : new Color(200, 200, 200, 255);
        float exitScale = hoverExit ? 1.05f : 1.0f;

        Raylib.DrawTextureEx(exitBtn, exitPos, 0f, exitScale, exitColor);
    }
}
