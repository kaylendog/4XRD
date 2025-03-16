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
        /// Internal reference to the 4D object.
        /// </summary>
        protected Object4D Object4D;

        /// <summary>
        /// Internal reference to the 4D transform.
        /// </summary>
        public Transform4D transform4D => Object4D.transform4D;

        /// <summary>
        /// The restitution of this surface.
        /// </summary>
        public float restitution = 0.6f;

        /// <summary>
        /// The friction of this surface.
        /// </summary>
        public float friction = 0.95f;
        
        protected virtual void Awake()
        {
            Object4D = GetComponent<Object4D>();
        }
        
        /// <summary>
        /// Compute the signed distance between this collider and a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public abstract float SignedDistance(Vector4 position, float radius);
        
        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public abstract Vector4 Normal(Vector4 position, float radius);
    }
}
