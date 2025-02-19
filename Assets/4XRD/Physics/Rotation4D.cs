using UnityEngine;

namespace _4XRD.Physics
{
    public class Rotation4D
    {
        static Rotation4D RotateXY(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateXY(m._rot, angle)
            };
        }
  
        /// <summary>
        /// Rotate the matrix in the XY plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateXY(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(0, new Vector4(c, -s, 0, 0));
            m.SetColumn(1, new Vector4(s, c, 0, 0));
            return m * mat;
        }
        
        static Rotation4D RotateYZ(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateYZ(m._rot, angle)
            };
        }
        
        /// <summary>
        /// Rotate the matrix in the YZ plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateYZ(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(1, new Vector4(0, c, -s, 0));
            m.SetColumn(2, new Vector4(0, s, c, 0));
            return m * mat;
        }
        
        static Rotation4D RotateXZ(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateXZ(m._rot, angle)
            };
        }
        
        /// <summary>
        /// Rotate the matrix in the XZ plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateXZ(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(0, new Vector4(c, 0, s, 0));
            m.SetColumn(2, new Vector4(-s, 0, c, 0));
            return m * mat;
        }
        
        static Rotation4D RotateXW(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateXW(m._rot, angle)
            };
        }
        
        /// <summary>
        /// Rotate the matrix in the XW plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateXW(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(0, new Vector4(c, 0, 0, -s));
            m.SetColumn(3, new Vector4(s, 0, 0, c));
            return m * mat;
        }
        
        static Rotation4D RotateYW(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateYW(m._rot, angle)
            };
        }
        
        /// <summary>
        /// Rotate the matrix in the YW plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateYW(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(1, new Vector4(0, c, 0, -s));
            m.SetColumn(3, new Vector4(0, s, 0, c));
            return m * mat;
        }
        
        static Rotation4D RotateZW(Rotation4D m, float angle)
        {
            return new Rotation4D()
            {
                _rot = RotateZW(m._rot, angle)
            };
        }
        
        /// <summary>
        /// Rotate the matrix in the ZW plane.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static Matrix4x4 RotateZW(Matrix4x4 mat, float angle)
        {
            var m = Matrix4x4.identity;
            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            m.SetColumn(2, new Vector4(0, 0, c, s));
            m.SetColumn(3, new Vector4(0, 0, -s, c));
            return m * mat;
        }
    
        /// <summary>
        /// Return a new Rotation4D from an instance of Euler4.
        /// </summary>
        /// <param name="angles"></param>
        /// <returns></returns>
        static Rotation4D FromEuler4(Euler4 angles)
        {
            return new Rotation4D()
                .RotateXY(angles.XY)
                .RotateYZ(angles.YZ)
                .RotateXZ(angles.XZ)
                .RotateXW(angles.XW)
                .RotateYW(angles.YW)
                .RotateZW(angles.ZW);
        }

        public Vector4 RotateVector(Vector4 v)
        {
            return _rot * v;
        }
        
        /// <summary>
        /// The internal rotation matrix.
        /// </summary>
        Matrix4x4 _rot = Matrix4x4.identity;
        
        Rotation4D RotateXY(float angle)
        {
            return RotateXY(this, angle);
        }
        
        Rotation4D RotateYZ(float angle)
        {
            return RotateYZ(this, angle);
        }
        
        Rotation4D RotateXZ(float angle)
        {
            return RotateXZ(this, angle);
        }
        
        Rotation4D RotateXW(float angle)
        {
            return RotateXW(this, angle);
        }
        
        Rotation4D RotateYW(float angle)
        {
            return RotateYW(this, angle);
        }
        
        Rotation4D RotateZW(float angle)
        {
            return RotateXW(this, angle);
        }
    }
}
