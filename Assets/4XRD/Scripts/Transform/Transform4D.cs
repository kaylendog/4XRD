using System;
using UnityEngine;

namespace _4XRD.Transform
{
    /// <summary>
    /// A four-dimensional transform.
    /// </summary>
    [Serializable]
    public struct Transform4D
    {
        /// <summary>
        /// The position in 4D space.
        /// </summary>
        public Vector4 position;

        /// <summary>
        /// The objects scale.
        /// </summary>
        public Vector4 scale;

        /// <summary>
        /// The rotation of this transform.
        /// </summary>
        public Rotation4x4 rotation;

        /// <summary>
        /// The identity transformation.
        /// </summary>
        public static Transform4D identity = new(
            Vector4.zero,
            Vector4.one,
            Rotation4x4.identity
        );

        /// <summary>
        /// Apply a transformation to a given vector.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 operator *(Transform4D transform, Vector4 v)
        {
            var scaled = new Vector4(
                v.x * transform.scale.x,
                v.y * transform.scale.y,
                v.z * transform.scale.z,
                v.w * transform.scale.w
            );
            return transform.rotation * scaled + transform.position;
        }

        /// <summary>
        /// Compose two transformations.
        /// </summary>
        /// <returns></returns>
        public static Transform4D operator *(Transform4D a, Transform4D b)
        {
            return new Transform4D(
                a.rotation * a.scale.elemMul(b.position) + a.position,
                (b.rotation.inverse * a.scale).elemMul(b.scale),
                a.rotation * b.rotation
            );
        }

        /// <summary>
        /// Construct a new Transform4D.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        public Transform4D(Vector4 position, Vector4 scale, Rotation4x4 rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
        }

        /// <summary>
        /// The inverse of this transform.
        /// </summary>
        public Transform4D inverse => new Transform4D(
            -(rotation.inverse * position),
            new Vector4(1 / scale.x, 1 / scale.y, 1 / scale.z, 1 / scale.w),
            rotation.inverse
        );

        /// <summary>
        /// Returns a string representation of the Transform4D.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Position: {position}, Scale: {scale}, Rotation: {rotation}";
        }
    }
}
