using UnityEngine;
using _4XRD.Mesh;
using _4XRD.Physics.Tensors;

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
        public Vector4 velocity = Vector4.zero;
            
        /// <summary>
        /// The angular velocity of this rigidbody.
        /// </summary>
        public Rotation6 angularVelocity = new();

        /// <summary>
        /// The mass of this body.
        /// </summary>
        public float mass = 1.0f;
       
        /// <summary>
        /// The center of mass of this body.
        /// </summary>
        public Vector4 centerOfMass = Vector4.zero;
        
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
            _transform.rotationMat *= Rotation4x4.FromAngles(angularVelocity * dt);
        }
    }
}
