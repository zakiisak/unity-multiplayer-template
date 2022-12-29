using UnityEngine;

namespace Assets.Code
{
    public static class MathUtils
    {
        public static Vector2 Circle2D(float degrees)
        {
            float x = Mathf.Cos(degrees * Mathf.Deg2Rad);
            float y = Mathf.Sin(degrees * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }

    }
}
