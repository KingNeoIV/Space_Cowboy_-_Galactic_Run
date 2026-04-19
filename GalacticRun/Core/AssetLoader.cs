using Raylib_cs;

namespace GalacticRun.Core
{
    /*  
        Centralized texture management for the game.
    
        The AssetLoader handles loading and caching of textures to ensure
        that each asset is loaded only once. This prevents redundant GPU
        allocations, reduces disk access, and keeps asset management
        consistent across the engine. All textures can be unloaded in a
        single call during shutdown.
    */
    public class AssetLoader
    {
        // Cache of loaded textures keyed by their file path.
        private readonly Dictionary<string, Texture2D> textures = new();

        // Loads a texture from the given file path if it has not already
        // been loaded. Returns the cached instance on subsequent calls.
        public Texture2D LoadTexture(string path)
        {
            if (!textures.ContainsKey(path))
                textures[path] = Raylib.LoadTexture(path);

            return textures[path];
        }

        /*  
            Unloads all textures currently stored in the cache.
            Should be called during game shutdown to release GPU memory
            and ensure a clean exit.
        */
        public void UnloadAll()
        {
            foreach (var tex in textures.Values)
                Raylib.UnloadTexture(tex);

            textures.Clear();
        }
    }
}
