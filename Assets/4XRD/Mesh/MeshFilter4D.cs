using UnityEngine;

namespace _4XRD.Mesh
{
    [ExecuteInEditMode]
    public class MeshFilter4D : MonoBehaviour
    {
        [SerializeField] private PrimitiveType4D type;

        public Mesh4D Mesh { get; private set; }

        void OnValidate()
        {
            Mesh = Mesh4D.CreatePrimitive(type);    
        }
    }
}
