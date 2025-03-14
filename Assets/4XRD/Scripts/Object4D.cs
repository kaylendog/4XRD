
using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(Transform4D))]
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {
        [SerializeField] PrimitiveType4D type;
        
        /// <summary>
        /// The mesh of this object.
        /// </summary>
        public Mesh4D mesh;
        
        /// <summary>
        /// This object's transform.
        /// </summary>
        Transform4D _transform4D;
        
        /// <summary>
        /// The global W slider.
        /// </summary>
        Slider _wSlider;
    
        MeshFilter _meshFilter;
    
        void OnEnable()
        {
            mesh = Mesh4D.CreatePrimitive(type);
            _transform4D = GetComponent<Transform4D>();
            _wSlider = GameObject.Find("W_Slider").GetComponent<Slider>();
            _meshFilter=  GetComponent<MeshFilter>();
        }

        void Update()
        {
            _meshFilter.mesh = mesh.GetSlice(_transform4D, _wSlider.value);
        }
    }
}
