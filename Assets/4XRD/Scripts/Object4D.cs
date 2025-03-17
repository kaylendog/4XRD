using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.Scripts
{
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {
        /// <summary>
        /// This object's transform.
        /// </summary>
        public Transform4D transform4D = Transform4D.identity;

        public bool isStatic = false;

        public virtual void Update()
        {
            if (isStatic)
            {
                transform4D.position = transform.position;
                transform4D.rotation = Rotation4x4.FromAngles(Euler6.FromEuler3(transform.rotation.eulerAngles));
                transform4D.scale = transform.localScale;
            }
            {
                transform.position = transform4D.position;
                transform.localScale = transform4D.scale;
            }
        }
    }
}
