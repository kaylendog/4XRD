using MIConvexHull;
using UnityEngine;

namespace _4XRD.Mesh.MeshBuilder
{
    public class HypersphereBuilder : Mesh4DBuilder
    {
        public new Mesh4D Build()
        {
            int NUM_VERTICES = 600;
            Vertex[] vertices4 = new Vertex[NUM_VERTICES];
            for (int i=0; i<NUM_VERTICES; i++){
                vertices4[i] = new Vertex(SampleHypersphere());
            }

            var resultObject = ConvexHull.Create(vertices4);
            if (resultObject.Outcome != ConvexHullCreationResultOutcome.Success)
            {
                Debug.LogError("Failed to create convex hull: " + resultObject.ErrorMessage);
                return null;
            }
            var result = resultObject.Result;

            foreach (var cell in result.Faces)
            {
                int point0 = AddVertex(cell.Vertices[0].ToVector4());
                int point1 = AddVertex(cell.Vertices[1].ToVector4());
                int point2 = AddVertex(cell.Vertices[2].ToVector4());
                int point3 = AddVertex(cell.Vertices[3].ToVector4());

                AddEdge(point0, point1);
                AddEdge(point0, point2);
                AddEdge(point0, point3);
                AddEdge(point1, point2);
                AddEdge(point1, point3);
                AddEdge(point2, point3);

                AddFace(point0, point1, point2);
                AddFace(point0, point1, point3);
                AddFace(point0, point2, point3);
                AddFace(point1, point2, point3);

                AddCell(point0, point1, point2, point3);
            }

            return base.Build();
        }

        Vector4 SampleHypersphere()
        {
            float x = SampleGaussian();
            float y = SampleGaussian();
            float z = SampleGaussian();
            float w = SampleGaussian();
            float rms = Mathf.Sqrt(x*x + y*y + z*z + w*w);
            return new Vector4(x/rms, y/rms, z/rms, w/rms);
        }

        // Use Marsaglia Polar Method to sample gaussian from uniform
        float SampleGaussian() {
            float v1 = 0;
            float v2 = 0;
            float s = 0;

            while (s <= 0 || s >= 1 ) {
                v1 = 2 * Random.Range(0f, 1f) - 1;
                v2 = 2 * Random.Range(0f, 1f) - 1;
                s = v1 * v1 + v2 * v2;
            }
            
            return v1 * Mathf.Sqrt(-2 * Mathf.Log(s) / s);
        }

        internal class Vertex : IVertex
        {
            public double[] Position { get; }
            public Vertex(Vector4 v)
            {
                Position = new double[] {v.x, v.y, v.z, v.w};
            }

            public Vector4 ToVector4()
            {
                return new Vector4((float)Position[0], (float)Position[1], (float)Position[2], (float)Position[3]);
            }
        }
    }
}