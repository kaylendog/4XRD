using UnityEngine;
using UnityEngine.Android;

namespace _4XRD.Physics
{
    
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
        public Vector4 angularVelocity = Vector4.zero;
        
        void OnEnable()
        {
            _transform4D = GetComponent<Transform4D>();
        }
    }
}
