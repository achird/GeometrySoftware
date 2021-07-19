using CSharpFunctionalExtensions;
using geometry.Core.Application.Sqrs;

namespace geometry.Core.Triangulation.Application.Use.Query
{
    /// <summary>
    /// Найти триангуляцию для случайного набора точек
    /// </summary>
    public interface IFindTriangulationQueryHandler : IQueryHandler<FindTriangulationQuery, Result<TriangulationDto>>
    {

    }
}
