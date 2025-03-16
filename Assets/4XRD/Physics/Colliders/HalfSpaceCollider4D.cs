
using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// A half-space collider
    /// </summary>
    public class HalfSpaceCollider4D : StaticCollider4D
    {
        public Vector4 normal => transform4D.rotation * new Vector4(0, 1, 0, 0);

        public override float SignedDistance(Vector4 point, float radius)
        {
            return normal.Dot(point - transform4D.position) - radius;
        }

        public override Vector4 Normal(Vector4 position)
        {
            return normal;
        }
    }
}
