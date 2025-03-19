using UnityEngine;

using _4XRD.Transform;

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

        public override Vector4 ClosestPoint(Vector4 position)
        {
            return _projectedCollider.ClosestPoint(position);
        }

        public override Vector4 Normal(Vector4 position)
        {
            return (position - _projectedCollider.ClosestPoint(position).XYZW()).normalized;
        }
    }
}
