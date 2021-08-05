using CSharpFunctionalExtensions;

namespace geometry.Core.Triangulation.Domain.Common
{
    /// <summary>
    /// Точка на плоскости
    /// </summary>
    public class Point : ValueObject<Point>
    {
        private Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Координата по оси X
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Координата по оси Y
        /// </summary>
        public double Y { get; private set; }

        protected override bool EqualsCore(Point other)
        {
            return (X, Y) == (other.X, other.Y);
        }

        protected override int GetHashCodeCore()
        {
            return (X, Y).GetHashCode();
        }

        /// <summary>
        /// Сложить две точки
        /// </summary>
        /// <returns></returns>
        public static Point operator +(Point p1, Point p2)
        {
            return Create(p1.X + p2.X, p1.Y + p2.Y);
        }

        /// <summary>
        /// Вычесть из одной точки другую
        /// </summary>
        /// <returns></returns>
        public static Point operator -(Point p1, Point p2)
        {
            return Create(p1.X - p2.X, p1.Y - p2.Y);
        }

        /// <summary>
        /// Умножить координаты точки
        /// </summary>
        /// <returns></returns>
        public static Point operator *(Point p, double multiply)
        {
            return Create(p.X * multiply, p.Y * multiply);
        }

        /// <summary>
        /// Разделить координаты точки
        /// </summary>
        /// <returns></returns>
        public static Point operator /(Point p, double divider)
        {
            return Create(p.X / divider, p.Y / divider);
        }

        /// <summary>
        /// Создать точку с координатами (x, y)
        /// </summary>
        /// <returns></returns>
        public static Point Create(double x, double y)
        {
            return new Point(x, y);
        }
    }
}
