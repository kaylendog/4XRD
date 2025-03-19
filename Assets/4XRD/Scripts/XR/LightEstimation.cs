using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

namespace _4XRD.XR
{
    [RequireComponent(typeof(Light))]
    public class LightEstimation : MonoBehaviour
    {
        public ARCameraManager arCameraManager;
        public Light Light;

        void Awake()
        {
            Light = GetComponent<Light>();
        }

        void OnEnable()
        {
            arCameraManager.frameReceived += FrameReceived;
        }

        private void FrameReceived(ARCameraFrameEventArgs args)
        {
            ARLightEstimationData lightEstimation = args.lightEstimation;

            Light.intensity = lightEstimation.averageBrightness ?? Light.intensity;
            Light.colorTemperature = lightEstimation.averageColorTemperature ?? Light.colorTemperature;
            Light.color = lightEstimation.colorCorrection ?? Light.color;
            Light.transform.rotation = Quaternion.LookRotation(
                lightEstimation.mainLightDirection ?? Vector3.down
            );
            Light.color = lightEstimation.mainLightColor ?? Light.color;
            Light.intensity = lightEstimation.mainLightIntensityLumens ?? Light.intensity;

            if (lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = lightEstimation.ambientSphericalHarmonics.Value;
            }
        }
    }
}
