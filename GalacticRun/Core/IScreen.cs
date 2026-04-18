namespace GalacticRun.Core
{
    /// <summary>
    /// Defines the lifecycle contract for all game screens.
    ///
    /// Each screen (Main Menu, Gameplay, Pause Menu, etc.) implements
    /// this interface to participate in the engine's update/draw loop.
    /// The ScreenManager invokes these methods in a consistent order,
    /// ensuring predictable initialization, content loading, updating,
    /// rendering, and cleanup.
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Called once when the screen is first created.
        /// Used for non-graphics initialization such as setting up state.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Loads all graphical and asset-dependent content.
        /// Called after Initialize and before the screen becomes active.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Updates the screen's logic once per frame.
        /// Handles input, animations, and game state changes.
        /// </summary>
        void Update();

        /// <summary>
        /// Draws the screen's visual elements once per frame.
        /// Called after Update during the main render pass.
        /// </summary>
        void Draw();

        /// <summary>
        /// Unloads all content previously loaded by LoadContent.
        /// Called when the screen is removed or replaced.
        /// </summary>
        void UnloadContent();
    }
}
