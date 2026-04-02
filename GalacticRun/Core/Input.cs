using Raylib_cs;

namespace GalacticRun.Core
{
    public static class Input
    {
        public static bool PausePressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Escape);
        }

        public static bool ConfirmPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Enter);
        }

        public static bool BackPressed()
        {
            return Raylib.IsKeyPressed(KeyboardKey.Backspace);
        }
    }
}
