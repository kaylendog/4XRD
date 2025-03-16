using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// A half-space collider
    /// </summary>
    public class HalfSpaceCollider4D : StaticCollider4D
    {
        public Vector4 normal => transform4D.rotation * new Vector4(0, 1, 0, 0);
        
        public override float SignedDistance(Vector4 point)
        {
            return 0;
            // return normal.Dot(point - position);
        }

        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override Vector4 Normal(Vector4 position)
        {
            return Vector4.zero;
        }
    }
}
