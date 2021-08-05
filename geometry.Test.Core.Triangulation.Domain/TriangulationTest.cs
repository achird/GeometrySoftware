﻿using geometry.Core.Triangulation.Domain;
using geometry.Core.Triangulation.Domain.Common;
using System.Collections.Generic;
using Xunit;

namespace geometry.Test.Core.Triangulation.Domain
{
    public class TriangulationTest
    {
        [Fact]
        public void DelaunayTriangulationTest()
        {
            var delaunay = new Delaunay(new List<Point>()
            {
                Point.Create(6, 0),
                Point.Create(3, 1),
                Point.Create(1, 4),
                Point.Create(2, 8),
                Point.Create(4, 6),
                Point.Create(2, -2),
                Point.Create(5, 2)
            });
            Assert.Equal(7, delaunay.Triangles.Count);
        }


    }
}
