using geometry.Core.Triangulation.Domain.Common;
using Xunit;

namespace geometry.Test.Core.Triangulation.Domain
{
    public class VectorTest
    {
        [Fact]
        public void CreateTest()
        {
            var vector1 = Vector.Create(0, 3, 2, 1);
            var vector2 = Vector.Create(Point.Create(0, 3), Point.Create(2, 1));
            var resultVector3 = Vector.CreateNonZero(Point.Create(0, 0), Point.Create(0, 0));
            var resultVector4 = Vector.CreateNonZero(Point.Create(0, 0), Point.Create(1, 1));

            Assert.Equal(vector1, vector2);
            Assert.Equal(vector1, vector2);
            Assert.True(resultVector3.IsFailure);
            Assert.True(resultVector4.IsSuccess);
        }

        [Fact]
        public void OperatorTest()
        {
            var vector1 = Vector.Create(0, 3, 2, 1);
            var vector2 = Vector.Create(0, 3, 2, 0);

            Assert.Equal(2 * (-3) - (-2) * 2, vector1 * vector2);
            Assert.Equal(2 * 2 + (-2) * (-3), vector1 ^ vector2);
        }

        [Fact]
        public void LengthTest()
        {
            var length = Vector.Create(1, 1, 1, 4).Length();

            Assert.Equal(3.0d, length);
        }

        [Fact]
        public void PositionTest()
        {
            var vector = Vector.Create(0, 3, 2, 1);
            var above = Point.Create(1, 4);
            var below = Point.Create(1, 1);
            var match = Point.Create(2, 1);

            // above
            Assert.Equal(Relative.Left, vector.Position(above));

            // below
            Assert.Equal(Relative.Right, vector.Position(below));

            // match
            Assert.Equal(Relative.Match, vector.Position(match));
        }

        [Fact]
        public void NormalTest()
        {
            var vector1 = Vector.Create(0, 3, 2, 1);
            var vector2 = Vector.Create(1, 1, 1, 4);

            // act
            var perp1 = vector1.Normal();
            var perp2 = vector2.Normal();
            var reversePerp1 = perp1.Reverse();
            var reversePerp2 = perp2.Reverse();

            // check
            Assert.Equal(0, vector1 ^ perp1);
            Assert.Equal(0, vector2 ^ perp2);
            Assert.Equal(0, vector1 ^ reversePerp1);
            Assert.Equal(0, vector2 ^ reversePerp2);
            Assert.NotEqual(perp1, reversePerp1);
            Assert.NotEqual(perp2, reversePerp2);
        }

        [Fact]
        public void IsIntersectTest()
        {
            var vector1 = Vector.Create(1, 1, 1, 4);
            var vector2 = Vector.Create(0, 3, 2, 1);
            var vector3 = Vector.Create(0, 2, 2, 0);
            var vector4 = Vector.Create(1, 4, 3, 4);

            // intersect
            Assert.True(vector1.IsIntersect(vector2));
            Assert.True(vector1.IsIntersect(vector4));

            // parallel
            Assert.False(vector2.IsIntersect(vector3));

            // no intersect
            Assert.False(vector2.IsIntersect(vector4));
        }

        [Fact]
        public void IntersectTest()
        {
            var vector1 = Vector.Create(0, 3, 2, 1);
            var vector2 = Vector.Create(1, 1, 1, 4);
            var vector3 = Vector.Create(-1, 0, 3, 1);
            var vector4 = Vector.Create(0, 2, 3, 2);

            // check
            Assert.Equal(Point.Create(1, 2), vector1.Intersect(vector2));
            Assert.Equal(Point.Create(1, 2), vector2.Intersect(vector1));
            Assert.Equal(Point.Create(1, 2), vector2.Intersect(vector4));
            Assert.Equal(Point.Create(1, 0.5d), vector2.Intersect(vector3));
            Assert.Equal(Point.Create(2.2d, 0.8d), vector1.Intersect(vector3));
        }
    }
}
