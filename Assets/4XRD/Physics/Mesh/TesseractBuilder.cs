namespace _4XRD.Physics.Mesh
{
    public class TesseractBuilder : Mesh4DBuilder
    {
        
        public new Mesh4D Build()
        {   
            // Cube 1 face down
            int point0 = AddVertex(-1, -1, -1, -1);
            int point1 = AddVertex(-1, -1,  1, -1);
            int point2 = AddVertex( 1, -1, -1, -1);
            int point3 = AddVertex( 1, -1,  1, -1);

            // Cube 1 Face up
            int point4 = AddVertex(-1, 1,  1, -1);
            int point5 = AddVertex(-1, 1, -1, -1);
            int point6 = AddVertex( 1, 1,  1, -1);
            int point7 = AddVertex( 1, 1, -1, -1);

            // Cube 2 face down
            int point8 = AddVertex(-1, -1, -1, 1);
            int point9 = AddVertex( 1, -1, -1, 1);
            int point10 = AddVertex(-1, -1,  1, 1);
            int point11 = AddVertex( 1, -1,  1, 1);

            // Face up
            int point12 = AddVertex(-1, 1, -1, 1);
            int point13 = AddVertex( 1, 1, -1, 1);
            int point14 = AddVertex(-1, 1,  1, 1);
            int point15 = AddVertex( 1, 1,  1, 1);

            AddCubeEdgeFaceCell(point0, point1, point2, point3, point4, point5, point6, point7);
            AddCubeEdgeFaceCell(point8, point9, point10, point11, point12, point13, point14, point15);
            AddCubeEdgeFaceCell(point0, point1, point4, point5, point8, point9, point12, point13);
            AddCubeEdgeFaceCell(point2, point3, point6, point7, point10, point11, point14, point15);
            AddCubeEdgeFaceCell(point0, point2, point4, point6, point8, point10, point12, point14);
            AddCubeEdgeFaceCell(point1, point3, point5, point7, point9, point11, point13, point15);
            AddCubeEdgeFaceCell(point0, point1, point2, point3, point8, point9, point10, point11);
            AddCubeEdgeFaceCell(point4, point5, point6, point7, point12, point13, point14, point15);

            return base.Build();
        }

        void AddCubeEdgeFaceCell(
            int point0,
            int point1,
            int point2,
            int point3,
            int point4,
            int point5,
            int point6,
            int point7
        )
        {
            // Edges
            AddEdge(point0, point1);
            AddEdge(point2, point3);
            AddEdge(point0, point2);
            AddEdge(point1, point3);
            AddEdge(point1, point2);

            AddEdge(point4, point5);
            AddEdge(point6, point7);
            AddEdge(point4, point6);
            AddEdge(point5, point7);
            AddEdge(point5, point6);

            AddEdge(point0, point4);
            AddEdge(point1, point5);
            AddEdge(point2, point6);
            AddEdge(point3, point7);
            AddEdge(point0, point5);
            AddEdge(point3, point5);
            AddEdge(point2, point4);
            AddEdge(point2, point7);

            // Counter Clockwise Winding
            AddFace(point0, point2, point1);
            AddFace(point1, point2, point3);
            AddFace(point4, point5, point6);
            AddFace(point5, point7, point6);
            AddFace(point0, point4, point2);
            AddFace(point2, point4, point6);
            AddFace(point1, point3, point5);
            AddFace(point3, point7, point5);
            AddFace(point0, point1, point5);
            AddFace(point0, point5, point4);
            AddFace(point2, point7, point3);
            AddFace(point2, point6, point7);

            AddCell(point0, point1, point2, point5);
            AddCell(point1, point3, point5, point2);
            AddCell(point4, point0, point5, point2);
            AddCell(point3, point7, point2, point5);
            AddCell(point6, point4, point5, point2);
            AddCell(point7, point6, point5, point2);
        }
    }
}