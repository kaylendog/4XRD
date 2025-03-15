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
        }
    }
}