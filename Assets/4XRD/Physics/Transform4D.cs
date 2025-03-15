using UnityEngine;
using _4XRD.Physics.Tensors;

namespace _4XRD.Physics
{
    [ExecuteInEditMode]
    public class Transform4D : MonoBehaviour
    {
        public Vector4 position = Vector4.zero;
        public Vector4 scale = Vector4.one;
        public Rotation4x4 rotationMat = Rotation4x4.identity;

        void Update()
        {
            // propagate 4D transform to 3D
            transform.position = position;
            transform.localScale = scale;
        }
    }
}
