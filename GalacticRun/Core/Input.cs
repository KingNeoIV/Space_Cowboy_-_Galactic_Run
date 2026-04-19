using Raylib_cs;

namespace GalacticRun.Core
{
    /*  
        Centralized input helper for common game actions.

        This static class abstracts raw key checks into semantic,
        intent-based methods (Pause, Confirm, Back). Screens and
        gameplay systems can query these without needing to know
        which physical keys trigger each action.
    */
    public static class Input
    {
        // Returns true when the player presses the pause key.
        public static bool PausePressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Escape);
        }

        // Returns true when the player confirms an action.
        public static bool ConfirmPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Enter);
        }

        // Returns true when the player requests to go back or cancel.
        public static bool BackPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Backspace);
        }
    }
}
