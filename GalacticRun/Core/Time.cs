using Raylib_cs;

namespace GalacticRun.Core
{
    /// <summary>
    /// Provides fixed‑timestep update timing for deterministic gameplay.
    ///
    /// The Time class accumulates frame time and signals when the engine
    /// should run a fixed update step (typically 60 Hz). This ensures
    /// consistent gameplay behavior regardless of rendering frame rate.
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Duration of a single fixed update step (60 updates per second).
        /// </summary>
        public const float FixedDelta = 1f / 60f;

        // Accumulates elapsed time until it reaches the fixed step threshold.
        private static float accumulator = 0f;

        /// <summary>
        /// Determines whether enough time has passed to run a fixed update.
        ///
        /// Returns true when the accumulated frame time meets or exceeds
        /// the fixed timestep duration. Excess time is carried over to
        /// maintain long‑term timing accuracy.
        /// </summary>
        public static bool ShouldUpdate()
        {
            accumulator += Raylib.GetFrameTime();

            if (accumulator >= FixedDelta)
            {
                accumulator -= FixedDelta;
                return true;
            }

            return false;
        }
    }
}
