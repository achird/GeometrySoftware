using CSharpFunctionalExtensions;
using geometry.Core.Application.Sqrs;

namespace geometry.Core.Triangulation.Application.Use.Query
{
    /// <summary>
    /// Получить триангуляцию для случайного набора из N-точек
    /// </summary>
    public class FindTriangulationQuery : IQuery<Result<TriangulationDto>>
    {
        /// <summary>
        /// Количество точек в наборе
        /// </summary>
        public int PointCount { get; set; }

        /// <summary>
        /// Координата левого верхнего угла поля
        /// </summary>
        public PointDto LeftUpCorner { get; set; }

        /// <summary>
        /// Координата правого нижнего угла поля
        /// </summary>
        public PointDto RightBottomCorner { get; set; }

    }
}
