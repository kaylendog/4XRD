using System;
using UnityEngine;

namespace _4XRD.Physics
{
    public record Rotor4
    {
        public readonly float S = 1.0f;
        public readonly Bivector4 B;
        public readonly Quadvector4 Q;

        /// <summary>
        /// The identity rotor.
        /// </summary>
        public static Rotor4 Identity { get => new(); }

        /// <summary>
        /// Return a rotation between two vectors.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        static Rotor4 RotationBetween(Vector4 from, Vector4 to)
        {
            var rotor = new Rotor4(
                1 + (to | from),
                to ^ from,
                Quadvector4.Zero
            );
            return rotor.Normalized();
        }

        /// <summary>
        /// Rotor negation.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rotor4 operator -(Rotor4 r)
        {
            return new Rotor4(
                r.S,
                -r.B,
                r.Q
            );
        }

        /// <summary>
        /// Vector rotation.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator *(Rotor4 r, Vector4 v)
        {
            var (a1, a3) = r.B * v;
            var b3 = r.Q | v;
            var q1 = r.S * v + a1;
            var q3 = a3 + b3;
            return r.S * q1 + (q1 | -r.B) + (-r.B | q3) + (q3 | r.Q);
        }

        /// <summary>
        /// Product with a bivector.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rotor4 operator *(Rotor4 r, Bivector4 b)
        {
            var (s, bPrime, q) = r.B * b;
            return new Rotor4(
                s,
                r.S * b + bPrime + (r.Q | b),
                q
            );
        }
        
        /// <summary>
        /// Product with another rotor.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rotor4 operator *(Rotor4 a, Rotor4 b)
        {
            var (a0, a2, a4) = a.B * b.B;
            return new Rotor4(
                a.S * b.S + a0 + a.Q.XYZW * b.Q.XYZW,
                a.S * b.B + b.S * a.B + a2 + (a.Q | b.B) + (b.Q | a.B),
                a.S * b.Q + b.S * a.Q + a4
            );
        }

        /// <summary>
        /// Construct a zero rotor.
        /// </summary>
        public Rotor4()
        {
            S = 1.0f;
            B = Bivector4.Zero;
            Q = Quadvector4.Zero;
        }

        /// <summary>
        /// Construct a rotor with the given components.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        /// <param name="q"></param>
        public Rotor4(float s, Bivector4 b, Quadvector4 q)
        {
            S = s;
            B = b;
            Q = q;
        }

        /// <summary>
        /// Normalize this rotor.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArithmeticException"></exception>
        public Rotor4 Normalized()
        {
            var (rPlus, rMinus) = Decompose();
            var rPlusNorm = new Rotor4(rPlus.S - 0.5f, rPlus.B, rPlus.Q);
            var rMinusNorm = new Rotor4(rMinus.S - 0.5f, rMinus.B, rMinus.Q);

            var plusMag = 2.0f * Mathf.Sqrt(
                Mathf.Pow(rPlus.S, 2) + Mathf.Pow(rPlus.B.XY, 2) + Mathf.Pow(rPlus.B.XZ, 2) + Mathf.Pow(rPlus.B.XW, 2)
            );
            var minusMag = 2.0f * Mathf.Sqrt(
                Mathf.Pow(rPlus.S, 2) + Mathf.Pow(rPlus.B.XY, 2) + Mathf.Pow(rPlus.B.XZ, 2) + Mathf.Pow(rPlus.B.XW, 2)
            );

            if (plusMag == 0 || minusMag == 0)
            {
                throw new ArithmeticException();
            }
            
            var invPlusMag = 1.0f / plusMag;
            var invMinusMag = 1.0f / minusMag;
            
            var invPlus = new Rotor4(
                rPlus.S * invPlusMag, rPlus.B * invPlusMag, new Quadvector4(rPlus.S)
                );
            
            var invMinus = new Rotor4(
                rMinus.S * invMinusMag, rMinus.B * invMinusMag, new Quadvector4(-rMinus.S)
            );
            
            return invPlus * invMinus;
        }

        /// <summary>
        /// Decompose this rotation into two isoclinic rotations.
        /// </summary>
        /// <returns></returns>
        public (Rotor4, Rotor4) Decompose()
        {
            var posHalf = new Quadvector4(0.5f);
            var negHalf = new Quadvector4(-0.5f);

            return (
                new Rotor4(0.5f + 0.5f * S + 0.5f * Q.XYZW, 0.5f * B + (posHalf | B), 0.5f * Q + S * posHalf + negHalf),
                new Rotor4(0.5f + 0.5f * S - 0.5f * Q.XYZW, 0.5f * B + (posHalf | B), 0.5f * Q + S * posHalf + negHalf)
            );
        }

        /// <summary>
        /// Convert this rotor to a Unity Matrix4x4.
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 ToUnity()
        {
            var x = this * Vector4.Right;
            var y = this * Vector4.Up;
            var z = this * -Vector4.Forward;
            var w = this * Vector4.Ana;
            return new Matrix4x4(x.ToUnity(), y.ToUnity(), z.ToUnity(), w.ToUnity());
        }
    }
}
