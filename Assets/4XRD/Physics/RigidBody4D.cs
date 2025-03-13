using _4XRD.Mesh;
using UnityEngine;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Transform4D), typeof(MeshFilter4D))]
    public class RigidBody4D : MonoBehaviour
    {
        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        Transform4D _transform;
    
        /// <summary>
        /// The mesh associated with this rigidbody.
        /// </summary>
        MeshFilter4D _meshFilter;

        /// <summary>
        /// The bounding box of the mesh.
        /// </summary>
        BoundingBox4D _bounds;
        
        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.Zero;
            
        /// <summary>
        /// The angular velocity of this rigidbody.
        /// </summary>
        public Bivector4 angularVelocity = Bivector4.Zero;

        /// <summary>
        /// The mass of this body.
        /// </summary>
        public float mass = 1.0f;
       
        /// <summary>
        /// The center of mass of this body.
        /// </summary>
        public Vector4 centerOfMass = Vector4.Zero;
        
        void OnEnable()
        {
            _transform = GetComponent<Transform4D>();
            _meshFilter = GetComponent<MeshFilter4D>();
            _bounds = BoundingBox4D.FromMesh(_meshFilter.Mesh);
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            _transform.position += velocity * dt;
            _transform.rotation *= 0.5f * angularVelocity * dt;
            _transform.rotation = _transform.rotation.Normalized();
        }
    }
}
