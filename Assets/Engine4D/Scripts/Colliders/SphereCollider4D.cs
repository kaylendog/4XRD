//#########[---------------------------]#########
//#########[  GENERATED FROM TEMPLATE  ]#########
//#########[---------------------------]#########

using UnityEngine;

namespace Engine4D.Scripts.Colliders
{
    public class SphereCollider4D : Collider4D {
        public Vector4 center = Vector4.zero;
        public float radius = 1.0f;

        protected override void Awake() {
            base.Awake();
            Vector4 size = radius * Vector4.one;
            aabbMin = center - size;
            aabbMax = center + size;
        }

        public override Vector4 NP(Vector4 localPt) {
            return center + radius * (localPt - center).normalized;
        }
    }
}
