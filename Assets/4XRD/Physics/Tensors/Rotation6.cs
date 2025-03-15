using System;
using UnityEngine;

namespace _4XRD.Physics.Tensors
{
    [Serializable]
    public class Rotation6
    {
        [field: SerializeField] public float XY { get; private set; }
        
        [field: SerializeField] public float XZ { get; private set; }
        
        [field: SerializeField] public float XW { get; private set; }
        
        [field: SerializeField] public float YZ { get; private set; }
        
        [field: SerializeField] public float YW { get; private set; }
        
        [field: SerializeField] public float ZW { get; private set; }

        public Rotation6()
        {
            XY = 0;
            XZ = 0;
            XW = 0;
            YZ = 0;
            YW = 0;
            ZW = 0;
        }

        public Rotation6(float XY, float XZ, float XW, float YZ, float YW, float ZW)
        {
            this.XY = XY;
            this.XZ = XZ;
            this.XW = XW;
            this.YZ = YZ;
            this.YW = YW;
            this.ZW = ZW;
        }

        public static Rotation6 zero => new Rotation6();

        /// <summary>
        /// Rotation6 unary, equivalent to the identity.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rotation6 operator +(Rotation6 r) => r;

        /// <summary>
        /// Rotation6 unary, equivalent to scaling by -1.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rotation6 operator -(Rotation6 r) => new(
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
        public static Rotation6 operator +(Rotation6 a, Rotation6 b) => new(
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
        public static Rotation6 operator -(Rotation6 a, Rotation6 b) => a + -b;

        /// <summary>
        /// Right multiplication by a scalar.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Rotation6 operator *(Rotation6 r, float f) => new(
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
        public static Rotation6 operator *(float f, Rotation6 r) => r * f;
    }
}