using System;
using UnityEngine;

namespace _4XRD.Physics
{
    [Serializable]
    public class Euler6
    {
        [field: SerializeField] public float XY { get; private set; }

        [field: SerializeField] public float XZ { get; private set; }

        [field: SerializeField] public float XW { get; private set; }

        [field: SerializeField] public float YZ { get; private set; }

        [field: SerializeField] public float YW { get; private set; }

        [field: SerializeField] public float ZW { get; private set; }

        public Euler6()
        {
            XY = 0;
            XZ = 0;
            XW = 0;
            YZ = 0;
            YW = 0;
            ZW = 0;
        }

        public Euler6(float XY, float XZ, float XW, float YZ, float YW, float ZW)
        {
            this.XY = XY;
            this.XZ = XZ;
            this.XW = XW;
            this.YZ = YZ;
            this.YW = YW;
            this.ZW = ZW;
        }

        public static Euler6 zero => new Euler6();

        /// <summary>
        /// Rotation6 unary, equivalent to the identity.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Euler6 operator +(Euler6 r) => r;

        /// <summary>
        /// Rotation6 unary, equivalent to scaling by -1.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Euler6 operator -(Euler6 r) => new(
            -r.XY,
            -r.XZ,
            -r.XW,
            -r.YZ,
            -r.YW,
            -r.ZW
        );

        /// <summary>
        /// Rotation6 addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Euler6 operator +(Euler6 a, Euler6 b) => new(
            a.XY + b.XY,
            a.XZ + b.XZ,
            a.XW + b.XW,
            a.YZ + b.YZ,
            a.YW + b.YW,
            a.ZW + b.ZW
        );

        /// <summary>
        /// Rotation6 subtraction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Euler6 operator -(Euler6 a, Euler6 b) => a + -b;

        /// <summary>
        /// Right multiplication by a scalar.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Euler6 operator *(Euler6 r, float f) => new(
            r.XY * f,
            r.XZ * f,
            r.XW * f,
            r.YZ * f,
            r.YW * f,
            r.ZW * f
        );

        /// <summary>
        /// Left multiplication by a scalar.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Euler6 operator *(float f, Euler6 r) => r * f;

        /// <summary>
        /// Reverse engineer an Euler6 from a rotation.
        /// </summary>
        /// <param name="rot"></param>
        /// <returns></returns>
        public static Euler6 From(Rotation4x4 rot)
        {
            var euler = new Euler6();

            euler.XY = Mathf.Atan2(rot.matrix.m10, rot.matrix.m00);
            rot = rot.RotateXY(-euler.XY);

            euler.XZ = Mathf.Atan2(-rot.matrix.m20, rot.matrix.m00);
            rot = rot.RotateXZ(-euler.XZ);
            
            euler.XW = Mathf.Atan2(-rot.matrix.m30, rot.matrix.m00);
            rot = rot.RotateXW(-euler.XW);
            
            euler.YZ = Mathf.Atan2(rot.matrix.m21, rot.matrix.m11);
            rot = rot.RotateYZ(-euler.YZ);

            euler.YW = Mathf.Atan2(-rot.matrix.m31, rot.matrix.m11);
            rot = rot.RotateYW(-euler.YW);

            euler.ZW = Mathf.Atan2(rot.matrix.m32, rot.matrix.m22);
            
            return euler;
        }
    }
}
