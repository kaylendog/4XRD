using UnityEngine;

namespace Engine4D.Scripts
{
    public class CompassLetter : MonoBehaviour {
        private Camera mainCam = null;

        protected void Update() {
            if (UnityEngine.XR.XRSettings.enabled) {
                if (!mainCam) { mainCam = Camera.main; }
                transform.rotation = Quaternion.LookRotation(mainCam.transform.position - transform.position, Vector3.up);
            } else {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
