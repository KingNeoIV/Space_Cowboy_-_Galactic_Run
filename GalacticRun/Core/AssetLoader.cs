using Raylib_cs;

namespace GalacticRun.Core
{
    public class AssetLoader
    {
        private readonly Dictionary<string, Texture2D> textures = new();

        public Texture2D LoadTexture(string path)
        {
            if (!textures.ContainsKey(path))
                textures[path] = Raylib.LoadTexture(path);

            return textures[path];
        }

        public void UnloadAll()
        {
            foreach (var tex in textures.Values)
                Raylib.UnloadTexture(tex);

            textures.Clear();
        }
    }
}
