using System;
using UnityEngine;

using _4XRD.XR;

namespace _4XRD.Transform
{
    [ExecuteInEditMode]
    public class Bounds : MonoBehaviour
    {
        public ARPlane4DController arPlane4DController;

        public float defaultXSize = 10;
        public float defaultZSize = 10;
        public float defaultYFloor = -1;

        public float boundPadding = 1;

        GameObject minXBound;
        GameObject maxXBound;
        GameObject minYBound;
        GameObject minZBound;
        GameObject maxZBound;

        void Awake()
        {
            arPlane4DController = GameObject.Find("XR Origin")?.GetComponent<ARPlane4DController>();
            minXBound = gameObject.transform.GetChild(0).gameObject;
            maxXBound = gameObject.transform.GetChild(1).gameObject;
            minYBound = gameObject.transform.GetChild(2).gameObject;
            minZBound = gameObject.transform.GetChild(3).gameObject;
            maxZBound = gameObject.transform.GetChild(4).gameObject;
        }

        // Update is called once per frame
        void Update()
        {   
            float minX = -defaultXSize/2;
            float maxX = defaultXSize/2;
            float minY = defaultYFloor;
            float minZ = -defaultZSize/2;
            float maxZ = defaultZSize/2;

            if (arPlane4DController != null)
            {
                minX = Math.Min(minX, arPlane4DController.minX - boundPadding);
                maxX = Math.Max(maxX, arPlane4DController.maxX + boundPadding);
                minY = Math.Min(minY, arPlane4DController.minY - boundPadding);
                minZ = Math.Min(minZ, arPlane4DController.minZ - boundPadding);
                maxZ = Math.Max(maxZ, arPlane4DController.maxZ + boundPadding);
            }

            minXBound.transform.localPosition = new Vector3(minX, 0, 0);
            maxXBound.transform.localPosition = new Vector3(maxX, 0, 0);
            minYBound.transform.localPosition = new Vector3(0, minY, 0);
            minZBound.transform.localPosition = new Vector3(0, 0, minZ);
            maxZBound.transform.localPosition = new Vector3(0, 0, maxZ);
        }
    }
}