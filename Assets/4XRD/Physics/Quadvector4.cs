namespace _4XRD.Physics
{
    /// <summary>
    /// A quadvector, or 4-blade.
    /// </summary>
    public struct Quadvector4
    {
        public readonly float XYZW;

        /// <summary>
        /// The zero quadvector.
        /// </summary>
        public static Quadvector4 zero
        {
            get => new Quadvector4();
        }

        /// <summary>
        /// Inner product of a quadvector with a vector
        /// </summary>
        /// <param name="q"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Trivector4 operator |(Quadvector4 q, Vector4 v) => new Trivector4(
            q.XYZW * v.W,
            -q.XYZW * v.Z,
            q.XYZW * v.Y,
            -q.XYZW * v.X
        );

        /// <summary>
        /// Inner product of a quadvector with a bivector
        /// </summary>
        /// <param name="q"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bivector4 operator |(Quadvector4 q, Bivector4 b) => new Bivector4(
            -b.ZW * q.XYZW,
            b.YW * q.XYZW,
            -b.YZ * q.XYZW,
            -b.XW * q.XYZW,
            b.XZ * q.XYZW,
            -b.XY * q.XYZW
        );

        /// <summary>
        /// Construct a new quadvector from its element.
        /// </summary>
        /// <param name="xyzw"></param>
        public Quadvector4(float xyzw = 0.0f)
        {
            XYZW = xyzw;
        }


    }
}
