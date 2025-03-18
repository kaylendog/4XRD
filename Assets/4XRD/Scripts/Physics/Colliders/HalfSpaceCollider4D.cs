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
        public Vector4 normal => new(0, 1, 0, 0);

        protected override Vector4 LocalClosestPoint(Vector4 position)
        {
            return normal.Dot(position - transform4D.position) * normal;
        }

        protected override Vector4 LocalNormal(Vector4 position)
        {
            return normal;
        }
    }
}
