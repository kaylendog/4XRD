using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace _4XRD.XR
{
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlane4DController : MonoBehaviour
    {
        [field: SerializeField]
        public float minX { get; private set; }

        [field: SerializeField]
        public float maxX { get; private set; }

        [field: SerializeField]
        public float minY { get; private set; }

        [field: SerializeField]
        public float maxY { get; private set; }

        [field: SerializeField]
        public float minZ { get; private set; }

        [field: SerializeField]
        public float maxZ { get; private set; }
        
        ARPlaneManager arPlaneManager;

        void Awake()
        {
            minX = float.MaxValue;
            maxX = float.MinValue;
            minY = float.MaxValue;
            maxY = float.MinValue;
            minZ = float.MaxValue;
            maxZ = float.MinValue;

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
                UpdateBounds(plane.center);
            }

            foreach (var plane in eventArgs.updated)
            {
                UpdateBounds(plane.center);
            }
        }

        private void UpdateBounds(Vector3 position)
        {
            minX = Mathf.Min(minX, position.x);
            maxX = Mathf.Max(maxX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxY = Mathf.Max(maxY, position.y);
            minZ = Mathf.Min(minZ, position.z);
            maxZ = Mathf.Max(maxZ, position.z);
        }
    }
}
