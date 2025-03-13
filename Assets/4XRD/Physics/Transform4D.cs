using System;
using UnityEngine;

namespace _4XRD.Physics
{
    public class Transform4D : MonoBehaviour
    {
        public Vector4 position = Vector4.zero;
        public Vector4 scale = Vector4.one;
        public Rotor4 rotation = Rotor4.identity;

        void Update()
        {
            // propagate 4D transform to 3D
            transform.position = position.ToUnity();
            transform.localScale = scale.ToUnity();
        }
    }
}
