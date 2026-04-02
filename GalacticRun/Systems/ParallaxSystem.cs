using GalacticRun.Core;

namespace GalacticRun.Systems
{
    public class ParallaxSystem
    {
        private readonly BackgroundScroller background;
        private readonly PlanetSystem planets;

        public ParallaxSystem(BackgroundScroller background, PlanetSystem planets)
        {
            this.background = background;
            this.planets = planets;
        }

        public void LoadContent()
        {
            background.LoadContent();
            planets.LoadContent();
        }

        public void Update()
        {
            background.Update();
            planets.Update();
        }

        public void Draw()
        {
            background.Draw();
            planets.Draw();
        }

        public void UnloadContent()
        {
            background.UnloadContent();
            planets.UnloadContent();
        }
    }
}
