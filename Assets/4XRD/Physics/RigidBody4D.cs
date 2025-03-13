using System;
using UnityEngine;
using UnityEngine.Android;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Transform4D))]
    public class RigidBody4D : MonoBehaviour
    {
        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        private Transform4D _transform4D;
    
        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.zero;
            
        /// <summary>
        /// The angular velocity of this rigidbody.
        /// </summary>
        public Bivector4 angularVelocity = Bivector4.zero;

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
            _transform4D = GetComponent<Transform4D>();
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            _transform.position = velocity * dt;
            _transform.rotation *= (0.5f * angularVelocity * dt);
            _transform.rotation = _transform.rotation.Normalized();
        }
    }
}
