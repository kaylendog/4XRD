using System.Diagnostics;
using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(RigidBody4D))]
[ExecuteInEditMode]
public class Object4D : MonoBehaviour
{
    private Transform4D _transform;

    MeshFilter4D _meshFilter;
    
    private Slider _wSlider;
    
    private void OnEnable()
    {
        _transform = GetComponent<Transform4D>();
        _meshFilter = GetComponent<MeshFilter4D>();
        _wSlider = GameObject.Find("W_Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        GetComponent<MeshFilter>().mesh = _meshFilter.Mesh.GetSlice(_transform, _wSlider.value);
    }
}
