using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

using _4XRD.Mesh;
using _4XRD.Physics;
using _4XRD.Transform;

namespace _4XRD.UI
{
    public class DragUIController : MonoBehaviour
    {        
        /// <summary>
        /// The raycast manager.
        /// </summary>
        public ARRaycastManager arRaycastManager;
        
        /// <summary>
        /// The tesseract trigger.
        /// </summary>
        [Header("Triggers")]
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
        /// The moving hypersphere trigger.
        /// </summary>
        public EventTrigger movingHypersphereTrigger;

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
        
        /// <summary>
        /// The drag target.
        /// </summary>
        GameObject _dragTarget;

        /// <summary>
        /// The drag target location.
        /// </summary>
        GameObject _dragLocation;
        
        void OnEnable()
        {
            EventTrigger.Entry tesseractDrag = new();
            EventTrigger.Entry simplexDrag = new();
            EventTrigger.Entry hypersphereDrag = new();
            EventTrigger.Entry movingHypersphereDrag = new();
            
            tesseractDrag.eventID = EventTriggerType.Drag;
            simplexDrag.eventID = EventTriggerType.Drag;
            hypersphereDrag.eventID = EventTriggerType.Drag;
            movingHypersphereDrag.eventID = EventTriggerType.Drag;
            
            tesseractDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            simplexDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            hypersphereDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            movingHypersphereDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            
            tesseractTrigger.triggers.Add(tesseractDrag);
            simplexTrigger.triggers.Add(simplexDrag);
            hypersphereTrigger.triggers.Add(hypersphereDrag);
            movingHypersphereTrigger.triggers.Add(movingHypersphereDrag);
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
            _dragTarget = Instantiate(tesseract);
            _dragLocation = null;
        }

        /// <summary>
        /// Initiate a simplex drag.
        /// </summary>
        public void InitiateSimplexDrag()
        {
            _dragTarget = Instantiate(simplex);
            _dragLocation = null;
        }

        /// <summary>
        /// Initiate a sphere drag.
        /// </summary>
        public void InitiateHypersphereDrag()
        {
            _dragTarget = Instantiate(hypersphere);
            _dragLocation = null;
        }
        
        /// <summary>
        /// Drag event.
        /// </summary>
        /// <param name="data"></param>
        void OnDrag(PointerEventData data)
        {
            if (arRaycastManager == null)
            {
                Assert.IsNotNull(Camera.main);
                var _ray = Camera.main.ScreenPointToRay(data.position);
                var didHit = UnityEngine.Physics.Raycast(_ray, out RaycastHit hit);
                
                // ignore no-hits and bad targets
                if (!didHit || _dragTarget == null)
                {
                    return;
                }
            
                // update transform (use ray for safety)
                var targetPosition = hit.transform.position - _ray.direction * 2f;
                _dragTarget.transform.position = targetPosition;

                // set to currently viewed slice
                var transform4D = _dragTarget.GetComponent<Object4D>().transform4D;
                transform4D.position.w = MeshObject4D.SlicingConstant;

                _dragLocation = hit.collider.gameObject;
                return;
            }
            
            // raycast with floor
            Assert.IsNotNull(Camera.main);
            List<ARRaycastHit> hits = new();
            Ray ray = Camera.main.ScreenPointToRay(data.position);
            arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon);

            // ignore no-hits and bad targets
            if (hits.Count == 0 || _dragTarget == null)
            {
                _dragLocation = null;
                return;
            }

            ARRaycastHit raycastHit = hits[0];
            if (raycastHit.trackable is ARPlane plane)
            {
                _dragLocation = plane.gameObject;
            
                // update transform (use ray for safety)
                var targetPosition = raycastHit.pose.position - ray.direction * 0.3f;
                _dragTarget.transform.position = targetPosition;

                // set to currently viewed slice
                var transform4D = _dragTarget.GetComponent<Object4D>().transform4D;
                transform4D.position.w = MeshObject4D.SlicingConstant;
            }
            else 
            {
                _dragLocation = null;
            }
        }

        /// <summary>
        /// End of the drag.
        /// </summary>
        public void EndDrag()
        {
            if (_dragLocation == null)
            {
                Destroy(_dragTarget);
                return;
            }

            var transform4D = _dragTarget.GetComponent<Object4D>().transform4D;
            transform4D.position.w = MeshObject4D.SlicingConstant;
            if (_dragLocation.GetComponent<ARPlane>() != null)
            {
                _dragTarget.transform.SetParent(_dragLocation.transform);
            }
            _dragTarget = null;
        }

        /// <summary>
        /// End drag with random output velocity.
        /// </summary>
        public void EndDragRandomVelocity()
        {
            if (_dragLocation == null)
            {
                Destroy(_dragTarget);
                return;
            }
            
            var object4D = _dragTarget.GetComponent<Object4D>();
            if (object4D != null)
            {
                object4D.isStatic = false;
            }
            var ball = _dragTarget.GetComponent<Ball4D>();
            if (ball != null)
            {
                ball.velocity.x = Random.Range(-2f, 2f);
                ball.velocity.z = Random.Range(-2f, 2f);
                ball.velocity.w = Random.Range(-0.5f, 0.5f);
            }
            if (_dragLocation.GetComponent<ARPlane>() != null)
            {
                _dragTarget.transform.SetParent(_dragLocation.transform);
            }
            _dragTarget = null;
        }
    }
}
