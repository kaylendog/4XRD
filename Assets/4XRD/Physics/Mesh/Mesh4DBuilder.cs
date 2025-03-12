using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}

namespace _4XRD.Physics.Mesh
{
    public class Mesh4DBuilder : IPrimitiveMesh4DBuilder
    {
        readonly List<Vertex4> vertices = new();
        readonly HashSet<Edge> edges = new();
        readonly HashSet<Face> faces = new();
        readonly HashSet<Cell> cells = new();
        
        public Mesh4D Build()
        {
            Vector4[] vertices = new Vector4[this.vertices.Count];
            int[] edges = new int[this.edges.Count];
            int[] faces = new int[this.faces.Count];
            int[] cells = new int[this.cells.Count];

            foreach (var (vertex, index) in this.vertices.Select((vertex, index) => (vertex, index)))
            {
                vertices[index] = new Vector4(vertex.X, vertex.Y, vertex.Z, vertex.W);
            }

            foreach (var (edge, index) in this.edges.Select((edge, index) => (edge, index)))
            {
                edges[index * 2] = edge.Vertex1;
                edges[index * 2 + 1] = edge.Vertex2;
            }

            foreach (var (face, index) in this.faces.Select((face, index) => (face, index)))
            {
                faces[index * 3] = face.Vertex1;
                faces[index * 3 + 1] = face.Vertex2;
                faces[index * 3 + 2] = face.Vertex3;
            }

            foreach (var (cell, index) in this.cells.Select((cell, index) => (cell, index)))
            {
                cells[index * 4] = cell.Vertex1;
                cells[index * 4 + 1] = cell.Vertex2;
                cells[index * 4 + 2] = cell.Vertex3;
                cells[index * 4 + 3] = cell.Vertex4;
            }

            UnityEngine.Mesh mesh = new UnityEngine.Mesh();
            return new Mesh4D(vertices, edges, faces, cells);
        }

        public int AddVertex(float x, float y, float z, float w)
        {
            vertices.Add(new Vertex4(x, y, z, w));
            return vertices.Count - 1;
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            edges.Add(new Edge(vertex1, vertex2));
        }

        public void AddFace(int vertex1, int vertex2, int vertex3)
        {
            faces.Add(new Face(vertex1, vertex2, vertex3));
        }

        public void AddCell(int vertex1, int vertex2, int vertex3, int vertex4)
        {
            cells.Add(new Cell(vertex1, vertex2, vertex3, vertex4));
        }
    }

    public record Vertex4(float X, float Y, float Z, float W);

    public record Edge (int Vertex1, int Vertex2);

    public record Face (int Vertex1, int Vertex2, int Vertex3);

    public record Cell (int Vertex1, int Vertex2, int Vertex3, int Vertex4);
}