using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// A half-space collider
    /// </summary>
    public class HalfSpaceCollider4D : Collider4D
    {
        public Vector4 normal => transform4D.rotation * Vector4.up;
        
        public override float SignedDistance(Vector4 point)
        {
            return normal.Dot(point - position);
        }
    }
}
