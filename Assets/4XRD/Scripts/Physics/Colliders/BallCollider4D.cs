using UnityEngine;

using _4XRD.Transform;

namespace _4XRD.Physics.Colliders
{
    public class BallCollider4D : StaticCollider4D
    {
        public float radius = 1.0f;
        
        public override Vector4 ClosestPoint(Vector4 position)
        {
            Transform4D t = ToWorldSpace();
            Vector4 localPosition = t.ApplyInverse(position);
            Vector4 localClosest = localPosition.normalized * radius;
            return t.Apply(localClosest);
        }

        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override Vector4 Normal(Vector4 position)
        {
            Transform4D t = ToWorldSpace();
            Vector4 localPosition = t.ApplyInverse(position);
            Vector4 localNormal = localPosition.normalized;
            return transform4D.GetRotation() * localNormal;
        }

        private Transform4D ToWorldSpace()
        {
            return new Transform4D(
                transform4D.position,
                Vector4.one,
                transform4D.eulerAngles
            );
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
        }
    }
}
