using System.Linq;
using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(RigidBody4D))]
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {
        /// <summary>
        /// The mesh filter.
        /// </summary>
        MeshFilter _meshFilter;

        /// <summary>
        /// This object's transform.
        /// </summary>
        Transform4D _transform4D;

        /// <summary>
        /// The 4D mesh filter.
        /// </summary>
        MeshFilter4D _meshFilter4D;
        
        /// <summary>
        /// The global W slider.
        /// </summary>
        Slider _wSlider;
    
        void OnEnable()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _transform4D = GetComponent<Transform4D>();
            _meshFilter4D=  GetComponent<MeshFilter4D>();
            _wSlider = GameObject.Find("W_Slider").GetComponent<Slider>();
        }

        void Update()
        {
            _meshFilter.mesh = _meshFilter4D.Mesh.GetSlice(_transform4D, _wSlider.value);
        }
    }
}
