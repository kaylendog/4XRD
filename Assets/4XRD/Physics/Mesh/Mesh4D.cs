using System;
using UnityEngine;

namespace _4XRD.Physics.Mesh
{
    public class Mesh4D : ScriptableObject
    {

        /// <summary>
        ///     Vertices of the 4D mesh.
        /// </summary>
        public Vector4[] vertices;

        /// <summary>
        ///     Array of edge vertex indices.
        /// </summary>
        public int[] edges;

        /// <summary>
        ///     Array of triangle vertex indices.
        /// </summary>
        public int[] faces;

        /// <summary>
        ///     Array of tetrahedral cell vertex indices.
        /// </summary>
        public int[] cells;

        /// <summary>
        ///     Construct a new mesh with the given data.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="edges"></param>
        /// <param name="faces"></param>
        /// <param name="cells"></param>
        public Mesh4D(
            Vector4[] vertices,
            int[] edges,
            int[] faces,
            int[] cells
        )
        {
            this.vertices = vertices;
            this.edges = edges;
            this.faces = faces;
            this.cells = cells;
        }

        /// <summary>
        ///     Create a primitive object of the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Mesh4D CreatePrimitive(PrimitiveType4D type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets a 3D slice of the 4D mesh at the given w value.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        UnityEngine.Mesh GetSlice(float w)
        {
            throw new NotImplementedException();
        }
    }
}
