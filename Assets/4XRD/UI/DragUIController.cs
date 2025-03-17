using System;
using System.Collections.Generic;
using _4XRD.Mesh;
using _4XRD.Scripts;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace _4XRD.UI
{
    [ExecuteInEditMode]
    public class DragUIController : MonoBehaviour
    {
        /// <summary>
        /// The tesseract trigger.
        /// </summary>
        public EventTrigger tesseractTrigger;
        
        /// <summary>
        /// The simplex trigger.
        /// </summary>
        public EventTrigger simplexTrigger;
        
        /// <summary>
        /// The hypersphere trigger.
        /// </summary>
        public EventTrigger hypersphereTrigger;

        public ARPlaneManager _ARPlaneManager;
        
        public ARRaycastManager _ARRaycastManager;

        public Slider slicingSlider;

        /// <summary>
        /// The drag target.
        /// </summary>
        GameObject _dragTarget = null;

        PrimitiveType4D _dragType = PrimitiveType4D.Tesseract;

        /// <summary>
        /// Tesseract template.
        /// </summary>
        [Header("Templates")]
        public GameObject tesseract;

        /// <summary>
        /// Simplex template
        /// </summary>
        public GameObject simplex;

        /// <summary>
        /// Hypersphere template
        /// </summary>
        public GameObject hypersphere;

        void Awake()
        {
            slicingSlider = GameObject.Find("SlicingSlider").GetComponent<Slider>();
            var inventoryDrawer = GameObject.Find("InventoryDrawer");
            tesseractTrigger = inventoryDrawer.GetNamedChild("Tesseract").GetComponent<EventTrigger>();
            simplexTrigger = inventoryDrawer.GetNamedChild("Simplex").GetComponent<EventTrigger>();
            hypersphereTrigger = inventoryDrawer.GetNamedChild("Hypersphere").GetComponent<EventTrigger>();
            _ARPlaneManager = GameObject.Find("XR Origin").GetComponent<ARPlaneManager>();
            _ARRaycastManager = GameObject.Find("XR Origin").GetComponent<ARRaycastManager>();
        }

        void OnEnable()
        {
            slicingSlider.onValueChanged.RemoveListener(UpdateSliderValue);
            tesseractTrigger.triggers.Clear();
            simplexTrigger.triggers.Clear();
            hypersphereTrigger.triggers.Clear();
            
            slicingSlider.onValueChanged.AddListener(UpdateSliderValue);
            
            EventTrigger.Entry tesseractBeginDrag = new();
            EventTrigger.Entry simplexBeginDrag = new();
            EventTrigger.Entry hypersphereBeginDrag = new();
            
            tesseractBeginDrag.eventID = EventTriggerType.BeginDrag;
            simplexBeginDrag.eventID = EventTriggerType.BeginDrag;
            hypersphereBeginDrag.eventID = EventTriggerType.BeginDrag;
            
            tesseractBeginDrag.callback.AddListener(data => { InitiateTesseractDrag(); });
            simplexBeginDrag.callback.AddListener(data => { InitiateSimplexDrag(); });
            hypersphereBeginDrag.callback.AddListener(data => { InitiateHypersphereDrag(); });
            
            tesseractTrigger.triggers.Add(tesseractBeginDrag);
            simplexTrigger.triggers.Add(simplexBeginDrag);
            hypersphereTrigger.triggers.Add(hypersphereBeginDrag);
            
            EventTrigger.Entry tesseractDrag = new();
            EventTrigger.Entry simplexDrag = new();
            EventTrigger.Entry hypersphereDrag = new();
            
            tesseractDrag.eventID = EventTriggerType.Drag;
            simplexDrag.eventID = EventTriggerType.Drag;
            hypersphereDrag.eventID = EventTriggerType.Drag;
            
            tesseractDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            simplexDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            hypersphereDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            
            tesseractTrigger.triggers.Add(tesseractDrag);
            simplexTrigger.triggers.Add(simplexDrag);
            hypersphereTrigger.triggers.Add(hypersphereDrag);

            EventTrigger.Entry tesseractEndDrag = new();
            EventTrigger.Entry simplexEndDrag = new();
            EventTrigger.Entry hypersphereEndDrag = new();
            
            tesseractEndDrag.eventID = EventTriggerType.EndDrag;
            simplexEndDrag.eventID = EventTriggerType.EndDrag;
            hypersphereEndDrag.eventID = EventTriggerType.EndDrag;
            
            tesseractEndDrag.callback.AddListener(data => { EndDrag(); });
            simplexEndDrag.callback.AddListener(data => { EndDrag(); });
            hypersphereEndDrag.callback.AddListener(data => { EndDrag(); });
            
            tesseractTrigger.triggers.Add(tesseractEndDrag);
            simplexTrigger.triggers.Add(simplexEndDrag);
            hypersphereTrigger.triggers.Add(hypersphereEndDrag);
        }

        void OnDisable()
        {
            slicingSlider.onValueChanged.RemoveListener(UpdateSliderValue);
            tesseractTrigger.triggers.Clear();
            simplexTrigger.triggers.Clear();
            hypersphereTrigger.triggers.Clear();
        }
        
        /// <summary>
        /// Update the slider value.
        /// </summary>
        public void UpdateSliderValue(float value)
        {
            MeshObject4D.SlicingConstant = value;
        }
        
        /// <summary>
        /// Initiate a drag.
        /// </summary>
        /// <param name="type"></param>
        public void InitiateDrag(PrimitiveType4D type)
        {
            _dragType = type;
            _dragTarget = type switch
            {
                PrimitiveType4D.Tesseract => Instantiate(tesseract),
                PrimitiveType4D.Simplex4 => Instantiate(simplex),
                PrimitiveType4D.Hypersphere => Instantiate(hypersphere),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };
        }

        /// <summary>
        /// Initiate a tesseract drag.
        /// </summary>
        public void InitiateTesseractDrag()
        {
            InitiateDrag(PrimitiveType4D.Tesseract);
        }

        /// <summary>
        /// Initiate a simplex drag.
        /// </summary>
        public void InitiateSimplexDrag()
        {
            InitiateDrag(PrimitiveType4D.Simplex4);
        }

        /// <summary>
        /// Initiate a sphere drag.
        /// </summary>
        public void InitiateHypersphereDrag()
        {
            InitiateDrag(PrimitiveType4D.Hypersphere);
        }
        
        
        /// <summary>
        /// Drag event.
        /// </summary>
        /// <param name="data"></param>
        void OnDrag(PointerEventData data)
        {
            // raycast with floor
            Assert.IsNotNull(Camera.main);
            List<ARRaycastHit> hits = new();
            Ray ray = Camera.main.ScreenPointToRay(data.position);
            _ARRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon);

            // ignore no-hits and bad targets
            if (hits.Count == 0 || _dragTarget == null)
            {
                return;
            }

            ARRaycastHit? raycastHit = null;
            foreach (var hit in hits)
            {
                if ((hit.hitType & TrackableType.Planes) != 0)
                {
                    ARPlane plane = _ARPlaneManager.GetPlane(hit.trackableId);
                    if (plane.subsumedBy != null)
                    {
                        raycastHit = hit;
                        break;
                    }
                }
            }

            if (raycastHit.HasValue)
            {
                // update transform (use ray for safety)
                var transform4D = _dragTarget.GetComponent<Object4D>().transform4D;
                var targetPosition = raycastHit.Value.pose.position - ray.direction * 2.0f;
                _dragTarget.transform.position = targetPosition;
                transform4D.position = targetPosition;

                // set to currently viewed slice
                transform4D.position.w = MeshObject4D.SlicingConstant;

                // set parent to hit object
                var parent = raycastHit.Value.trackable.gameObject;
                _dragTarget.transform.SetParent(parent.transform);
            }
        }

        /// <summary>
        /// End of the drag.
        /// </summary>
        public void EndDrag()
        {
            var obj = _dragTarget.GetComponent<Object4D>();
            if (obj != null)
            {
                obj.isStatic = true;
            }
            _dragTarget = null;
        }
    }
}
