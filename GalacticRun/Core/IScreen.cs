namespace GalacticRun.Core
{
    /*  
        Defines the lifecycle contract for all game screens.

        Each screen (Main Menu, Gameplay, Pause Menu, etc.) implements
        this interface to participate in the engine's update/draw loop.
        The ScreenManager invokes these methods in a consistent order,
        ensuring predictable initialization, content loading, updating,
        rendering, and cleanup.
    */
    public interface IScreen
    {
        // Called once when the screen is first created.
        void Initialize();

        // Loads all graphical and asset-dependent content.
        void LoadContent();

        // Updates the screen's logic once per frame.
        void Update();

        // Draws the screen's visual elements once per frame.
        void Draw();

        // Unloads all content previously loaded by LoadContent.
        void UnloadContent();
    }
}
