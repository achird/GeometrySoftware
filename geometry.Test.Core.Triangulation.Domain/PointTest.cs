using geometry.Core.Triangulation.Domain.Common;
using Xunit;

namespace geometry.Test.Core.Triangulation.Domain
{
    public class PointTest
    {
        [Fact]
        public void SubtractTest()
        {
            var p1 = Point.Create(1, 5);
            var p2 = Point.Create(10, 3);

            Assert.Equal(p1 - p2, Point.Create(-9, 2));
        }

        [Fact]
        public void AddTest()
        {
            var p1 = Point.Create(1, 5);
            var p2 = Point.Create(10, 3);

            Assert.Equal(p1 + p2, Point.Create(11, 8));
        }

        [Fact]
        public void MultiplyTest()
        {
            var p = Point.Create(1, 5);

            Assert.Equal(p * 5, Point.Create(5, 25));
        }

        [Fact]
        public void DivideTest()
        {
            var p = Point.Create(10, 5);

            Assert.Equal(p / 5, Point.Create(2, 1));
        }

    }
}
