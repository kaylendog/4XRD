using System.Collections.Generic;
using System.Linq;
using Vector4 = _4XRD.Physics.Tensors.Vector4;

// Makes records work
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}

namespace _4XRD.Mesh.MeshBuilder
{
    public class Mesh4DBuilder : IMesh4DBuilder
    {
        private readonly List<Vector4> vertices = new();
        private readonly HashSet<Edge> edges = new();
        private readonly HashSet<Face> faces = new();
        private readonly HashSet<Cell> cells = new();
        
        public Mesh4D Build()
        {
            Vector4[] vertices = new Vector4[this.vertices.Count];
            int[] edges = new int[this.edges.Count * 2];
            int[] faces = new int[this.faces.Count * 3];
            int[] cells = new int[this.cells.Count * 4];

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
            
            return Mesh4D.CreateInstance(vertices, edges, faces, cells);
        }

        public int AddVertex(float x, float y, float z, float w)
        {
            vertices.Add(new Vector4(x, y, z, w));
            return vertices.Count - 1;
        }

        public int AddVertex(Vector4 vertex)
        {
            vertices.Add(vertex);
            return vertices.Count - 1;
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            List<int> vertices = new() { vertex1, vertex2 };
            vertices.Sort();
            edges.Add(new Edge(vertices[0], vertices[1]));
        }

        public void AddFace(int vertex1, int vertex2, int vertex3)
        {
            List<int> vertices = new() { vertex1, vertex2, vertex3 };
            vertices.Sort();
            faces.Add(new Face(vertices[0], vertices[1], vertices[2]));
        }

        public void AddCell(int vertex1, int vertex2, int vertex3, int vertex4)
        {
            List<int> vertices = new() { vertex1, vertex2, vertex3, vertex4 };
            vertices.Sort();
            cells.Add(new Cell(vertices[0], vertices[1], vertices[2], vertices[3]));
        }
    }
    public record Edge (int Vertex1, int Vertex2);

    public record Face (int Vertex1, int Vertex2, int Vertex3);

    public record Cell (int Vertex1, int Vertex2, int Vertex3, int Vertex4);
}