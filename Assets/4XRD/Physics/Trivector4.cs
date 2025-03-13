namespace _4XRD.Physics
{
    /// <summary>
    /// A trivector, or 4-vector.
    /// </summary>
    public record Trivector4
    {
        public readonly float XYZ, XYW, XZW, YZW;

        /// <summary>
        /// The zero trivector.
        /// </summary>
        public static Trivector4 zero
        {
            get => new();
        }

        /// <summary>
        /// Bivector unary, equivalent to the identity.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Trivector4 operator +(Trivector4 v) => v;

        /// <summary>
        /// Component-wise negation.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Trivector4 operator -(Trivector4 v) => new(
            -v.XYZ,
            -v.XYW,
            -v.XZW,
            -v.YZW
        );

        /// <summary>
        /// Bivector addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Trivector4 operator +(Trivector4 a, Trivector4 b) => new(
            a.XYZ + b .XYZ,
            a.XYW + b .XYW,
            a.XZW + b .XZW,
            a.YZW + b .YZW
        );

        /// <summary>
        /// Bivector subtraction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Trivector4 operator -(Trivector4 a, Trivector4 b) => a + -b;

        /// <summary>
        /// Right multiplication by a scalar.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Trivector4 operator *(Trivector4 v, float f) => new(
            v.XYZ * f,
            v.XYW * f,
            v.XZW * f,
            v.YZW * f
        );

        /// <summary>
        /// Left multiplication by a scalar.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Trivector4 operator *(float f, Trivector4 v) => v * f;
    
        /// <summary>
        /// Inner product with a quadvector.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Vector4 operator |(Trivector4 t, Quadvector4 q) => new(
            q.XYZW * t.YZW,
            -q.XYZW * t.XZW,
            q.XYZW * t.XYW,
            -q.XYZW * t.XYZ
        );

        public Trivector4(float xyz = 0, float xyw = 0, float xzw = 0, float yzw = 0)
        {
            XYZ = xyz;
            XYW = xyw;
            XZW = xzw;
            YZW = yzw;
        }
    }
}
