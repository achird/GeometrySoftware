using System.Collections.Generic;

namespace geometry.Core.Triangulation.Application.Use.Query
{
    /// <summary>
    /// Данные о триангуляции
    /// </summary>
    public class TriangulationDto
    {
        /// <summary>
        /// Точки
        /// </summary>
        public IList<PointDto> Points { get; set; }
        /// <summary>
        /// Треугольники
        /// </summary>
        public IList<TriangleDto> Triangles { get; set; }
    }
}
