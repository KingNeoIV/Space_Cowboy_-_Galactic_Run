using Raylib_cs;
using System.Numerics;

public class PauseMenuScreen
{
    // ------------------------------------------------------------
    // TEXTURES
    // ------------------------------------------------------------
    // These are the UI elements that make up the pause menu.
    // They are loaded once and reused every frame.
    private Texture2D windowPanel;   // Background panel for the pause window
    private Texture2D header;        // "PAUSE" title graphic
    private Texture2D playBtn;       // Resume button
    private Texture2D exitBtn;       // Exit-to-main-menu or exit-game button

    // ------------------------------------------------------------
    // UI ELEMENT POSITIONS
    // ------------------------------------------------------------
    // These vectors determine where each UI element is drawn.
    private Vector2 windowPos;       // Centered pause window position
    private Vector2 headerPos;       // Header text position
    private Vector2 playPos;         // Resume button position
    private Vector2 exitPos;         // Exit button position

    // ------------------------------------------------------------
    // BUTTON HITBOXES
    // ------------------------------------------------------------
    // These rectangles define the clickable areas for the buttons.
    private Rectangle playRect;      // Resume button hitbox
    private Rectangle exitRect;      // Exit button hitbox

    // Current screen resolution (updated if fullscreen changes)
    private int screenWidth;
    private int screenHeight;

    public PauseMenuScreen(int width, int height)
    {
        screenWidth = width;
        screenHeight = height;
    }

    // Called when the window size changes (ex: fullscreen toggle)
    public void SetScreenSize(int width, int height)
    {
        screenWidth = width;
        screenHeight = height;

        // Recalculate hitboxes to match new positions
        playRect = new Rectangle(playPos.X, playPos.Y, playBtn.Width, playBtn.Height);
        exitRect = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
    }

    public void Load()
    {
        // ------------------------------------------------------------
        // LOAD UI TEXTURES
        // ------------------------------------------------------------
        windowPanel = Raylib.LoadTexture("assets/ui/esc_menu/Window.png");
        header     = Raylib.LoadTexture("assets/ui/esc_menu/Header.png");
        playBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Play_BTN.png");
        exitBtn    = Raylib.LoadTexture("assets/ui/esc_menu/Exit_BTN.png");

        // ------------------------------------------------------------
        // POSITION UI ELEMENTS
        // ------------------------------------------------------------

        // Center the pause window on screen
        windowPos = new Vector2(
            screenWidth / 2 - windowPanel.Width / 2,
            screenHeight / 2 - windowPanel.Height / 2
        );

        // Header sits near the top of the window
        headerPos = new Vector2(
            screenWidth / 2 - header.Width / 2,
            windowPos.Y + 40
        );

        // Resume button placed below the header
        playPos = new Vector2(
            screenWidth / 2 - playBtn.Width / 2,
            headerPos.Y + header.Height + 120
        );

        // Exit button placed below the resume button
        exitPos = new Vector2(
            screenWidth / 2 - exitBtn.Width / 2,
            playPos.Y + playBtn.Height + 40
        );

        // Create clickable hitboxes based on texture sizes
        playRect = new Rectangle(playPos.X, playPos.Y, playBtn.Width, playBtn.Height);
        exitRect = new Rectangle(exitPos.X, exitPos.Y, exitBtn.Width, exitBtn.Height);
    }

    // ------------------------------------------------------------
    // UPDATE — Handles input and returns actions
    // ------------------------------------------------------------
    // Returns:
    //   "RESUME" → unpause the game
    //   "EXIT"   → exit gameplay
    //   null     → no action yet
    public string? Update()
    {
        Vector2 mouse = Raylib.GetMousePosition();

        // Resume button click
        if (Raylib.CheckCollisionPointRec(mouse, playRect) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
            return "RESUME";

        // Exit button click
        if (Raylib.CheckCollisionPointRec(mouse, exitRect) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
            return "EXIT";

        return null; // No button pressed this frame
    }

    // ------------------------------------------------------------
    // DRAW — Renders the pause menu UI
    // ------------------------------------------------------------
    public void Draw()
    {
        Vector2 mouse = Raylib.GetMousePosition();

        // Draw the pause window background
        Raylib.DrawTexture(windowPanel, (int)windowPos.X, (int)windowPos.Y, Color.White);

        // Draw the header text
        Raylib.DrawTexture(header, (int)headerPos.X, (int)headerPos.Y, Color.White);

        // ------------------------------------------------------------
        // RESUME BUTTON (Hover Highlight)
        // ------------------------------------------------------------
        bool hoverPlay = Raylib.CheckCollisionPointRec(mouse, playRect);
        Color playColor = hoverPlay ? Color.White : new Color(200, 200, 200, 255);

        Raylib.DrawTexture(playBtn, (int)playPos.X, (int)playPos.Y, playColor);

        // ------------------------------------------------------------
        // EXIT BUTTON (Hover Highlight)
        // ------------------------------------------------------------
        bool hoverExit = Raylib.CheckCollisionPointRec(mouse, exitRect);
        Color exitColor = hoverExit ? Color.White : new Color(200, 200, 200, 255);

        Raylib.DrawTexture(exitBtn, (int)exitPos.X, (int)exitPos.Y, exitColor);
    }
}
