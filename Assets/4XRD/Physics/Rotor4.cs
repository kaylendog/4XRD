using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

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
        public static Rotor4 identity
        {
            get => new Rotor4();
        }

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
                Quadvector4.zero
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

        public static Rotor4 operator *(Rotor4 r, Vector4 v)
        {
            var (a1, a3) = r.B * v;
            var b3 = r.Q | v;
            var q1 = r.S * v + a1;
        }
        
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
        /// Construct a zero rotor.
        /// </summary>
        public Rotor4() {}

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

        Rotor4 Normalized()
        {
            throw new NotImplementedException();
        }

        Vector4 Rotate(Vector4 v)
        {
            throw new NotImplementedException();
        }
    }
}
