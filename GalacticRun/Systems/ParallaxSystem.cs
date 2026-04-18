using GalacticRun.Core;

namespace GalacticRun.Systems
{
    /// <summary>
    /// Combines multiple visual layers into a unified parallax system.
    ///
    /// This class coordinates the background scroller and planet layers,
    /// ensuring they load, update, and draw in the correct order. It acts
    /// as a simple façade that groups related visual systems into a single
    /// unit for cleaner screen-level code.
    /// </summary>
    public class ParallaxSystem
    {
        // Base scrolling background layer.
        private readonly BackgroundScroller background;

        // Foreground/midground planet layers for depth.
        private readonly PlanetSystem planets;

        /// <summary>
        /// Creates a new parallax system composed of a background layer
        /// and a planet layer.
        /// </summary>
        public ParallaxSystem(BackgroundScroller background, PlanetSystem planets)
        {
            this.background = background;
            this.planets = planets;
        }

        /// <summary>
        /// Loads all textures and assets required by the parallax layers.
        /// </summary>
        public void LoadContent()
        {
            background.LoadContent();
            planets.LoadContent();
        }

        /// <summary>
        /// Updates each parallax layer once per frame.
        /// Background scrolls continuously; planets move at varying speeds.
        /// </summary>
        public void Update()
        {
            background.Update();
            planets.Update();
        }

        /// <summary>
        /// Draws the parallax layers in the correct order:
        /// background first, then planets.
        /// </summary>
        public void Draw()
        {
            background.Draw();
            planets.Draw();
        }

        /// <summary>
        /// Unloads all parallax-related content.
        /// Most cleanup is handled by the AssetLoader.
        /// </summary>
        public void UnloadContent()
        {
            background.UnloadContent();
            planets.UnloadContent();
        }
    }
}
