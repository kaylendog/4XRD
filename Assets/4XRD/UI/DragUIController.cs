using System;
using _4XRD.Mesh;
using _4XRD.Physics;
using _4XRD.Physics.Colliders;
using _4XRD.Scripts;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace _4XRD.UI
{
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

        /// <summary>
        /// The drag target.
        /// </summary>
        GameObject _dragTarget = null;

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
        
        void Start()
        {
            EventTrigger.Entry tesseractEntry = new EventTrigger.Entry();
            EventTrigger.Entry simplexEntry = new EventTrigger.Entry();
            EventTrigger.Entry hypersphereEntry = new EventTrigger.Entry();
            
            tesseractEntry.eventID = EventTriggerType.Drag;
            simplexEntry.eventID = EventTriggerType.Drag;
            hypersphereEntry.eventID = EventTriggerType.Drag;
            
            tesseractEntry.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            simplexEntry.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            hypersphereEntry.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            
            tesseractTrigger.triggers.Add(tesseractEntry);
            simplexTrigger.triggers.Add(simplexEntry);
            hypersphereTrigger.triggers.Add(hypersphereEntry);
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
        public void InitiateDrag(
            PrimitiveType4D type
        )
        {
            switch (type)
            {
                case PrimitiveType4D.Tesseract:
                    _dragTarget = Instantiate(tesseract);
                    break;
                case PrimitiveType4D.Simplex4:
                    _dragTarget = Instantiate(simplex);
                    break;
                case PrimitiveType4D.Hypersphere:
                    _dragTarget = Instantiate(hypersphere);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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
            var ray = Camera.main.ScreenPointToRay(data.position);
            var didHit = UnityEngine.Physics.Raycast(ray, out RaycastHit hit);
            
            // ignore no-hits and bad targets
            if (!didHit || _dragTarget == null)
            {
                return;
            }
        
            // update transform (use ray for safety)
            var transform4D = _dragTarget.GetComponent<Object4D>().transform4D;
            var targetPosition = hit.point - ray.direction * 2.0f;
            _dragTarget.transform.position = targetPosition;
            transform4D.position = targetPosition;

            // set to currently viewed slice
            transform4D.position.w = MeshObject4D.SlicingConstant;
        }

        /// <summary>
        /// End of the drag.
        /// </summary>
        public void EndDrag()
        {
            var obj = _dragTarget.GetComponent<Object4D>();
            if (obj != null)
            {
                obj.isStatic = false;
            }
            _dragTarget = null;
        }
    }
}
