using Raylib_cs;

namespace GalacticRun.Core
{
    /// <summary>
    /// Centralized input helper for common game actions.
    ///
    /// This static class abstracts raw key checks into semantic
    /// intent-based methods (Pause, Confirm, Back). Screens and
    /// gameplay systems can query these without needing to know
    /// which physical keys trigger each action.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Returns true when the player presses the pause key.
        /// Used for toggling menus or pausing gameplay.
        /// </summary>
        public static bool PausePressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Escape);
        }

        /// <summary>
        /// Returns true when the player confirms an action.
        /// Typically used for menu selections or advancing UI.
        /// </summary>
        public static bool ConfirmPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Enter);
        }

        /// <summary>
        /// Returns true when the player requests to go back or cancel.
        /// Commonly used for navigating backward in menus.
        /// </summary>
        public static bool BackPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Backspace);
        }
    }
}
