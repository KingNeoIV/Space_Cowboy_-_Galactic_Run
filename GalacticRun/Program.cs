using GalacticRun.Core;

namespace GalacticRun
{
    /// <summary>
    /// Application entry point for Galactic Run.
    ///
    /// Initializes the main Game instance and starts the primary
    /// game loop. This class is intentionally minimal to keep all
    /// engine and gameplay logic encapsulated within the Game class.
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            // Create and run the core game instance.
            Game game = new Game();
            game.Run();
        }
    }
}
