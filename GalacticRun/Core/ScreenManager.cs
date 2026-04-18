using System.Collections.Generic;

namespace GalacticRun.Core
{
    /// <summary>
    /// Manages the active screen stack for the game.
    ///
    /// The ScreenManager controls which screens are active, which one
    /// receives updates, and how screens are layered during rendering.
    /// Screens are pushed and popped like a stack, allowing modal
    /// overlays (e.g., pause menus) to sit on top of gameplay without
    /// destroying underlying state.
    /// </summary>
    public class ScreenManager
    {
        // Stack of active screens, with the topmost screen being the current one.
        private readonly Stack<IScreen> screenStack = new();

        /// <summary>
        /// Pushes a new screen onto the stack.
        /// Initializes and loads its content before it becomes active.
        /// </summary>
        public void PushScreen(IScreen screen)
        {
            screen.Initialize();
            screen.LoadContent();
            screenStack.Push(screen);
        }

        /// <summary>
        /// Removes the topmost screen from the stack.
        /// Unloads its content to free resources.
        /// </summary>
        public void PopScreen()
        {
            if (screenStack.Count > 0)
            {
                var top = screenStack.Pop();
                top.UnloadContent();
            }
        }

        /// <summary>
        /// Replaces the current screen with a new one.
        /// Equivalent to popping the current screen and pushing another.
        /// </summary>
        public void ReplaceScreen(IScreen screen)
        {
            PopScreen();
            PushScreen(screen);
        }

        /// <summary>
        /// Updates only the topmost screen.
        /// This ensures that underlying screens remain paused or inactive.
        /// </summary>
        public void Update()
        {
            if (screenStack.Count > 0)
                screenStack.Peek().Update();
        }

        /// <summary>
        /// Draws screens from bottom to top, allowing layered rendering.
        /// Stops early if a modal screen (e.g., PauseMenuScreen) is encountered,
        /// preventing underlying screens from drawing over it.
        /// </summary>
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
