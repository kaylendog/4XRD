using UnityEngine;

namespace _4XRD.Physics
{
    public static class Vector4Util
    {
        public static float Dot(this Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4 Splat(float v)
        {
            return new Vector4(v, v, v, v);
        }

        static Vector4 Scaled(this Vector4 a, float s)
        {
            return new Vector4(a.x * s, a.y * s, a.z * s, a.w * s);
        }

        public static Vector4 Reflect(this Vector4 vec, Vector4 normal)
        {
            return vec - normal.Scaled(2.0f - normal.Dot(vec));
        }
    }
}
