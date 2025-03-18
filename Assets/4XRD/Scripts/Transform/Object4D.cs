using UnityEngine;

using _4XRD.XR;

namespace _4XRD.Transform
{
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {
        [SerializeField]
        ARPlane4DController arPlane4DController;
        
        /// <summary>
        /// This object's transform.
        /// </summary>
        public Transform4D transform4D;

        public bool isStatic;

        readonly float FLOOR_LEEWAY = 1;

        public virtual void Awake()
        {
            arPlane4DController = GameObject.Find("XR Origin")?.GetComponent<ARPlane4DController>();
        }

        public virtual void Update()
        {
            if (isStatic)
            {
                transform4D.position.x = transform.position.x;
                transform4D.position.y = transform.position.y;
                transform4D.position.z = transform.position.z;
                
                // transform4D.eulerAngles = transform4D.eulerAngles.SetUnityEuler3(
                //     transform.rotation.eulerAngles
                // );
                
                transform4D.scale.x = transform.localScale.x;
                transform4D.scale.y = transform.localScale.y;
                transform4D.scale.z = transform.localScale.z;
            }
            else
            {
                transform.position = transform4D.position;
                // transform.rotation = Quaternion.Euler(transform4D.eulerAngles.GetUnityEuler3());
                transform.localScale = transform4D.scale;

                float lowestY = 0;
                if (arPlane4DController != null) {
                    lowestY = arPlane4DController.lowestY;
                }
                if (transform4D.position.y < lowestY - FLOOR_LEEWAY)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
