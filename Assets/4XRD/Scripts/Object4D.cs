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
        /// The mesh this object is using.
        /// </summary>
        public Mesh4D mesh => _meshFilter4D.Mesh;
        
        void OnEnable()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter4D=  GetComponent<MeshFilter4D>();
            _wSlider = GameObject.Find("W_Slider").GetComponent<Slider>();
        }

        void Update()
        {
            _meshFilter.mesh = mesh.GetSlice(transform4D, _wSlider.value);
        }
    }
}
