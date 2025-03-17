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

        void Update()
        {
            if (!isStatic)
            {
                transform.position = transform4D.position;
                transform.localScale = transform4D.scale;
            }
        }
    }
}
