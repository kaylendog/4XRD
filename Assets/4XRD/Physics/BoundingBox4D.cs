using _4XRD.Mesh;
using UnityEngine;

namespace _4XRD.Physics
{
    public record BoundingBox4D
    {
        /// <summary>
        /// The first corner of the bounding box.
        /// </summary>
        public readonly Vector4 Min;

        /// <summary>
        /// The second corner of the bounding box.
        /// </summary>
        public readonly Vector4 Max;

        /// <summary>
        /// The middle of the bounding box.
        /// </summary>
        public Vector4 center
        {
            get => Vector4.Lerp(Min, Max, 0.5f);
        }

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
                minX = Mathf.Min(minX, vertex.X);
                minY = Mathf.Min(minY, vertex.Y);
                minZ = Mathf.Min(minZ, vertex.Z);
                minW = Mathf.Min(minW, vertex.W);

                maxX = Mathf.Max(maxX, vertex.X);
                maxY = Mathf.Max(maxY, vertex.Y);
                maxZ = Mathf.Max(maxZ, vertex.Z);
                maxW = Mathf.Max(maxW, vertex.W);
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
            return Min.X <= other.Min.X &&
                Min.Y <= other.Min.Y &&
                Min.Z <= other.Min.Z &&
                Min.W <= other.Min.W &&
                Max.X >= other.Max.X &&
                Max.Y >= other.Max.Y &&
                Max.Z >= other.Max.Z &&
                Max.W >= other.Max.W;
        }

        /// <summary>
        /// SDFSDFSDFSDF.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public BoundingBox4D Intersection(BoundingBox4D other)
        {
            float minX = Mathf.Max(Min.X, other.Min.X);
            float minY = Mathf.Max(Min.Y, other.Min.Y);
            float minZ = Mathf.Max(Min.Z, other.Min.Z);
            float minW = Mathf.Max(Min.W, other.Min.W);

            float maxX = Mathf.Min(Max.X, other.Max.X);
            float maxY = Mathf.Min(Max.Y, other.Max.Y);
            float maxZ = Mathf.Min(Max.Z, other.Max.Z);
            float maxW = Mathf.Min(Max.W, other.Max.W);
            
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
