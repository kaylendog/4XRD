using _4XRD.Mesh;
using UnityEngine;

namespace _4XRD.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshFilter4D))]
    [ExecuteInEditMode]
    public class MeshObject4D : Object4D
    {
        /// <summary>
        /// The slicing constant.
        /// </summary>
        public static float SlicingConstant;
        
        /// <summary>
        /// The mesh filter.
        /// </summary>
        MeshFilter _meshFilter;
    
        /// <summary>
        /// The 4D mesh filter.
        /// </summary>
        MeshFilter4D _meshFilter4D;

        /// <summary>
        /// The mesh this object is using.
        /// </summary>
        public Mesh4D mesh => _meshFilter4D.Mesh;

        public override void Awake()
        {
            base.Awake();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter4D = GetComponent<MeshFilter4D>();
        }

        public override void Update()
        {
            base.Update();
            _meshFilter.mesh = mesh.GetSlice(transform4D, SlicingConstant);
        }
    }
}
