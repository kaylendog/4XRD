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
        /// The XR origin.
        /// </summary>
        public GameObject xrOrigin;
        
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
        /// Moving Hypersphere template
        /// </summary>
        public GameObject movingHypersphere;

        /// <summary>
        /// The taret
        /// </summary>
        public GameObject target;

        /// <summary>
        /// The arm offset.
        /// </summary>
        public float armOffset = -0.3f;
        
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
        
        // /// <summary>
        // /// Initiate a drag.
        // /// </summary>
        // /// <param name="type"></param>
        // public void InitiateDrag(PrimitiveType4D type)
        // {
        //     _dragTarget = type switch
        //     {
        //         PrimitiveType4D.Tesseract => Instantiate(tesseract),
        //         PrimitiveType4D.Simplex4 => Instantiate(simplex),
        //         PrimitiveType4D.Hypersphere => Instantiate(hypersphere),
        //         _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        //     };
        // }

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
        /// Initiate a movingsphere drag.
        /// </summary>
        public void InitiateTargetDrag()
        {
            _dragTarget = Instantiate(target);
            _dragLocation = null;
        }
        
        /// <summary>
        /// Drag event.
        /// </summary>
        /// <param name="data"></param>
        void OnDrag(PointerEventData data)
        {            
            Assert.IsNotNull(Camera.main);
            Ray ray = Camera.main.ScreenPointToRay(data.position);

            Vector3 hitPosition;
            if (arRaycastManager != null)
            {
                List<ARRaycastHit> hits = new();
                arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon);

                if (hits.Count == 0 || _dragTarget == null)
                {
                    _dragLocation = null;
                    return;
                }

                ARRaycastHit raycastHit = hits[0];
                if (raycastHit.trackable is not ARPlane)
                {
                    _dragLocation = null;
                    return;
                }

                hitPosition = raycastHit.pose.position;
                _dragLocation = raycastHit.trackable.gameObject;
            }
            else
            {
                RaycastHit hit;
                var didHit = UnityEngine.Physics.Raycast(ray, out hit);
                
                // ignore no-hits and bad targets
                if (!didHit || _dragTarget == null)
                {
                    _dragLocation = null;
                    return;
                }

                hitPosition = hit.point;
                _dragLocation = hit.collider.gameObject;
            }
            
            // update transform (use ray for safety)
            var targetPosition = hitPosition - ray.direction * 0.05f;
            _dragTarget.transform.position = targetPosition;

            // set to currently viewed slice if is 4D object
            if (_dragTarget.TryGetComponent<Object4D>(out var object4D))
            {
                object4D.SetWPosStatic(MeshObject4D.SlicingConstant);
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

            if (_dragLocation.GetComponent<ARPlane>() != null)
            {
                _dragTarget.transform.SetParent(_dragLocation.transform);
            }
            _dragTarget = null;
        }

        /// <summary>
        /// End drag with random output velocity.
        /// </summary>
        public void EndDragLaunchHypersphere()
        {
            if (_dragLocation == null)
            {
                Destroy(_dragTarget);
                return;
            }

            Vector3 throwTarget = _dragTarget.transform.position;
            Vector3 playerPosition = Camera.main.transform.position + new Vector3(0, armOffset, 0);

            Destroy(_dragTarget);

            var hypersphere = Instantiate(movingHypersphere);
            hypersphere.GetComponent<Object4D>().SetPosition(new Vector4(
                playerPosition.x,
                playerPosition.y,
                playerPosition.z,
                MeshObject4D.SlicingConstant
            ));

            float throwSpeed = 0.5f;
            hypersphere.GetComponent<Ball4D>().velocity = new Vector4(
                (throwTarget.x - playerPosition.x) / throwSpeed,
                (throwTarget.y - playerPosition.y) / throwSpeed + (0.5f * 9.8f * throwSpeed),
                (throwTarget.z - playerPosition.z) / throwSpeed,
                0
            );

            _dragTarget = null;
        }
    }
}
