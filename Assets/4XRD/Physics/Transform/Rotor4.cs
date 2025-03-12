using System;
using UnityEngine;

namespace _4XRD.Physics.Transform
{
    public class Rotor4
    {
        // scalar part
        public readonly float S;

        // bi-vector parts
        public readonly Bivector4 V;

        // 4-vector part
        public readonly float XYZW;

        Rotor4(float s = 1, float xy = 0, float xz = 0, float xw = 0, float yz = 0, float yw = 0, float zw = 0, float xyzw = 0)
        {
            S = s;
            V = new Bivector4(
                xy,
                xz,
                xw,
                yz,
                yw,
                zw
            );
            XYZW = xyzw;
        }

        static Rotor4 RotationBetween(Vector4 from, Vector4 to)
        {
            var rotor = new Rotor4();
            rotor.S = 1 + Vector4.Dot(to, from);
            rotor.V = Bivector4.Outer(to, from);
            Normalized();
            // the left side of the products have b a, not a b, so flip
        }

        float Length()
        {
            return S * S + V.Length();
        }
        
        Rotor4 Normalized()
        {
            float length = Mathf.Pow(S, 2);
        }
        
        Vector4 Rotate(Vector4 v)
        {
            throw new NotImplementedException();
        }
    }
}
