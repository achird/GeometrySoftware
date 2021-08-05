using CSharpFunctionalExtensions;

namespace geometry.Core.Triangulation.Domain.Common
{
    /// <summary>
    /// Треугольник
    /// </summary>
    public class Triangle : ValueObject<Triangle>
    {
        private Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Точка A
        /// </summary>
        public Point A { get; private set; }

        /// <summary>
        /// Точка B
        /// </summary>
        public Point B { get; private set; }
        
        /// <summary>
        /// Точка C
        /// </summary>
        public Point C { get; private set; }

        protected override bool EqualsCore(Triangle other)
        {
            return (A, B, C) == (other.A, other.B, other.C);
        }

        protected override int GetHashCodeCore()
        {
            return (A, B, C).GetHashCode();
        }

        /// <summary>
        /// Создать треугольник
        /// </summary>
        /// <returns></returns>
        public static Triangle Create(Point pointA, Point pointB, Point pointC)
        {
            return new Triangle(pointA, pointB, pointC);
        }

        /// <summary>
        /// Создать треугольник
        /// </summary>
        /// <returns></returns>
        public static Triangle Create(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            return new Triangle(Point.Create(Ax, Ay), Point.Create(Bx, By), Point.Create(Cx, Cy));
        }
    }
}
