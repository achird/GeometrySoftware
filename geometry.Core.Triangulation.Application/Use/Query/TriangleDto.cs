namespace geometry.Core.Triangulation.Application.Use.Query
{
    /// <summary>
    /// Треугольник, заданный координатами 3 вершин
    /// </summary>
    public class TriangleDto
    {
        /// <summary>
        /// Координата точка A
        /// </summary>
        public PointDto A { get; set; }
        /// <summary>
        /// Координата точка B
        /// </summary>
        public PointDto B { get; set; }
        /// <summary>
        /// Координата точка C
        /// </summary>
        public PointDto C { get; set; }
    }
}
