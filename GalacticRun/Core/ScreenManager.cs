using System.Collections.Generic;

namespace GalacticRun.Core
{
    public class ScreenManager
    {
        private readonly Stack<IScreen> screenStack = new();

        public void PushScreen(IScreen screen)
        {
            screen.Initialize();
            screen.LoadContent();
            screenStack.Push(screen);
        }

        public void PopScreen()
        {
            if (screenStack.Count > 0)
            {
                var top = screenStack.Pop();
                top.UnloadContent();
            }
        }

        public void ReplaceScreen(IScreen screen)
        {
            PopScreen();
            PushScreen(screen);
        }

        public void Update()
        {
            if (screenStack.Count > 0)
                screenStack.Peek().Update();
        }

        public void Draw()
        {
            // Convert stack to array so we can draw bottom → top
            var screens = screenStack.ToArray();

            // Draw from bottom to top
            for (int i = screens.Length - 1; i >= 0; i--)
            {
                screens[i].Draw();

                // Pause menu is modal — stop drawing anything underneath it
                if (screens[i] is GalacticRun.Screens.PauseMenuScreen)
                    break;
            }
        }

    }
}
