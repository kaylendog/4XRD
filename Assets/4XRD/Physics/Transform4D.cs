using UnityEngine;

namespace _4XRD.Physics
{
    public class Transform4D : MonoBehaviour
    {
        public Vector4 position = Vector4.zero;
        public Vector4 scale = new Vector4(1, 1, 1);
        public Rotation4D rotation = new Rotation4D();

        public void Update()
        
        {
            UpdateRotationMatrix();
        }
    
        void RotateXY() {}
        
        /// <summary>
        /// Updates the rotation matrix from the Euler4.
        /// </summary>
        void UpdateRotationMatrix()
        {
            RotationMatrix =
                Matrix4x4.identity
                    .RotateXY(Rotation.XY * Mathf.Deg2Rad)
                    .RotateYZ(Rotation.YZ * Mathf.Deg2Rad)
                    .RotateXZ(Rotation.XZ * Mathf.Deg2Rad)
                    .RotateXW(Rotation.XW * Mathf.Deg2Rad)
                    .RotateYW(Rotation.YW * Mathf.Deg2Rad)
                    .RotateZW(Rotation.ZW * Mathf.Deg2Rad);
        }

        public Vector4 Transform(Vector4 v)
        {
            v = _rotMatrix * v;
            v.x *= scale.x;
            v.y *= scale.y;
            v.z *= scale.z;
            v.w *= scale.w;
            v += position;
            return v;
        }
    }
    
}
