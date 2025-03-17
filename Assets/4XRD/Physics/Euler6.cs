using System;
using UnityEngine;

namespace _4XRD.Physics
{
    [Serializable]
    public class Euler6
    {
        [field: SerializeField] public float XW { get; private set; }

        [field: SerializeField] public float YW { get; private set; }

        [field: SerializeField] public float ZW { get; private set; }
        
        [field: SerializeField] public float XY { get; private set; }

        [field: SerializeField] public float YZ { get; private set; }

        [field: SerializeField] public float XZ { get; private set; }

        public Euler6()
        {
            XW = 0;
            YW = 0;
            ZW = 0;
            XY = 0;
            YZ = 0;
            XZ = 0;
        }

        public Euler6(float XW, float YW, float ZW, float XY, float YZ, float XZ)
        {
            this.XW = XW;
            this.YW = YW;
            this.ZW = ZW;
            this.XY = XY;
            this.YZ = YZ;
            this.XZ = XZ;
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
            -r.XW,
            -r.YW,
            -r.ZW,
            -r.XY,
            -r.YZ,
            -r.XZ
        );

        /// <summary>
        /// Rotation6 addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Euler6 operator +(Euler6 a, Euler6 b) => new(
            a.XW + b.XW,
            a.YW + b.YW,
            a.ZW + b.ZW,
            a.XY + b.XY,
            a.YZ + b.YZ,
            a.XZ + b.XZ
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
            r.XW * f,
            r.YW * f,
            r.ZW * f,
            r.XY * f,
            r.YZ * f,
            r.XZ * f
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

            euler.XZ = Mathf.Atan2(-rot.matrix.m20, rot.matrix.m00);
            rot = rot.RotateXZ(-euler.XZ);

            euler.YZ = Mathf.Atan2(-rot.matrix.m21, rot.matrix.m11);
            rot = rot.RotateYZ(-euler.YZ);

            euler.XY = Mathf.Atan2(rot.matrix.m01, rot.matrix.m00);
            rot = rot.RotateXY(-euler.XY);

            euler.ZW = Mathf.Atan2(-rot.matrix.m32, rot.matrix.m22);
            rot = rot.RotateZW(-euler.ZW);

            euler.YW = Mathf.Atan2(-rot.matrix.m31, rot.matrix.m11);
            rot = rot.RotateYW(-euler.YW);

            euler.XW = Mathf.Atan2(-rot.matrix.m30, rot.matrix.m00);
            rot = rot.RotateXW(-euler.XW);
            
            return euler;
        }

        public Euler6 GetWRotations()
        {
            return new Euler6(XW, YW, ZW, 0, 0, 0);
        }

        public Euler6 SetUnityEuler3(Vector3 euler)
        {
            euler *= Mathf.Deg2Rad;
            return new Euler6(
                XW,
                YW,
                ZW,
                -euler.z,
                -euler.x,
                euler.y
            );
        }

        public Vector3 GetUnityEuler3()
        {
            return new Vector3(-YZ, XZ, -XY) * Mathf.Rad2Deg;
        }

        public override string ToString()
        {
            return $"({XW}, {YW}, {ZW}, {XY}, {YZ}, {XZ})";
        }
    }
}
