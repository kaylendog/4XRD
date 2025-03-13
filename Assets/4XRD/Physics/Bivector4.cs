using UnityEngine;

namespace _4XRD.Physics
{
    /// <summary>
    /// A bivector, or 2-blade.
    /// </summary>
    public record Bivector4
    {
        public readonly float XY, XZ, XW, YZ, YW, ZW;

        /// <summary>
        /// The zero bivector.
        /// </summary>
        public static Bivector4 Zero { get => new(); }

        /// <summary>
        /// Biector unary, equivalent to the identity.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bivector4 operator +(Bivector4 v) => v;

        /// <summary>
        /// Component-wise negation.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bivector4 operator -(Bivector4 v) => new Bivector4(
            -v.XY,
            -v.XZ,
            -v.XW,
            -v.YZ,
            -v.YW,
            -v.ZW
        );

        /// <summary>
        /// Bivector addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bivector4 operator +(Bivector4 a, Bivector4 b) => new Bivector4(
            a.XY + b.XY,
            a.XZ + b.XZ,
            a.XW + b.XW,
            a.YZ + b.YZ,
            a.YW + b.YW,
            a.ZW + b.ZW
        );

        /// <summary>
        /// Bivector subtraction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bivector4 operator -(Bivector4 a, Bivector4 b) => a + -b;

        /// <summary>
        /// Right multiplication by a scalar.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Bivector4 operator *(Bivector4 v, float f) => new Bivector4(
            v.XY * f,
            v.XZ * f,
            v.XW * f,
            v.YZ * f,
            v.YW * f,
            v.ZW * f
        );

        /// <summary>
        /// Left multiplication by a scalar.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bivector4 operator *(float f, Bivector4 v) => v * f;

        /// <summary>
        /// Inner product with a vector.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator |(Bivector4 b, Vector4 v) => new Vector4(
            b.XY * v.Y + b.XZ * v.Z + b.XW * v.W,
            -b.XY * v.X + b.YZ * v.Z + b.YW * v.W,
            -b.XZ * v.X - b.YZ * v.Y + b.ZW * v.W,
            -b.XW * v.X - b.YW * v.Y - b.ZW * v.Z
        );

        /// <summary>
        /// Inner product with a trivector.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector4 operator |(Bivector4 b, Trivector4 t) => new Vector4(
            -b.YW * t.XYW - b.YZ * t.XYZ - b.ZW * t.XZW,
            b.XW * t.XYW + b.XZ * t.XYZ - b.ZW * t.YZW,
            b.XW * t.XZW - b.XY * t.XYZ + b.YW * t.YZW,
           -b.XY * t.XYW - b.XZ * t.XZW - b.YZ * t.YZW
        );

        /// <summary>
        /// Outer product with a vector.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Trivector4 operator ^(Bivector4 b, Vector4 v) => new Trivector4(
            xyz: b.XY * v.Z - b.XZ * v.Y + b.YZ * v.X,
            xyw: -b.XW * v.Y + b.XY * v.W + b.YW * v.X,
            xzw: -b.XW * v.Z + b.XZ * v.W + b.ZW * v.X,
            yzw: -b.YW * v.Z + b.YZ * v.W + b.ZW * v.Y
        );

        /// <summary>
        /// Geometric product of a bivector with a vector.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static (Vector4, Trivector4) operator *(Bivector4 b, Vector4 v) => (
            b | v,
            b ^ v
        );

        /// <summary>
        /// Geometric product of a bivector with a bivector.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static (float, Bivector4, Quadvector4) operator *(Bivector4 a, Bivector4 b)
        {
            return (
                -a.XY * b.XY
                - a.XZ * b.XZ
                - a.XW * b.XW
                - a.YZ * b.YZ
                - a.YW * b.YW
                - a.ZW * b.ZW,
                new Bivector4(
                    -a.XW * b.YW - a.XZ * b.YZ + a.YW * b.XW + a.YZ * b.XZ,
                    -a.XW * b.ZW + a.XY * b.YZ - a.YZ * b.XY + a.ZW * b.XW,
                    a.XY * b.YW + a.XZ * b.ZW - a.YW * b.XY - a.ZW * b.XZ,
                    -a.XY * b.XZ + a.XZ * b.XY - a.YW * b.ZW + a.ZW * b.YW,
                    a.XW * b.XY - a.XY * b.XW + a.YZ * b.ZW - a.ZW * b.YZ,
                    a.XW * b.XZ - a.XZ * b.XW + a.YW * b.YZ - a.YZ * b.YW
                ),
                new Quadvector4(
                    a.XW * b.YZ
                    + a.XY * b.ZW
                    - a.XZ * b.YW
                    - a.YW * b.XZ
                    + a.YZ * b.XW
                    + a.ZW * b.XY
                )
            );
        }

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
        /// Decompose this bivector.
        /// </summary>
        /// <returns></returns>
        public (Bivector4, Bivector4) Decompose()
        {
            var positive = new Quadvector4(0.5f);
            var negative = new Quadvector4(-0.5f);

            var bPlus = 0.5f * this + (positive | this);
            var bMinus = 0.5f * this + (negative | this);

            return (bPlus, bMinus);
        }
    
        /// <summary>
        /// Convert this bivector into a rotor.
        /// </summary>
        /// <returns></returns>
        public Rotor4 Exp()
        {
            var (bPlus, bMinus) = Decompose();

            var thetaPlus = 2.0f
                * Mathf.Sqrt(Mathf.Pow(bPlus.XY, 2) + Mathf.Pow(bPlus.XZ, 2) + Mathf.Pow(bPlus.XW, 2));
            var thetaMinus = 2.0f
                * Mathf.Sqrt(Mathf.Pow(bMinus.XY, 2) + Mathf.Pow(bMinus.XZ, 2) + Mathf.Pow(bMinus.XW, 2));

            var invThetaPlus = 0.0f;
            if (thetaPlus > 0.0f)
            {
                invThetaPlus = 1.0f / thetaPlus;
            }
            var invThetaMinus = 0.0f;
            if (thetaMinus > 0.0f)
            {
                invThetaMinus = 1.0f / thetaMinus;
            }

            var bPlusNorm = invThetaPlus * bPlus;
            var bMinusNorm = invThetaMinus * bMinus;

            return new Rotor4(
                0.5f * Mathf.Cos(thetaPlus) + 0.5f * Mathf.Sin(thetaPlus),
                Mathf.Sin(thetaPlus) * bPlusNorm + Mathf.Sin(thetaMinus) * bMinusNorm,
                new Quadvector4(
                    0.5f * Mathf.Cos(thetaPlus) - 0.5f * Mathf.Sin(thetaPlus))
            );
        }
    }
}
