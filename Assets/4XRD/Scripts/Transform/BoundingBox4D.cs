using System;
using UnityEngine;

namespace _4XRD.Transform
{
    [Serializable]
    public class BoundingBox4D
    {
        /// <summary>
        /// The first corner of the bounding box.
        /// </summary>
        [field: SerializeField] public Vector4 min { get; private set; }

        /// <summary>
        /// The second corner of the bounding box.
        /// </summary>
        [field: SerializeField] public Vector4 max { get; private set; }

        /// <summary>
        /// The middle of the bounding box.
        /// </summary>
        public Vector4 center => Vector4.Lerp(min, max, 0.5f);

        // /// <summary>
        // /// Compute a bounding box from a mesh.
        // /// </summary>
        // /// <param name="mesh"></param>
        // public static BoundingBox4D FromMesh(Mesh.Mesh4D mesh)
        // {
        //     float minX = float.MaxValue;
        //     float minY = float.MaxValue;
        //     float minZ = float.MaxValue;
        //     float minW = float.MaxValue;
        //     float maxX = float.MinValue;
        //     float maxY = float.MinValue;
        //     float maxZ = float.MinValue;
        //     float maxW = float.MinValue;

        //     foreach (var vertex in mesh.Vertices)
        //     {
        //         minX = Mathf.Min(minX, vertex.x);
        //         minY = Mathf.Min(minY, vertex.y);
        //         minZ = Mathf.Min(minZ, vertex.z);
        //         minW = Mathf.Min(minW, vertex.w);

        //         maxX = Mathf.Max(maxX, vertex.x);
        //         maxY = Mathf.Max(maxY, vertex.y);
        //         maxZ = Mathf.Max(maxZ, vertex.z);
        //         maxW = Mathf.Max(maxW, vertex.w);
        //     }

        //     return new BoundingBox4D(
        //         new Vector4(minX, minY, minZ, minW),
        //         new Vector4(maxX, maxY, maxZ, maxW)
        //     );
        // }

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

        // /// <summary>
        // /// Test if this bounding box includes another.
        // /// </summary>
        // /// <param name="other"></param>
        // /// <returns></returns>
        // public bool Includes(BoundingBox4D other)
        // {
        //     return min.x <= other.min.x &&
        //         min.y <= other.min.y &&
        //         min.z <= other.min.z &&
        //         min.w <= other.min.w &&
        //         max.x >= other.max.x &&
        //         max.y >= other.max.y &&
        //         max.z >= other.max.z &&
        //         max.w >= other.max.w;
        // }

        // /// <summary>
        // /// SDFSDFSDFSDF.
        // /// </summary>
        // /// <param name="other"></param>
        // /// <returns></returns>
        // public BoundingBox4D Intersection(BoundingBox4D other)
        // {
        //     float minX = Mathf.Max(min.x, other.min.x);
        //     float minY = Mathf.Max(min.y, other.min.y);
        //     float minZ = Mathf.Max(min.z, other.min.z);
        //     float minW = Mathf.Max(min.w, other.min.w);

        //     float maxX = Mathf.Min(max.x, other.max.x);
        //     float maxY = Mathf.Min(max.y, other.max.y);
        //     float maxZ = Mathf.Min(max.z, other.max.z);
        //     float maxW = Mathf.Min(max.w, other.max.w);

        //     if (minX > maxX || minY > maxY ||
        //         minZ > maxZ || minW > maxW)
        //     {
        //         return null;
        //     }

        //     return new BoundingBox4D(
        //         new Vector4(minX, minY, minZ, minW),
        //         new Vector4(maxX, maxY, maxZ, maxW)
        //     );
        // }

        // /// <summary>
        // /// Test whether two bounding boxes overlap.
        // /// </summary>
        // /// <param name="other"></param>
        // /// <returns></returns>
        // public bool Overlaps(BoundingBox4D other)
        // {
        //     return Intersection(other) != null;
        // }

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

        /// <summary>
        /// Returns the closest point on the bounding box to the given point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector4 ClosestPointOnBounds(Vector4 point)
        {
            if (Includes(point))
            {
                // If the point is inside the bounding box, find the closest point on the boundary
                float[] distances = new float[8];
                distances[0] = point.x - min.x;
                distances[1] = max.x - point.x;
                distances[2] = point.y - min.y;
                distances[3] = max.y - point.y;
                distances[4] = point.z - min.z;
                distances[5] = max.z - point.z;
                distances[6] = point.w - min.w;
                distances[7] = max.w - point.w;

                int minIndex = Array.IndexOf(distances, Mathf.Min(distances));

                return minIndex switch
                {
                    0 => new Vector4(min.x, point.y, point.z, point.w),
                    1 => new Vector4(max.x, point.y, point.z, point.w),
                    2 => new Vector4(point.x, min.y, point.z, point.w),
                    3 => new Vector4(point.x, max.y, point.z, point.w),
                    4 => new Vector4(point.x, point.y, min.z, point.w),
                    5 => new Vector4(point.x, point.y, max.z, point.w),
                    6 => new Vector4(point.x, point.y, point.z, min.w),
                    7 => new Vector4(point.x, point.y, point.z, max.w),
                    _ => point,
                };
            }
            else
            {
                return new Vector4(
                    Mathf.Clamp(point.x, min.x, max.x),
                    Mathf.Clamp(point.y, min.y, max.y),
                    Mathf.Clamp(point.z, min.z, max.z),
                    Mathf.Clamp(point.w, min.w, max.w)
                );
            }
        }
    }
}
