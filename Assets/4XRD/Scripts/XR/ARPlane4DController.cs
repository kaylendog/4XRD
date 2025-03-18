using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace _4XRD.XR
{
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlane4DController : MonoBehaviour
    {
        [field: SerializeField]
        public float lowestY { get; private set; }
        
        ARPlaneManager arPlaneManager;

        void Awake()
        {
            lowestY = 0;
            arPlaneManager = GetComponent<ARPlaneManager>();
        }

        void OnEnable()
        {
            arPlaneManager.trackablesChanged.AddListener(OnTrackablesChanged);
        }

        void OnDisable()
        {
            arPlaneManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
        }

        void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> eventArgs)
        {
            foreach (var plane in eventArgs.added)
            {
                float planeY = plane.gameObject.transform.position.y;
                if (planeY < lowestY)
                {
                    lowestY = planeY;
                }
            }

            foreach (var plane in eventArgs.updated)
            {
                float planeY = plane.gameObject.transform.position.y;
                if (planeY < lowestY)
                {
                    lowestY = planeY;
                }
            }
        }
    }
}
