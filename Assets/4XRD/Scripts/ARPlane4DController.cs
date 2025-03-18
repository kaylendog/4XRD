using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlane4DController : MonoBehaviour
{
    ARPlaneManager _ARPlaneManager;

    void Awake()
    {
        _ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        _ARPlaneManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDisable()
    {
        _ARPlaneManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> eventArgs)
    {
        foreach (var plane in eventArgs.removed)
        {
            ARPlane subsumedByPlane = plane.Value.subsumedBy;

            List<GameObject> planeGameObjects = new();
            plane.Value.gameObject.GetChildGameObjects(planeGameObjects);
            planeGameObjects.ForEach(obj => obj.transform.SetParent(subsumedByPlane?.gameObject.transform));
        }
    }
}