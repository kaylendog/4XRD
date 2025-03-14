using System;
using System.Collections.Generic;
using System.Linq;
using MIConvexHull;
using UnityEngine;
using _4XRD.Physics;
using Vector4 = _4XRD.Physics.Vector4;

namespace _4XRD.Mesh
{
    [Serializable]
    public class Mesh4D : ScriptableObject
    {

        /// <summary>
        ///     Vertices of the 4D mesh.
        /// </summary>
        public readonly Vector4[] Vertices;

        /// <summary>
        ///     Array of edge vertex indices.
        /// </summary>
        public readonly int[] Edges;

        /// <summary>
        ///     Array of triangle vertex indices.
        /// </summary>
        public readonly int[] Faces;

        /// <summary>
        ///     Array of tetrahedral cell vertex indices.
        /// </summary>
        public readonly int[] Cells;

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
            Vertices = vertices;
            Edges = edges;
            Faces = faces;
            Cells = cells;
        }

        /// <summary>
        ///     Create a primitive object of the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Mesh4D CreatePrimitive(PrimitiveType4D type)
        {
            switch(type) 
            {
                case PrimitiveType4D.Tesseract:
                    return new TesseractBuilder().Build();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets a 3D slice of the 4D mesh at the given w value.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public UnityEngine.Mesh GetSlice(Transform4D transform, float w)
        {
            Rotor4 rotation = transform.rotation;

            List<Vector4> vertices = new();
            for (int i = 0; i < Edges.Length; i+=2)
            {
                Vector4 v1 = rotation * Vertices[Edges[i]] + Vector4.Ana * transform.position.W;
                Vector4 v2 = rotation * Vertices[Edges[i + 1]] + Vector4.Ana * transform.position.W;

                AddIntersection(v1, v2, w, vertices);
            }

            if (vertices.Count < 3)
            {
                return null;
            }

            return GenerateMesh(vertices);
        }

        void AddIntersection(Vector4 v1, Vector4 v2, float w, List<Vector4> vertices)
        {
            if (Mathf.Approximately(v1.W, w) && Mathf.Approximately(v2.W, w))
            {
                vertices.Add(v1);
                vertices.Add(v2);
                return;
            }
            
            if (Mathf.Approximately(v1.W, v2.W))
            {
                return;
            }

            float t = (w - v1.W) / (v2.W - v1.W);
            if (t < 0 || t > 1)
            {
                return;
            }
            Vector4 v = Vector4.Lerp(v1, v2, t);
            vertices.Add(v);
        }

        UnityEngine.Mesh GenerateMesh(List<Vector4> vertices)
        {   
            Vertex[] vertices4 = new Vertex[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices4[i] = new Vertex(vertices[i]);
            }
            var resultObject = ConvexHull.Create(vertices4);
            if (resultObject.Outcome != ConvexHullCreationResultOutcome.Success)
            {
                Debug.LogError("Failed to create convex hull: " + resultObject.ErrorMessage);
                return null;
            }
            var result = resultObject.Result;

            Vector3[] vertices3 = new Vector3[result.Faces.Count() * 3];
            int[] triangles = new int[result.Faces.Count() * 3];

            int j = 0;
            foreach (var face in result.Faces)
            {
                vertices3[j] = new Vector3(
                    (float)face.Vertices[0].Position[0],
                    (float)face.Vertices[0].Position[1],
                    (float)face.Vertices[0].Position[2]
                );
                triangles[j] = j;
                j++;

                vertices3[j] = new Vector3(
                    (float)face.Vertices[1].Position[0],
                    (float)face.Vertices[1].Position[1],
                    (float)face.Vertices[1].Position[2]
                );
                triangles[j] = j;
                j++;

                vertices3[j] = new Vector3(
                    (float)face.Vertices[2].Position[0],
                    (float)face.Vertices[2].Position[1],
                    (float)face.Vertices[2].Position[2]
                );
                triangles[j] = j;
                j++;
            }

            UnityEngine.Mesh mesh = new()
            {
                vertices = vertices3,
                triangles = triangles,
            };

            mesh.RecalculateNormals();
            return mesh;
        }
    }

    class Vertex : IVertex
    {
        public double[] Position { get; }
        public Vertex(Vector4 v)
        {
            Position = new double[] {v.X, v.Y, v.Z};
        }
    }
}
