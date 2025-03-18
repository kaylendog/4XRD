using Unity.Profiling;
using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    public class BoxCollider4D : StaticCollider4D
    {
        protected override Vector4 LocalClosestPoint(Vector4 position)
        {
            return new Vector4(
                Mathf.Clamp(position.x, -1 / 2f, 1 / 2f),
                Mathf.Clamp(position.y, -1 / 2f, 1 / 2f),
                Mathf.Clamp(position.z, -1 / 2f, 1 / 2f),
                Mathf.Clamp(position.w, -1 / 2f, 1 / 2f)
            );
        }

        /// <summary>
        /// The normal to the surface at a given point.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected override Vector4 LocalNormal(Vector4 position)
        {
            var normal = Vector4.zero;

            normal.x = Mathf.Abs(position.x) > 1 / 2f ? Mathf.Sign(position.x) : 0f;
            normal.y = Mathf.Abs(position.y) > 1 / 2f ? Mathf.Sign(position.y) : 0f;
            normal.z = Mathf.Abs(position.z) > 1 / 2f ? Mathf.Sign(position.z) : 0f;
            normal.w = Mathf.Abs(position.w) > 1 / 2f ? Mathf.Sign(position.w) : 0f;

            return normal.normalized;
        }
    }
}
