using _4XRD.Physics.Transform;
using UnityEngine;

namespace _4XRD.Physics
{
    public class Transform4D : MonoBehaviour
    {
        public Vector4 position = Vector4.zero;
        public Vector4 scale = new(1, 1, 1);
        public Rotor4 rotation = new();

        public void Update() {}
    
        void RotateXY() {}
        
        public Vector4 Transform(Vector4 v)
        {
            v = rotation.RotateVector(v);
            v.x *= scale.x;
            v.y *= scale.y;
            v.z *= scale.z;
            v.w *= scale.w;
            v += position;
            return v;
        }
    }
    
}
