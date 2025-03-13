using _4XRD.Mesh;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Object4D : MonoBehaviour
{
    [SerializeField] private PrimitiveType4D type;
    private Mesh4D mesh4D;
    private Slider w_slider;
    
    private void Awake()
    {
        w_slider = GameObject.Find("W_Slider").GetComponent<Slider>();
        mesh4D = Mesh4D.CreatePrimitive(type);
    }

    private void Update()
    {
        GetComponent<MeshFilter>().mesh = mesh4D.GetSlice(w_slider.value);
    }
}
