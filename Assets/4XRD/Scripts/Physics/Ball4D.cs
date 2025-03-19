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
        public Object4D object4D { get; private set; }

        /// <summary>
        /// The transform of this rigidbody.
        /// </summary>
        public Transform4D transform4D => object4D.transform4D;

        /// <summary>
        /// The linear velocity of this rigidbody.
        /// </summary>
        public Vector4 velocity = Vector4.zero;

        /// <summary>
        /// The radius of this body.
        /// </summary>
        public float radius = 1.0f;

        /// <summary>
        /// The mass of this body.
        /// </summary>
        public float mass = 1.0f;

        void Awake()
        {
            object4D = GetComponent<Object4D>();
        }

        void Update()
        {
            if (!transform4D.IsUniformScale())
            {
                Debug.LogWarning("Ball scale transform is not uniform.");
            }
            radius = Mathf.Min(
                transform4D.scale.x,
                transform4D.scale.y,
                transform4D.scale.z,
                transform4D.scale.w
            );
            mass = radius;
        }
    }
}
