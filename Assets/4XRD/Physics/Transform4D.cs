using System;
using UnityEngine;

namespace _4XRD.Physics
{
    [ExecuteInEditMode]
    public class Transform4D : MonoBehaviour
    {
        public Vector4 position = Vector4.Zero;
        public Vector4 scale = Vector4.One;
        public Rotor4 rotation = Rotor4.Identity;

        void Update()
        {
            // propagate 4D transform to 3D
            transform.position = position.ToUnity();
            transform.localScale = scale.ToUnity();
        }
    }
}
