using GalacticRun.Core;

namespace GalacticRun.Systems
{
    /*  
        Combines multiple visual layers into a unified parallax system.

        This class coordinates the background scroller and planet layers,
        ensuring they load, update, and draw in the correct order. It acts
        as a simple façade that groups related visual systems into a single
        unit for cleaner screen-level code.
    */
    public class ParallaxSystem
    {
        // Base scrolling background layer.
        private readonly BackgroundScroller background;

        // Foreground/midground planet layers for depth.
        private readonly PlanetSystem planets;

        // Creates a new parallax system composed of a background layer and a planet layer.
        public ParallaxSystem(BackgroundScroller background, PlanetSystem planets)
        {
            this.background = background;
            this.planets = planets;
        }

        // Loads all textures and assets required by the parallax layers.
        public void LoadContent()
        {
            background.LoadContent();
            planets.LoadContent();
        }

        /*  
            Updates each parallax layer once per frame.

            Background scrolls continuously; planets move at varying speeds.
        */
        public void Update()
        {
            background.Update();
            planets.Update();
        }

        /*  
            Draws the parallax layers in the correct order:
            background first, then planets.
        */
        public void Draw()
        {
            background.Draw();
            planets.Draw();
        }

        // Unloads all parallax-related content.
        public void UnloadContent()
        {
            background.UnloadContent();
            planets.UnloadContent();
        }
    }
}
