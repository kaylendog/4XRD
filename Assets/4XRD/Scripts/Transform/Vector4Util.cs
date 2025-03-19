using UnityEngine;

namespace _4XRD.Transform
{
    /// <summary>
    /// Various Vector4 utilities and methods.
    /// </summary>
    public static class Vector4Util
    {
        /// <summary>
        /// Return a XYZ swizzle.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 XYZ(this Vector4 v) { return new Vector3(v.x, v.y, v.z); }

        /// <summary>
        /// Return a YZW swizzle.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 YZW(this Vector4 v) { return new Vector3(v.y, v.z, v.w); }

        /// <summary>
        /// Return a ZWX swizzle.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 ZWX(this Vector4 v) { return new Vector3(v.z, v.w, v.x); }

        /// <summary>
        /// Return a WXY swizzle.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 WXY(this Vector4 v) { return new Vector3(v.w, v.x, v.y); }


        /// <summary>
        /// Construct a Vector4 normal to all 3 arguments.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector4 TripleCross(this Vector4 a, Vector4 b, Vector4 c)
        {
            return new Vector4(
                -Vector3.Dot(YZW(a), Vector3.Cross(YZW(b), YZW(c))),
                Vector3.Dot(ZWX(a), Vector3.Cross(ZWX(b), ZWX(c))),
                -Vector3.Dot(WXY(a), Vector3.Cross(WXY(b), WXY(c))),
                Vector3.Dot(XYZ(a), Vector3.Cross(XYZ(b), XYZ(c))));
        }

        /// <summary>
        /// Vector dot product.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Dot(this Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4 ElemMul(this Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        /// <summary>
        /// Create a vector 4 from a vector 3.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 XYZW(this Vector3 v) { return new Vector4(v.x, v.y, v.z, 0f); }
    }
}
