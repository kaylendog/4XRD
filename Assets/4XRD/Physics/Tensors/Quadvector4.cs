// using System;
// using UnityEngine;

// namespace _4XRD.Physics.Tensors
// {
//     /// <summary>
//     /// A quadvector, or 4-blade.
//     /// </summary>
//     [Serializable]
//     public struct Quadvector4
//     {
//         [field: SerializeField] public float XYZW { get; private set;}

//         /// <summary>
//         /// The zero quadvector.
//         /// </summary>
//         public static Quadvector4 Zero{ get => new(); }

//         /// <summary>
//         /// Quadvector negation.
//         /// </summary>
//         public static Quadvector4 operator -(Quadvector4 a) => new( -a.XYZW );

//         /// <summary>
//         /// Quadvector addition.
//         /// </summary>
//         /// <param name="a"></param>
//         /// <param name="b"></param>
//         /// <returns></returns>
//         public static Quadvector4 operator +(Quadvector4 a, Quadvector4 b) => new(
//             a.XYZW + b.XYZW
//         );

//         /// <summary>
//         /// Quadvector subtraction.
//         /// </summary>
//         /// <param name="a"></param>
//         /// <param name="b"></param>
//         /// <returns></returns>
//         public static Quadvector4 operator -(Quadvector4 a, Quadvector4 b) => a + -b;

//         /// <summary>
//         /// Right multiplication by a scalar.
//         /// </summary>
//         /// <param name="v"></param>
//         /// <param name="f"></param>
//         /// <returns></returns>
//         public static Quadvector4 operator *(Quadvector4 v, float f) => new(
//             v.XYZW * f
//         );

//         /// <summary>
//         /// Left multiplication by a scalar.
//         /// </summary>
//         /// <param name="f"></param>
//         /// <param name="v"></param>
//         /// <returns></returns>
//         public static Quadvector4 operator *(float f, Quadvector4 v) => v * f;

//         // /// <summary>
//         // /// Inner product of a quadvector with a vector
//         // /// </summary>
//         // /// <param name="q"></param>
//         // /// <param name="b"></param>
//         // /// <returns></returns>
//         // public static Trivector4 operator |(Quadvector4 q, Vector4 v) => new Trivector4(
//         //     q.XYZW * v.W,
//         //     -q.XYZW * v.Z,
//         //     q.XYZW * v.Y,
//         //     -q.XYZW * v.X
//         // );

//         /// <summary>
//         /// Inner product of a quadvector with a bivector
//         /// </summary>
//         /// <param name="q"></param>
//         /// <param name="b"></param>
//         /// <returns></returns>
//         public static Bivector4 operator |(Quadvector4 q, Bivector4 b) => new(
//             -b.ZW * q.XYZW,
//             b.YW * q.XYZW,
//             -b.YZ * q.XYZW,
//             -b.XW * q.XYZW,
//             b.XZ * q.XYZW,
//             -b.XY * q.XYZW
//         );
//         // public static Bivector4 operator |(Quadvector4 q, Bivector4 b) => new(
//         //     b.ZW * q.XYZW,
//         //     b.YW * q.XYZW,
//         //     b.YZ * q.XYZW,
//         //     b.XW * q.XYZW,
//         //     b.XZ * q.XYZW,
//         //     b.XY * q.XYZW
//         // );

//         /// <summary>
//         /// Construct a new quadvector from its element.
//         /// </summary>
//         /// <param name="xyzw"></param>
//         public Quadvector4(float xyzw = 0.0f)
//         {
//             XYZW = xyzw;
//         }
//     }
// }
