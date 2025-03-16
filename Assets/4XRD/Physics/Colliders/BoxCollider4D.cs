using UnityEngine;

namespace _4XRD.Physics.Colliders
{
    public class BoxCollider4D : StaticCollider4D
    {
        public override float SignedDistance(Vector4 position)
        {
            var localPosition = transform4D.inverse * position;
            return Mathf.Max(
                Mathf.Abs(localPosition.x),
                Mathf.Abs(localPosition.y),
                Mathf.Abs(localPosition.z),
                Mathf.Abs(localPosition.w)
            ) - 1f / 2;
        }
    }
}
