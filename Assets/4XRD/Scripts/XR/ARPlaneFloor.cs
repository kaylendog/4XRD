using UnityEngine;

namespace _4XRD.XR
{
    public class ARPlaneFloor : MonoBehaviour
    {
        ARPlane4DController arPlane4DController;
        
        void Awake()
        {
            arPlane4DController = GameObject.Find("XR Origin").GetComponent<ARPlane4DController>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 position = transform.position;
            position.y = arPlane4DController.lowestY;
            transform.position = position;
        }
    }
}