namespace GalacticRun.Systems
{
    /*  
        Basic 2D camera system used for tracking world position and
        applying camera effects such as screen shake.

        This class currently provides simple positional control but is
        structured to support future expansion (shake, smoothing,
        following targets, etc.).
    */
    public class CameraSystem
    {
        // Current camera X position in world space.
        public float X { get; private set; } = 0f;

        // Current camera Y position in world space.
        public float Y { get; private set; } = 0f;

        // Triggers a camera shake effect (placeholder).
        public void Shake(float intensity) { }

        // Moves the camera instantly to the specified world position.
        public void MoveTo(float x, float y)
        {
            X = x;
            Y = y;
        }

        // Updates camera state each frame.
        public void Update() { }
    }
}
