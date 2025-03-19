using UnityEngine;

using _4XRD.Transform;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// A half-space collider
    /// </summary>
    public class HalfSpaceCollider4D : StaticCollider4D
    {
        // public Vector4 normal => transform4D.rotation * new Vector4(0, 1, 0, 0);
        public Vector4 normal => transform4D.GetRotation() * new Vector4(0, 1, 0, 0);

        public override Vector4 ClosestPoint(Vector4 position)
        {
            return normal.Dot(transform4D.position - position) * normal + position;
        }

        public override Vector4 Normal(Vector4 position)
        {
            return normal;
        }
    }
}
