using geometry.Core.Application.Sqrs;
using System.Collections.Generic;

namespace geometry.Core.Triangulation.Application.Use.Query
{
    /// <summary>
    /// Найти триангуляцию для случайного набора точек
    /// </summary>
    public interface IFindTriangulationQueryHandler : IQueryHandler<FindTriangulationQuery, TriangulationDto>
    {

    }
}
