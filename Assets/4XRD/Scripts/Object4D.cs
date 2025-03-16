using _4XRD.Physics;
using UnityEngine;

[ExecuteInEditMode]
public class Object4D : MonoBehaviour
{
    /// <summary>
    /// This object's transform.
    /// </summary>
    public Transform4D transform4D = Transform4D.identity;

    /// <summary>
    /// Whether object is static.
    /// </summary>
    public bool isStatic = false;

    void Update()
    {
        if (!isStatic)
        {
            // Propagate 4D transform to 3D transform.     
            transform.position = transform4D.position;
            transform.localScale = transform4D.scale;
        }
    }
}
