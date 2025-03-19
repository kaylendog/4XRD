using System;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

namespace _4XRD.Transform
{
    public class Rotation4x4
    {
        public readonly Matrix4x4 matrix;

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
                .RotateXW(euler6.XW)
                .RotateYW(euler6.YW)
                .RotateZW(euler6.ZW)
                .RotateXY(euler6.XY)
                .RotateYZ(euler6.YZ)
                .RotateXZ(euler6.XZ);
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

        /// <summary>
        /// Create a new rotation.
        /// </summary>
        public Rotation4x4()
        {
            matrix = Matrix4x4.identity;
        }

        public Rotation4x4(Matrix4x4 matrix)
        {
            this.matrix = matrix;
        }

        /// <summary>
        /// Return the inverse rotation, defined by the transpose of the rotation matrix (way quicker to compute than inverse).
        /// </summary>
        public Rotation4x4 inverse => new(matrix.transpose);

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
            return new Rotation4x4(rotationMatrix) * this;
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
            return new Rotation4x4(rotationMatrix) * this;
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
            return new Rotation4x4(rotationMatrix) * this;
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
            return new Rotation4x4(rotationMatrix) * this;
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
            return new Rotation4x4(rotationMatrix) * this;
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
            return new Rotation4x4(rotationMatrix) * this;
        }

        // public (Rotation4x4, Rotation4x4) SeparateWRotation()
        // {
        //     Matrix4x4 invRotation = matrix.transpose;
        //     Matrix4x4 B = GetInverseWRotation(invRotation);
        //     Matrix4x4 A = matrix * B.transpose;
        //     return (new Rotation4x4(A), new Rotation4x4(B));
        // }

        // private Matrix4x4 GetInverseWRotation(Matrix4x4 matrix)
        // {
        //     float a = matrix[0,3];
        //     float b = matrix[1,3];
        //     float c = matrix[2,3];
        //     float d = matrix[3,3];

        //     // Rotate ZW
        //     float norm_cd = Mathf.Sqrt(c * c + d * d);
        //     Matrix4x4 R_ZW = Matrix4x4.identity;
        //     if (norm_cd > 1e-8f)
        //     {
        //         float cos1 = d / norm_cd;
        //         float sin1 = c / norm_cd;
        //         R_ZW = new(
        //             new Vector4(1, 0,    0,     0),
        //             new Vector4(0, 1,    0,     0),
        //             new Vector4(0, 0, cos1, -sin1),
        //             new Vector4(0, 0, sin1,  cos1)
        //         );
        //     }

        //     // Rotate YW
        //     float norm_bcd = Mathf.Sqrt(b * b + norm_cd * norm_cd);
        //     Matrix4x4 R_YW = Matrix4x4.identity;
        //     if (norm_bcd > 1e-8f)
        //     {
        //         float cos2 = norm_cd / norm_bcd;
        //         float sin2 = b / norm_bcd;
        //         R_YW = new(
        //             new Vector4(1,    0, 0,     0),
        //             new Vector4(0, cos2, 0, -sin2),
        //             new Vector4(0,    0, 1,     0),
        //             new Vector4(0, sin2, 0,  cos2)
        //         );
        //     }

        //     // Rotate XW
        //     float norm_abcd = Mathf.Sqrt(a * a + norm_bcd * norm_bcd);
        //     Matrix4x4 R_XW = Matrix4x4.identity;
        //     if (norm_abcd > 1e-8f)
        //     {
        //         float cos3 = norm_bcd / norm_abcd;
        //         float sin3 = a / norm_abcd;
        //         R_XW = new(
        //             new Vector4(cos3, 0, 0, -sin3),
        //             new Vector4(   0, 1, 0,     0),
        //             new Vector4(   0, 0, 1,     0),
        //             new Vector4(sin3, 0, 0,  cos3)
        //         );
        //     }

        //     return R_XW * R_YW * R_ZW;
        // }

        public override string ToString()
        {
            return $"Rotation4x4(\n" +
                $"  {matrix.m00:F2}, {matrix.m01:F2}, {matrix.m02:F2}, {matrix.m03:F2}\n" +
                $"  {matrix.m10:F2}, {matrix.m11:F2}, {matrix.m12:F2}, {matrix.m13:F2}\n" +
                $"  {matrix.m20:F2}, {matrix.m21:F2}, {matrix.m22:F2}, {matrix.m23:F2}\n" +
                $"  {matrix.m30:F2}, {matrix.m31:F2}, {matrix.m32:F2}, {matrix.m33:F2}\n" +
                $")";
        }
    }
}
