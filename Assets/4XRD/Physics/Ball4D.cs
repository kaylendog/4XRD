using _4XRD.Scripts;
using UnityEngine;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Object4D))]
    public class Ball4D : MonoBehaviour
    {
        /// <summary>
        /// The object this body is attached to.
        /// </summary>
        Object4D _object4D;
        
        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        Transform4D _transform4D => _object4D.transform4D;
        
        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.zero;
        
        /// <summary>
        ///     The radius of this body.
        /// </summary>
        public float radius = 1.0f;
        
        void Awake()
        {
            _object4D = GetComponent<Object4D>();
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            _object4D.transform4D.position += velocity * dt;
        }
    }
}
