using UnityEngine;
using _4XRD.Scripts;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Object4D))]
    public class SphericalPhysics4D : MonoBehaviour
    {
        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        Transform4D _transform;
        
        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.zero;
            
        /// <summary>
        /// The angular velocity of this rigidbody.
        /// </summary>
        public Euler6 angularVelocity = new();

        /// <summary>
        /// The mass of this body.
        /// </summary>
        public float mass = 1.0f;
        
        void OnEnable()
        {
            _transform = GetComponent<Transform4D>();
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            _transform.position += velocity * dt;
            _transform.rotation *= Rotation4x4.FromAngles(angularVelocity * dt);
        }
    }
}
