using _4XRD.Mesh;
using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshFilter4D))]
    [ExecuteInEditMode]
    public class MeshObject4D : Object4D
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
        /// The slicing constant.
        /// </summary>
        public static float SlicingConstant = 0.0f;

        /// <summary>
        /// The mesh this object is using.
        /// </summary>
        public Mesh4D mesh => _meshFilter4D.Mesh;

        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter4D = GetComponent<MeshFilter4D>();
        }

        void Update()
        {
            _meshFilter.mesh = mesh.GetSlice(transform4D, SlicingConstant);
            transform.position = transform4D.position;
            transform.localScale = transform4D.scale;
        }
    }
}
