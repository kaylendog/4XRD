using UnityEngine;

using _4XRD.Transform;

namespace _4XRD.Physics.Colliders
{
    public class BoxCollider4D : StaticCollider4D
    {
        public override Vector4 ClosestPoint(Vector4 position)
        {
            Transform4D t = ToWorldSpace();
            Vector4 localPosition = t.ApplyInverse(position);
            Vector4 localClosest = GetLocalBounds().ClosestPointOnBounds(localPosition);
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
            BoundingBox4D bounds = GetLocalBounds();
            Vector4 closestPoint = bounds.ClosestPointOnBounds(localPosition);

            Vector4 localNormal;
            if (bounds.Includes(localPosition))
            {
                localNormal = (closestPoint - localPosition).normalized;
            }
            else
            {
                localNormal = (localPosition - closestPoint).normalized;
            }
            
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

        private BoundingBox4D GetLocalBounds()
        {
            return new BoundingBox4D(
                transform4D.scale.ElemMul(new Vector4(-1 / 2f, -1 / 2f, -1 / 2f, -1 / 2f)),
                transform4D.scale.ElemMul(new Vector4(1 / 2f, 1 / 2f, 1 / 2f, 1 / 2f))
            );
        }
    }
}
