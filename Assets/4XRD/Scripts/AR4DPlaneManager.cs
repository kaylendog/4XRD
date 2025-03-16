using _4XRD.Physics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneMeshVisualizer))]
public class AR4DPlaneManager : MonoBehaviour
{
    Object4D _object4D;

    void Awake()
    {
        _object4D = GetComponent<Object4D>();
    }

    // Update is called once per frame
    void Update()
    {
        _object4D.transform4D.position = transform.position;
        _object4D.transform4D.rotation = Rotation4x4.FromAngles(Euler6.FromEuler3(transform.rotation.eulerAngles));
    }
}
