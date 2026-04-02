using Raylib_cs;

// ------------------------------------------------------------
// WINDOW INITIALIZATION
// ------------------------------------------------------------

// Initial desired window size (Raylib may override this depending on monitor)
int screenWidth = 1920;
int screenHeight = 1080;

// Create the game window
Raylib.InitWindow(screenWidth, screenHeight, "Galactic Run");
Raylib.SetTargetFPS(60);

// Disable Raylib's default ESC-to-exit behavior
Raylib.SetExitKey(KeyboardKey.Null);

// After the window is created, Raylib reports the *actual* fullscreen size
screenWidth = Raylib.GetScreenWidth();
screenHeight = Raylib.GetScreenHeight();

// Used to ensure fullscreen is applied only once
bool fullscreenApplied = false;


// ------------------------------------------------------------
// MAIN MENU INITIALIZATION
// ------------------------------------------------------------

// Create and load the main menu UI
MainMenuScreen menu = new MainMenuScreen(screenWidth, screenHeight);
menu.Load();


// ------------------------------------------------------------
// MAIN MENU LOOP
// ------------------------------------------------------------
// Runs until the player chooses START or EXIT
// ------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{
    // Apply fullscreen only once after the first frame
    if (!fullscreenApplied)
    {
        Raylib.ToggleFullscreen();
        fullscreenApplied = true;

        // Update menu layout to match new fullscreen resolution
        screenWidth = Raylib.GetScreenWidth();
        screenHeight = Raylib.GetScreenHeight();
        menu.SetScreenSize(screenWidth, screenHeight);
    }

    // Handle menu button actions
    string? action = menu.Update();

    if (action == "START")
        break;      // Continue to gameplay

    if (action == "EXIT")
        return;     // Quit the entire game

    // Draw the main menu
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    menu.Draw();
    Raylib.EndDrawing();
}


// ------------------------------------------------------------
// PAUSE MENU INITIALIZATION
// ------------------------------------------------------------

// Create and load the pause menu UI
PauseMenuScreen pauseMenu = new PauseMenuScreen(screenWidth, screenHeight);
pauseMenu.Load();

// Tracks whether gameplay is paused
bool isPaused = false;


// ------------------------------------------------------------
// LEVEL 1 INITIALIZATION
// ------------------------------------------------------------

// Create and load the first level
Level1 level1 = new Level1(screenWidth, screenHeight);
level1.Load();


// ------------------------------------------------------------
// LEVEL 1 GAME LOOP
// ------------------------------------------------------------
// Runs until the player exits from the pause menu or closes the window
// ------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{
    // Toggle pause when ESC is pressed
    if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        isPaused = !isPaused;

    // Only update gameplay when NOT paused
    if (!isPaused)
        level1.Update();

    // Begin drawing the frame
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    // Draw the gameplay world
    level1.Draw();

    // Draw pause menu on top of gameplay when paused
    if (isPaused)
    {
        pauseMenu.Draw();
        string? action = pauseMenu.Update();

        if (action == "RESUME")
            isPaused = false;   // Return to gameplay

        if (action == "EXIT")
            break;              // Exit gameplay loop cleanly
    }

    Raylib.EndDrawing();
}


// ------------------------------------------------------------
// CLEAN SHUTDOWN
// ------------------------------------------------------------

// Close the window and release all Raylib resources
Raylib.CloseWindow();
