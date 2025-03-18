using UnityEngine;

using _4XRD.Transform;

namespace _4XRD.Physics
{
    [RequireComponent(typeof(Object4D))]
    public class Ball4D : MonoBehaviour
    {
        /// <summary>
        /// The object this body is attached to.
        /// </summary>
        public Object4D object4D
        {
            get => _object4D;
        }

        Object4D _object4D;

        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        public Transform4D transform4D
        {
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

        void Awake()
        {
            _object4D = GetComponent<Object4D>();
        }
    }
}
