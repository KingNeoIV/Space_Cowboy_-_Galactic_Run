namespace GalacticRun.Core
{
    public interface IScreen
    {
        void Initialize();
        void LoadContent();
        void Update();
        void Draw();
        void UnloadContent();
    }
}
