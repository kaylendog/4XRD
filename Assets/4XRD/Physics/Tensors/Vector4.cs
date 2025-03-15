using System;
using UnityEngine;

namespace _4XRD.Physics.Tensors
{
    /// <summary>
    /// A 4D vector, or 1-blade.
    /// </summary>
    [Serializable]
    public record Vector4
    {
        [field: SerializeField]
        public float X { get; private set;}

        [field: SerializeField]
        public float Y { get; private set;}

        [field: SerializeField]
        public float Z { get; private set;}
        
        [field: SerializeField]
        public float W { get; private set;}

        /// <summary>
        /// The zero vector.
        /// </summary>
        public static Vector4 Zero => new();

        /// <summary>
        /// The one vector.
        /// </summary>
        public static Vector4 One => Splat(1);

        /// <summary>
        /// Positive Y.
        /// </summary>
        public static Vector4 Up => new(0, 1);

        /// <summary>
        /// Negative Z.
        /// </summary>
        public static Vector4 Forward => new(0, 0, -1);

        /// <summary>
        /// Positive X.
        /// </summary>
        public static Vector4 Right => new(1);

        /// <summary>
        /// Positive W.
        /// </summary>
        public static Vector4 Ana => new(0, 0, 0, 1);

        /// <summary>
        /// Construct a vector with all dimensions set to `v`.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 Splat(float v) => new(v, v, v, v);
    
        /// <summary>
        /// Linearly interpolate between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t) => new(
            Mathf.Lerp(a.X, b.X, t),
            Mathf.Lerp(a.Y, b.Y, t),
            Mathf.Lerp(a.Z, b.Z, t),
            Mathf.Lerp(a.W, b.W, t)
        );

        /// <summary>
        /// Vector unary, equivalent to the identity.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator +(Vector4 v) => v;

        /// <summary>
        /// Vector unary, equivalent to scaling by -1.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 v) => new(
            -v.X,
            -v.Y,
            -v.Z,
            -v.W
        );

        /// <summary>
        /// Vector addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4 operator +(Vector4 a, Vector4 b) => new(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z,
            a.W + b.W
        );
        
        /// <summary>
        /// Vector subtraction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 a, Vector4 b) => a + -b;

        /// <summary>
        /// Right multiplication by a scalar.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector4 operator *(Vector4 v, float f) => new(
            v.X * f,
            v.Y * f,
            v.Z * f,
            v.W * f
        );

        /// <summary>
        /// Left multiplication by a scalar.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator *(float f, Vector4 v) => v * f;
    

        /// <summary>
        /// Inner product with a vector.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float operator |(Vector4 a, Vector4 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
            
        /// <summary>
        /// Inner product with a bivector.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4 operator |(Vector4 v, Bivector4 b) => new(
            -(b.XY * v.Y + b.XZ * v.Z + b.XW * v.W),
            b.XY * v.X - b.YZ * v.Z - b.YW * v.W,
            b.XZ * v.X + b.YZ * v.Y - b.ZW * v.W,
            b.XW * v.X + b.YW * v.Y + b.ZW * v.Z
        );

        /// <summary>
        /// Outer product with another vector.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bivector4 operator ^(Vector4 u, Vector4 v) => new(
            u.X * v.Y - u.Y * v.X,
            u.X * v.Z - u.Z * v.X,
            u.X * v.W - u.W * v.X,
            u.Y * v.Z - u.Z * v.Y,
            u.Y * v.W - u.W * v.Y,
            u.Z * v.W - u.W * v.Z
        );
        
        /// <summary>
        /// Outer product with a bivector.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Trivector4 operator ^(Vector4 u, Bivector4 b) => new(
            u.X * b.YZ - u.Y * b.XZ + u.Z * b.XY,
            u.X * b.YW - u.Y * b.XW + u.W * b.XY,
            u.X * b.ZW - u.Z * b.XW + u.W * b.XZ,
            u.X * b.ZW - u.Z * b.YW + u.W * b.YZ
        );

        /// <summary>
        /// Compute the geometric product of two vectors.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static (float S, Bivector4 B) operator *(Vector4 u, Vector4 v) => (
            u | v,
            u ^ v
        );
        
        /// <summary>
        /// Geometric product of a vector with a bivector
        /// </summary>
        /// <param name="u"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static (Vector4 S, Trivector4 B) operator *(Vector4 u, Bivector4 b) => (
            u | b,
            u ^ b
        );

        /// <summary>
        /// Construct a Vector4 from a Unity Vector4.
        /// </summary>
        /// <param name="v"></param>
        public Vector4(UnityEngine.Vector4 v)
        {
            X = v.x;
            Y = v.y;
            Z = v.z;
            W = v.w;
        }

        /// <summary>
        /// Create a vector component by component.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Converts this vector to a Unity Vector4.
        /// </summary>
        /// <returns></returns>
        public UnityEngine.Vector4 ToUnity() => new(X, Y, Z, W);
    }
}
