﻿using AutoMapper;
using geometry.Core.Triangulation.Application.Use.Query;
using geometry.Core.Triangulation.Domain;
using geometry.Core.Triangulation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<TriangulationDto> HandleAsync(FindTriangulationQuery query)
        {
            var leftUp = mapper.Map<Point>(query.LeftUpCorner);
            var rightBottom = mapper.Map<Point>(query.RightBottomCorner);
            var points = new List<Point>();
            var random = new Random();
            points.AddRange(
                from position in Enumerable.Range(1, query.PointCount)
                select Point.Create(
                    leftUp.X + (rightBottom.X - leftUp.X) * random.NextDouble(),
                    leftUp.Y + (rightBottom.Y - leftUp.Y) * random.NextDouble())
                );
            var triangulation = new Delaunay();
            return Task.FromResult(
                new TriangulationDto()
                {
                    Points = mapper.Map<IList<PointDto>>(points),
                    Triangles = mapper.Map<IList<TriangleDto>>(triangulation.Triangulation(points))
                });
        }
    }
}
