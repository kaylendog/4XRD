using UnityEngine;

namespace _4XRD.Physics
{
    
    public class RigidBody4D : MonoBehaviour
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
        public Vector4 angularVelocity = Vector4.zero;
        
        void Start()
        {
            _transform = GetComponent<Transform4D>();
        }
    }
}
