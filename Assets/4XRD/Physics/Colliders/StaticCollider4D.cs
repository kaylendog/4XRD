using _4XRD.Scripts;
using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// An arbitrary 4D collider.
    /// </summary>
    [RequireComponent(typeof(Object4D))]
    public abstract class StaticCollider4D : MonoBehaviour 
    {
        /// <summary>
        /// A hit between two or more colliders.
        /// </summary>
        public struct Hit
        {
            /// <summary>
            /// The displacement from the objects' origin.
            /// </summary>
            public Vector4 displacement;
            
            /// <summary>
            /// The normal to the surface.
            /// </summary>
            public Vector4 normal;

            /// <summary>
            /// The collider that was hit.
            /// </summary>
            public StaticCollider4D StaticCollider;
        }

        /// <summary>
        /// Internal reference to the 4D object.
        /// </summary>
        protected Object4D Object4D;

        /// <summary>
        /// Internal reference to the 4D transform.
        /// </summary>
        public Transform4D transform4D => Object4D.transform4D;
        
        protected virtual void Awake()
        {
            Object4D = GetComponent<Object4D>();
        }

        /// <summary>
        /// Test whether the given point in world-space collides with this object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="hit"></param>
        /// <returns></returns>
        public bool Collide(
            Vector4 position,
            float radius,
            ref Hit hit
        )
        {
            return false;
        }
        
        /// <summary>
        /// Compute the signed distance between this collider and a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract float SignedDistance(Vector4 position);
        
        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract Vector4 Normal(Vector4 position);
    }
}
