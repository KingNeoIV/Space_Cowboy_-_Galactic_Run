namespace GalacticRun.Systems
{
    /// <summary>
    /// Basic 2D camera system used for tracking world position and
    /// applying camera effects such as screen shake.
    ///
    /// This class currently provides simple positional control but
    /// is structured to support future expansion (shake, smoothing,
    /// following targets, etc.).
    /// </summary>
    public class CameraSystem
    {
        /// <summary>
        /// Current camera X position in world space.
        /// </summary>
        public float X { get; private set; } = 0f;

        /// <summary>
        /// Current camera Y position in world space.
        /// </summary>
        public float Y { get; private set; } = 0f;

        /// <summary>
        /// Triggers a camera shake effect.
        /// Currently a placeholder for future implementation.
        /// </summary>
        public void Shake(float intensity) { }

        /// <summary>
        /// Moves the camera instantly to the specified world position.
        /// </summary>
        public void MoveTo(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Updates camera state each frame.
        /// Reserved for future smoothing, shake decay, or tracking logic.
        /// </summary>
        public void Update() { }
    }
}
