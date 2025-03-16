using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// A collider that extends 3D meshes to 4D by assuming they fully occupy their 4D slices.
    /// </summary>
    [RequireComponent(typeof(MeshCollider))]
    public class ProjectedMeshCollider4D : StaticCollider4D
    {
        /// <summary>
        /// The internal collider.
        /// </summary>
        MeshCollider _projectedCollider;

        new void Awake()
        {
            _projectedCollider = GetComponent<MeshCollider>();
        }

        public override float SignedDistance(Vector4 position, float radius)
        {
            // assumes convex
            return (_projectedCollider.ClosestPoint(position.XYZ()) - position.XYZ()).magnitude - radius;
        }

        public override Vector4 Normal(Vector4 position)
        {
            return (_projectedCollider.ClosestPoint(position.XYZ()) - position.XYZ()).normalized.XYZW();
        }
    }
}
