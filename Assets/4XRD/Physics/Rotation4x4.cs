using System;
using UnityEngine;

namespace _4XRD.Physics
{
    [Serializable]
    public class Rotation4x4
    {
        readonly Matrix4x4 _matrix;
        
        /// <summary>
        /// The identity rotation.
        /// </summary>
        public static Rotation4x4 identity => new();

        /// <summary>
        /// Construct a rotation from Euler angles.
        /// </summary>
        /// <param name="euler6"></param>
        /// <returns></returns>
        public static Rotation4x4 FromAngles(Euler6 euler6)
        {
            Rotation4x4 rotation = new();
            return rotation
                .RotateXY(euler6.XY)
                .RotateXZ(euler6.XZ)
                .RotateXW(euler6.XW)
                .RotateYZ(euler6.YZ)
                .RotateYW(euler6.YW)
                .RotateZW(euler6.ZW);
        }
        
        public static Rotation4x4 operator *(Rotation4x4 a, Rotation4x4 b)
        {
            Matrix4x4 result = a._matrix * b._matrix;
            return new Rotation4x4(result);
        }

        public static Vector4 operator *(Rotation4x4 rotation, Vector4 vector)
        {
            return rotation._matrix * vector;
        }
        
        /// <summary>
        /// Create a new rotation.
        /// </summary>
        public Rotation4x4()
        {
            _matrix = Matrix4x4.identity;
        }

        public Rotation4x4(Matrix4x4 matrix)
        {
            _matrix = matrix;
        }

        /// <summary>
        /// Return the inverse rotation.
        /// </summary>
        public Rotation4x4 inverse => new Rotation4x4(_matrix.inverse);

        public Rotation4x4 RotateXY(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(cos, -sin, 0, 0),
                new Vector4(sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

        public Rotation4x4 RotateXZ(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(cos, 0, -sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(sin, 0, cos, 0),
                new Vector4(0, 0, 0, 1)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

        public Rotation4x4 RotateXW(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(cos, 0, 0, -sin),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(sin, 0, 0, cos)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

        public Rotation4x4 RotateYZ(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, -sin, 0),
                new Vector4(0, sin, cos, 0),
                new Vector4(0, 0, 0, 1)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

        public Rotation4x4 RotateYW(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, 0, -sin),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, sin, 0, cos)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

        public Rotation4x4 RotateZW(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            Matrix4x4 rotationMatrix = new(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, cos, -sin),
                new Vector4(0, 0, sin, cos)
            );
            return this * new Rotation4x4(rotationMatrix);
        }

      
    }
}
