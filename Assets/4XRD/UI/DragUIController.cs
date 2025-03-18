using System;
using System.Collections.Generic;
using _4XRD.Mesh;
using _4XRD.Physics;
using _4XRD.Scripts;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace _4XRD.UI
{
    [ExecuteInEditMode]
    public class DragUIController : MonoBehaviour
    {
        
        
        /// <summary>
        /// The plane manager.
        /// </summary>
        [Header("AR")]
        public ARPlaneManager arPlaneManager;
        
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
            hypersphereDrag.callback.AddListener(data => { OnDrag((PointerEventData) data); });
            
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
            Debug.Log("Initiate Drag");
            _dragTarget = Instantiate(tesseract);
        }

        /// <summary>
        /// Initiate a simplex drag.
        /// </summary>
        public void InitiateSimplexDrag()
        {
            _dragTarget = Instantiate(simplex);
        }

        /// <summary>
        /// Initiate a sphere drag.
        /// </summary>
        public void InitiateHypersphereDrag()
        {
            _dragTarget = Instantiate(hypersphere);
        }
        
        /// <summary>
        /// Drag event.
        /// </summary>
        /// <param name="data"></param>
        void OnDrag(PointerEventData data)
        {
            Debug.Log("Drag");
            // raycast with floor
            Assert.IsNotNull(Camera.main);
            List<ARRaycastHit> hits = new();
            Ray ray = Camera.main.ScreenPointToRay(data.position);
            arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon);

            // ignore no-hits and bad targets
            if (hits.Count == 0 || _dragTarget == null)
            {
                return;
            }

            Debug.Log($"Raycast hits: {hits.Count}");

            ARRaycastHit? raycastHit = null;
            foreach (var hit in hits)
            {
                if ((hit.hitType & TrackableType.PlaneWithinPolygon) != 0)
                {
                    ARPlane plane = arPlaneManager.GetPlane(hit.trackableId);
                    if (plane.subsumedBy != null)
                    {
                        raycastHit = hit;
                        break;
                    }
                }
            }

            if (!raycastHit.HasValue) 
            {
                return;
            }
            Debug.Log(raycastHit.Value.trackable.gameObject.name);
            
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

        /// <summary>
        /// End of the drag.
        /// </summary>
        public void EndDrag()
        {
            Debug.Log("End Drag");
            var obj = _dragTarget.GetComponent<Object4D>();
            if (obj != null)
            {
                obj.isStatic = true;
            }
            _dragTarget = null;
        }

        /// <summary>
        /// End drag with random output velocity.
        /// </summary>
        public void EndDragRandomVelocity()
        {
            Debug.Log("End Drag Random Velocity");
            var obj = _dragTarget.GetComponent<Object4D>();
            if (obj != null)
            {
                obj.isStatic = true;
            }
            var ball = _dragTarget.GetComponent<Ball4D>();
            if (ball != null)
            {
                ball.velocity.x = Random.Range(-2f, 2f);
                ball.velocity.z = Random.Range(-2f, 2f);
                ball.velocity.w = Random.Range(-0.5f, 0.5f);
            }
            _dragTarget = null;
        }
    }
}
