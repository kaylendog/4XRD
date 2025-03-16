using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshFilter4D))]
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {
        /// <summary>
        /// The mesh filter.
        /// </summary>
        MeshFilter _meshFilter;

        /// <summary>
        /// The 4D mesh filter.
        /// </summary>
        MeshFilter4D _meshFilter4D;

        /// <summary>
        /// The global W slider.
        /// </summary>
        Slider _wSlider;

        /// <summary>
        /// This object's transform.
        /// </summary>
        public Transform4D transform4D = Transform4D.identity;

        /// <summary>
        /// Whether object is static.
        /// </summary>
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
