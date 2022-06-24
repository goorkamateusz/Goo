namespace Goo.Tools.UnityHelpers
{
    public static class AngleHelper
    {
        public static float From180To180(float angle)
        {
            while (angle > 180) angle -= 360;
            while (angle < -180) angle += 360;
            return angle;
        }

        public static float From0To360(float angle)
        {
            while (angle < 0) angle += 360;
            while (angle > 360) angle -= 360;
            return angle;
        }
    }
}
