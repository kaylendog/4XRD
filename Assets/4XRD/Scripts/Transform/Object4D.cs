using UnityEngine;

using _4XRD.XR;

namespace _4XRD.Transform
{
    [ExecuteInEditMode]
    public class Object4D : MonoBehaviour
    {   
        /// <summary>
        /// This object's transform.
        /// </summary>
        [field: SerializeField]
        public Transform4D transform4D { get; private set; }

        public bool isStatic;

        public void SetPosition(Vector4 position)
        {
            if (isStatic)
            {
                Debug.LogWarning("Cannot set position of static object.");
                return;
            }
            transform4D = new(
                position,
                transform4D.scale,
                transform4D.eulerAngles
            );
        }

        public void SetWPosStatic(float wPos)
        {
            if (!isStatic)
            {
                Debug.LogWarning("Cannot set wPos of non-static object.");
                return;
            }
            transform4D = new(
                new Vector4(
                    transform4D.position.x,
                    transform4D.position.y,
                    transform4D.position.z,
                    wPos
                ),
                transform4D.scale,
                transform4D.eulerAngles
            );
        }

        public void SetScale(Vector4 scale)
        {
            transform4D = new(
                transform4D.position,
                scale,
                transform4D.eulerAngles
            );
        }

        public virtual void Awake() {}

        public virtual void Update()
        {
            if (isStatic)
            {
                // Propagate transform changes to the 4D transform.
                transform4D = new(
                    new Vector4(
                        transform.position.x,
                        transform.position.y,
                        transform.position.z,
                        transform4D.position.w
                    ),
                    new Vector4(
                        transform4D.scale.x,
                        transform4D.scale.y,
                        transform4D.scale.z,
                        transform4D.scale.w
                    ),
                    transform4D.eulerAngles.SetUnityEuler3(
                        transform.rotation.eulerAngles
                    )
                );
            }
            else
            {
                // Propagate 4D transform changes to the transform.
                transform.SetPositionAndRotation(
                    transform4D.position,
                    Quaternion.Euler(transform4D.eulerAngles.GetUnityEuler3())
                );                
            }
        }
    }
}
