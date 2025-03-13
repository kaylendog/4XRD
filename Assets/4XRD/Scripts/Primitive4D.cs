using System.Diagnostics;
using _4XRD.Mesh;
using _4XRD.Physics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(RigidBody4D))]
[ExecuteInEditMode]
public class Object4D : MonoBehaviour
{
    [SerializeField] private PrimitiveType4D type;
    private Mesh4D mesh4D;
    private Transform4D transform4D;
    private Slider w_slider;
    
    private void OnEnable()
    {
        mesh4D = Mesh4D.CreatePrimitive(type);
        transform4D = GetComponent<Transform4D>();
        w_slider = GameObject.Find("W_Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        Transform transform = GetComponent<Transform>();
        transform.position = transform4D.position.DropW();
        GetComponent<MeshFilter>().mesh = mesh4D.GetSlice(transform4D.rotation, w_slider.value);
    }
}
