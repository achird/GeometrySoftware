using System.Collections.Generic;

namespace geometry.Core.Triangulation.Application.Use.Query
{
    public class TriangulationDto
    {
        public IList<PointDto> Points { get; set; }
        public IList<TriangleDto> Triangles { get; set; }
    }
}
