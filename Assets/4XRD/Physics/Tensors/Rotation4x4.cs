using System;
using UnityEngine;

namespace _4XRD.Physics.Tensors
{
    public class Rotation4x4
    {
        private readonly Matrix4x4 matrix;

        public Rotation4x4()
        {
            matrix = Matrix4x4.identity;
        }

        public Rotation4x4(Matrix4x4 matrix)
        {
            this.matrix = matrix;
        }

        public static Rotation4x4 identity => new();

        public static Rotation4x4 FromAngles(Rotation6 rotation6)
        {
            Rotation4x4 rotation = new();
            rotation = rotation
                .RotXY(rotation6.XY)
                .RotXZ(rotation6.XZ)
                .RotXW(rotation6.XW)
                .RotYZ(rotation6.YZ)
                .RotYW(rotation6.YW)
                .RotZW(rotation6.ZW);
            return rotation;
        }

        public Rotation4x4 RotXY(float angle)
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

        public Rotation4x4 RotXZ(float angle)
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

        public Rotation4x4 RotXW(float angle)
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

        public Rotation4x4 RotYZ(float angle)
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

        public Rotation4x4 RotYW(float angle)
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

        public Rotation4x4 RotZW(float angle)
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

        public static Rotation4x4 operator *(Rotation4x4 a, Rotation4x4 b)
        {
            Matrix4x4 result = a.matrix * b.matrix;
            return new Rotation4x4(result);
        }

        public static Vector4 operator *(Rotation4x4 rotation, Vector4 vector)
        {
            return rotation.matrix * vector;
        }
    }
}
