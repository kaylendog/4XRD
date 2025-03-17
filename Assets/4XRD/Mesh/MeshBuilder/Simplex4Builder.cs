using UnityEngine;

namespace _4XRD.Mesh.MeshBuilder
{
    public class Simplex4Builder : Mesh4DBuilder
    {
        public new Mesh4D Build()
        {   
            // Cube 1 face down
            int point0 = AddVertex( 1,  1,  1, -1/Mathf.Sqrt(5));
            int point1 = AddVertex( 1, -1, -1, -1/Mathf.Sqrt(5));
            int point2 = AddVertex(-1,  1, -1, -1/Mathf.Sqrt(5));
            int point3 = AddVertex(-1, -1,  1, -1/Mathf.Sqrt(5));
            int point4 = AddVertex( 0,  0,  0,  4/Mathf.Sqrt(5));

            AddEdge(point0, point1);
            AddEdge(point0, point2);
            AddEdge(point0, point3);
            AddEdge(point0, point4);
            AddEdge(point1, point2);
            AddEdge(point1, point3);
            AddEdge(point1, point4);
            AddEdge(point2, point3);
            AddEdge(point2, point4);
            AddEdge(point3, point4);

            // AddFace(point0, point1, point2);
            // AddFace(point0, point1, point3);
            // AddFace(point0, point1, point4);
            // AddFace(point0, point2, point3);
            // AddFace(point0, point2, point4);
            // AddFace(point0, point3, point4);
            // AddFace(point1, point2, point3);
            // AddFace(point1, point2, point4);
            // AddFace(point1, point3, point4);
            // AddFace(point2, point3, point4);

            // AddCell(point0, point1, point2, point3);
            // AddCell(point0, point1, point2, point4);
            // AddCell(point0, point1, point3, point4);
            // AddCell(point0, point2, point3, point4);
            // AddCell(point1, point2, point3, point4);

            return base.Build();
        }
    }
}