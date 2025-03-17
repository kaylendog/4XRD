using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
        foreach (var plane in eventArgs.added)
        {
        }

        foreach (var plane in eventArgs.updated)
        {
        }

        foreach (var plane in eventArgs.removed)
        {
        }
    }
}
