using System;
using UnityEngine;

namespace _4XRD.Transform
{
    /// <summary>
    /// A four-dimensional transform.
    /// </summary>
    [Serializable]
    public class Transform4D
    {
        /// <summary>
        /// The position in 4D space.
        /// </summary>
        [field: SerializeField]
        public Vector4 position { get; private set; }

        /// <summary>
        /// The objects scale.
        /// </summary>
        [field: SerializeField]
        public Vector4 scale { get; private set; }

        /// <summary>
        /// The rotation of this transform.
        /// </summary>
        [field: SerializeField]
        public Euler6 eulerAngles { get; private set; }

        /// <summary>
        /// The identity transformation.
        /// </summary>
        public static Transform4D identity = new(
            Vector4.zero,
            Vector4.one,
            Euler6.zero
        );

        // /// <summary>
        // /// Compose two transformations.
        // /// </summary>
        // /// <returns></returns>
        // public static Transform4D operator *(Transform4D a, Transform4D b)
        // {
        //     return new Transform4D(
        //         a.rotation * a.scale.elemMul(b.position) + a.position,
        //         (b.rotation.inverse * a.scale).elemMul(b.scale),
        //         a.rotation * b.rotation
        //     );
        // }

        /// <summary>
        /// Construct a new Transform4D.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        public Transform4D(Vector4 position, Vector4 scale, Euler6 eulerAngles)
        {
            this.position = position;
            this.scale = scale;
            this.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// Apply a transformation to a given vector.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector4 Apply(Vector4 v)
        {
            var scaled = new Vector4(
                v.x * scale.x,
                v.y * scale.y,
                v.z * scale.z,
                v.w * scale.w
            );
            return GetRotation() * scaled + position;
        }

        /// <summary>
        /// The inverse of this transform.
        /// </summary>
        public Vector4 ApplyInverse(Vector4 v)
        {
            Vector4 scaleInverse = new(
                1 / scale.x,
                1 / scale.y,
                1 / scale.z,
                1 / scale.w
            );
            return scaleInverse.ElemMul(GetRotation().inverse * (v - position));
        }

        public Rotation4x4 GetRotation()
        {
            return Rotation4x4.FromAngles(eulerAngles);
        }

        public bool IsUniformScale()
        {
            return scale.x == scale.y && scale.y == scale.z && scale.z == scale.w;
        }
        
        /// <summary>
        /// Returns a string representation of the Transform4D.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Position: {position}, Scale: {scale}, Euler Angles: {eulerAngles}";
        }
    }
}
