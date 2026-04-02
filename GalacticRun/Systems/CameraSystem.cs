namespace GalacticRun.Systems
{
    public class CameraSystem
    {
        public float X { get; private set; } = 0f;
        public float Y { get; private set; } = 0f;

        public void Shake(float intensity) { }
        public void MoveTo(float x, float y) { X = x; Y = y; }
        public void Update() { }
    }
}
