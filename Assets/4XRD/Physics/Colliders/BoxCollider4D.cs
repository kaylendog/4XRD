using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    public class BoxCollider4D : StaticCollider4D
    {
        public override float SignedDistance(Vector4 position, float radius)
        {
            var closestPointOnSphere = (transform4D.position - position).normalized * radius + position;
            var localPosition = transform4D.inverse * closestPointOnSphere;
            
            return Mathf.Max(
                Mathf.Abs(localPosition.x),
                Mathf.Abs(localPosition.y),
                Mathf.Abs(localPosition.z),
                Mathf.Abs(localPosition.w)
            ) - 1f / 2;
        }
        
        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override Vector4 Normal(Vector4 position)
        {
            return Vector4.zero;
        }
    }
}
