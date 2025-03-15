using System;
using UnityEngine;
using _4XRD.Mesh;

namespace _4XRD.Physics.Tensors
{
    [Serializable]
    public record BoundingBox4D
    {
        /// <summary>
        /// The first corner of the bounding box.
        /// </summary>
        [field: SerializeField] public Vector4 Min { get; private set;}

        /// <summary>
        /// The second corner of the bounding box.
        /// </summary>
        [field: SerializeField] public Vector4 Max { get; private set;}

        /// <summary>
        /// The middle of the bounding box.
        /// </summary>
        public Vector4 Center => Vector4.Lerp(Min, Max, 0.5f);

        /// <summary>
        /// Compute a bounding box from a mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public static BoundingBox4D FromMesh(Mesh4D mesh)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float minZ = float.MaxValue;
            float minW = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            float maxZ = float.MinValue;
            float maxW = float.MinValue;

            foreach (var vertex in mesh.Vertices)
            {
                minX = Mathf.Min(minX, vertex.x);
                minY = Mathf.Min(minY, vertex.y);
                minZ = Mathf.Min(minZ, vertex.z);
                minW = Mathf.Min(minW, vertex.w);

                maxX = Mathf.Max(maxX, vertex.x);
                maxY = Mathf.Max(maxY, vertex.y);
                maxZ = Mathf.Max(maxZ, vertex.z);
                maxW = Mathf.Max(maxW, vertex.w);
            }
            
            return new BoundingBox4D(
                new Vector4(minX, minY, minZ, minW),
                new Vector4(maxX, maxY, maxZ, maxW)
            );
        }

        /// <summary>
        /// Construct a bounding box from two vectors.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public BoundingBox4D(Vector4 min, Vector4 max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Test if this bounding box includes another.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Includes(BoundingBox4D other)
        {
            return Min.x <= other.Min.x &&
                Min.y <= other.Min.y &&
                Min.z <= other.Min.z &&
                Min.w <= other.Min.w &&
                Max.x >= other.Max.x &&
                Max.y >= other.Max.y &&
                Max.z >= other.Max.z &&
                Max.w >= other.Max.w;
        }

        /// <summary>
        /// SDFSDFSDFSDF.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public BoundingBox4D Intersection(BoundingBox4D other)
        {
            float minX = Mathf.Max(Min.x, other.Min.x);
            float minY = Mathf.Max(Min.y, other.Min.y);
            float minZ = Mathf.Max(Min.z, other.Min.z);
            float minW = Mathf.Max(Min.w, other.Min.w);

            float maxX = Mathf.Min(Max.x, other.Max.x);
            float maxY = Mathf.Min(Max.y, other.Max.y);
            float maxZ = Mathf.Min(Max.z, other.Max.z);
            float maxW = Mathf.Min(Max.w, other.Max.w);
            
            if (minX > maxX || minY > maxY || 
                minZ > maxZ || minW > maxW)
            {
                return null;
            }

            return new BoundingBox4D(
                new Vector4(minX, minY, minZ, minW),
                new Vector4(maxX, maxY, maxZ, maxW)
            );
        }

        /// <summary>
        /// Test whether two bounding boxes overlap.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Overlaps(BoundingBox4D other)
        {
            return Intersection(other) != null;
        }
    }
}
