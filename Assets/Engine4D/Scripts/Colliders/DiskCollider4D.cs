//#########[---------------------------]#########
//#########[  GENERATED FROM TEMPLATE  ]#########
//#########[---------------------------]#########

using UnityEngine;

namespace Engine4D.Scripts.Colliders
{
    public class DiskCollider4D : Collider4D {
        public Vector4 center = Vector4.zero;
        public float radius = 1.0f;

        protected override void Awake() {
            base.Awake();
            Vector4 size = (Vector4)(Vector3.one * radius);
            aabbMin = center - size;
            aabbMax = center + size;
        }

        public override Vector4 NP(Vector4 localPt) {
            Vector3 d = (Vector3)(localPt - center);
            return center + (Vector4)(d * (radius / Mathf.Max(radius, d.magnitude)));
        }
    }
}
