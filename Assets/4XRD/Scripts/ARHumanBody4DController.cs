using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARHumanBodyManager))]
public class ARHumanBody4DController : MonoBehaviour
{
    public GameObject ARHumanBody4DPrefab;

    ARHumanBodyManager _ARHumanBodyManager;

    Dictionary<TrackableId, ARHumanBody4D> bodies = new();

    void Awake()
    {
        _ARHumanBodyManager = GetComponent<ARHumanBodyManager>();
    }

    void OnEnable()
    {
        _ARHumanBodyManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDisable()
    {
        _ARHumanBodyManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARHumanBody> eventArgs)
    {
        ARHumanBody4D bodyController;
        foreach (var body in eventArgs.added)
        {
            if (!bodies.TryGetValue(body.trackableId, out bodyController))
            {
                var newBody = Instantiate(ARHumanBody4DPrefab, body.transform);
                bodyController = newBody.GetComponent<ARHumanBody4D>();
                bodies.Add(body.trackableId, bodyController);
            }

            bodyController.CreateBody(body);
            bodyController.ApplyBodyPose(body);
        }

        foreach (var body in eventArgs.updated)
        {
            if (bodies.TryGetValue(body.trackableId, out bodyController))
            {
                bodyController.ApplyBodyPose(body);
            }
        }

        foreach (var body in eventArgs.removed)
        {
            if (bodies.TryGetValue(body.Key, out bodyController))
            {
                Destroy(bodyController.gameObject);
                bodies.Remove(body.Key);
            }
        }
    }
}
