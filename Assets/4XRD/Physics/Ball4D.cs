using _4XRD.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Object4D))]
    public class Ball4D : MonoBehaviour
    {
        /// <summary>
        /// The object this body is attached to.
        /// </summary>
        public Object4D object4D;
        
        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        public Transform4D transform4D { 
            get => object4D.transform4D;
            set => object4D.transform4D = value;
        }
        
        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.zero;
        
        /// <summary>
        ///     The radius of this body.
        /// </summary>
        public float radius = 1.0f;

        /// <summary>
        /// The mass of this body.
        /// </summary>
        public float mass = 1.0f;
        
        /// <summary>
        /// Whether or not this body is static.
        /// </summary>
        public bool isStatic = false;
        
        void Awake()
        {
            object4D = GetComponent<Object4D>();
        }

        void Update()
        {
            isStatic = object4D.isStatic;
        }
    }
}
