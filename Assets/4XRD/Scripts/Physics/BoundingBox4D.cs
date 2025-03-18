using System;
using UnityEngine;

namespace _4XRD.Physics
{
    [Serializable]
    public record BoundingBox4D
    {
        /// <summary>
        /// The first corner of the bounding box.
        /// </summary>
        [field: SerializeField]
        public Vector4 min { get; private set; }

        /// <summary>
        /// The second corner of the bounding box.
        /// </summary>
        [field: SerializeField]
        public Vector4 max { get; private set; }

        /// <summary>
        /// The middle of the bounding box.
        /// </summary>
        public Vector4 center => Vector4.Lerp(min, max, 0.5f);

        /// <summary>
        /// Compute a bounding box from a mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public static BoundingBox4D FromMesh(_4XRD.Mesh.Mesh4D mesh)
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
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Test if this bounding box includes another.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Includes(BoundingBox4D other)
        {
            return min.x <= other.min.x &&
                min.y <= other.min.y &&
                min.z <= other.min.z &&
                min.w <= other.min.w &&
                max.x >= other.max.x &&
                max.y >= other.max.y &&
                max.z >= other.max.z &&
                max.w >= other.max.w;
        }

        /// <summary>
        /// SDFSDFSDFSDF.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public BoundingBox4D Intersection(BoundingBox4D other)
        {
            float minX = Mathf.Max(min.x, other.min.x);
            float minY = Mathf.Max(min.y, other.min.y);
            float minZ = Mathf.Max(min.z, other.min.z);
            float minW = Mathf.Max(min.w, other.min.w);

            float maxX = Mathf.Min(max.x, other.max.x);
            float maxY = Mathf.Min(max.y, other.max.y);
            float maxZ = Mathf.Min(max.z, other.max.z);
            float maxW = Mathf.Min(max.w, other.max.w);

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

        /// <summary>
        /// Returns whether the given point is inside this bounding box.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Includes(Vector4 point)
        {
            return min.x <= point.x && point.x <= max.x &&
                min.y <= point.y && point.y <= max.y &&
                min.z <= point.z && point.z <= max.z &&
                min.w <= point.w && point.w <= max.w;
        }
    }
}
