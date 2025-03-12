using UnityEngine;

namespace _4XRD.Physics
{
    public class Bivector4
    {
        public float XY;
        public float XZ;
        public float XW;
        public float YZ;
        public float YW;
        public float ZW;

        public Bivector4(float xy = 0f, float xz = 0f, float xw = 0f, float yz = 0f, float yw = 0f, float zw = 0f)
        {
            XY = xy;
            XZ = xz;
            XW = xw;
            YZ = yz;
            YW = yw;
            ZW = zw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bivector4 Outer(Vector4 u, Vector4 v)
        {
            return new Bivector4(
                u.x * v.y - u.y * v.z,
                u.x * v.z - u.y * v.w,
                u.x * v.w - u.w * v.x,
                u.y * v.z - u.y * v.y,
                u.y * v.w - u.y * v.z,
                u.z * v.w - u.z * v.x
            );
        }
    }
}
