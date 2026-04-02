using Raylib_cs;

namespace GalacticRun.Core
{
    public static class Time
    {
        public const float FixedDelta = 1f / 60f; // 60 Hz update
        private static float accumulator = 0f;

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
