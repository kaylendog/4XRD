using _4XRD.Physics;
using _4XRD.Physics.Mesh;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Object4D : MonoBehaviour
{
    [SerializeField]
    private PrimitiveType4D type;

    private Mesh4D mesh4D;

    private void Awake()
    {
        mesh4D = Mesh4D.CreatePrimitive(type);
        GetComponent<MeshFilter>().mesh = mesh4D.GetSlice(0);
    }

    private void Update()
    {
        GetComponent<MeshFilter>().mesh = mesh4D.GetSlice(0);
    }
}
