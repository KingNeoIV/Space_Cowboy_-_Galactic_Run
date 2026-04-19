using System.Collections.Generic;

namespace GalacticRun.Core
{
    /*  
        Manages the active screen stack for the game.

        The ScreenManager controls which screens are active, which one
        receives updates, and how screens are layered during rendering.
        Screens are pushed and popped like a stack, allowing modal
        overlays (such as pause menus) to sit on top of gameplay without
        destroying underlying state.
    */
    public class ScreenManager
    {
        // Stack of active screens, with the topmost screen being the current one.
        private readonly Stack<IScreen> screenStack = new();

        // Pushes a new screen onto the stack and initializes it.
        public void PushScreen(IScreen screen)
        {
            screen.Initialize();
            screen.LoadContent();
            screenStack.Push(screen);
        }

        // Removes the topmost screen and unloads its content.
        public void PopScreen()
        {
            if (screenStack.Count > 0)
            {
                var top = screenStack.Pop();
                top.UnloadContent();
            }
        }

        // Replaces the current screen with a new one.
        public void ReplaceScreen(IScreen screen)
        {
            PopScreen();
            PushScreen(screen);
        }

        // Updates only the topmost screen.
        public void Update()
        {
            if (screenStack.Count > 0)
                screenStack.Peek().Update();
        }

        /*  
            Draws screens from bottom to top, allowing layered rendering.

            Stops early if a modal screen (such as PauseMenuScreen) is
            encountered, preventing underlying screens from drawing over it.
        */
        public void Draw()
        {
            // Convert stack to array so we can draw bottom → top
            var screens = screenStack.ToArray();

            // Draw from bottom to top
            for (int i = screens.Length - 1; i >= 0; i--)
            {
                screens[i].Draw();

                // Modal screens block drawing of screens beneath them
                if (screens[i] is GalacticRun.Screens.PauseMenuScreen)
                    break;
            }
        }
    }
}
