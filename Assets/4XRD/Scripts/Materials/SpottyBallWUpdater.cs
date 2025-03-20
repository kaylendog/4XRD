using _4XRD.Transform;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Object4D), typeof(MeshRenderer))]
public class SpottyBallWUpdater : MonoBehaviour
{

    Object4D _object4d;
    MeshRenderer _meshRenderer;

    void Awake()
    {
        _object4d = GetComponent<Object4D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        _meshRenderer.sharedMaterial.SetFloat("_W", _object4d.transform4D.position.w);
    }
}
