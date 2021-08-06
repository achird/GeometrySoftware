using AutoMapper;
using CSharpFunctionalExtensions;
using geometry.Core.Triangulation.Application.Use.Query;
using geometry.Core.Triangulation.Domain;
using geometry.Core.Triangulation.Domain.Common;
using System;
using System.Threading.Tasks;

namespace geometry.Core.Triangulation.Application.Interactor
{
    public class FindTriangulationQueryHandler : IFindTriangulationQueryHandler
    {
        private readonly IMapper mapper;

        public FindTriangulationQueryHandler(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Result<TriangulationDto>> HandleAsync(FindTriangulationQuery query)
        {
            return 
                Task.Run(() =>
                    Delaunay
                        .CreateRandomTriangulation(mapper.Map<Point>(query.LeftUpCorner), mapper.Map<Point>(query.RightBottomCorner), query.PointCount)
                        .Map(delaunay => mapper.Map<TriangulationDto>(delaunay)));
        }
    }
}
