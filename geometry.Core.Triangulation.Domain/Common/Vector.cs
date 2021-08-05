using CSharpFunctionalExtensions;
using System;

namespace geometry.Core.Triangulation.Domain.Common
{
    /// <summary>
    /// Линия
    /// </summary>
    public class Vector : ValueObject<Vector>
    {
        private Vector(Point src, Point dest)
        {
            Src = src ?? throw new ArgumentNullException(nameof(src));
            Dest = dest ?? throw new ArgumentNullException(nameof(dest));
            Offset = Dest - Src;
        }

        /// <summary>
        /// Исходная точка
        /// </summary>
        public Point Src { get; }

        /// <summary>
        /// Конечная точка
        /// </summary>
        public Point Dest { get; }

        /// <summary>
        /// Смещение относительно исходной точки
        /// </summary>
        public Point Offset { get; }

        /// <summary>
        /// Длинна линии
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(Math.Pow(Src.X - Dest.X, 2) + Math.Pow(Src.Y - Dest.Y, 2));
        }

        /// <summary>
        /// Положение точки point относительно вектора. 
        /// Возможные значения: Left - слева вектора, Right - справа вектора и Match - точка совпадает с прямой, проходящей через вектор
        /// </summary>
        /// <returns></returns>
        public Relative Position(Point point)
        {
            var multiply = this * Create(Src, point);

            if (multiply > 0) return Relative.Left;
            if (multiply < 0) return Relative.Right;

            return Relative.Match;
        }

        /// <summary>
        /// Перпендикулярный верктор по часовой стрелке
        /// </summary>
        /// <returns></returns>
        public Vector Normal()
        {
            var xc = Src.X + (Dest.X - Src.X) / 2;
            var yc = Src.Y + (Dest.Y - Src.Y) / 2;

            return Create(xc - Offset.Y / 2, yc + Offset.X / 2, xc + Offset.Y / 2, yc - Offset.X / 2);
        }

        /// <summary>
        /// Обратить вектор
        /// </summary>
        /// <returns></returns>
        public Vector Reverse()
        {
            return Create(Dest, Src);
        }

        protected override bool EqualsCore(Vector other)
        {
            return (Src, Dest) == (other.Src, other.Dest);
        }

        protected override int GetHashCodeCore()
        {
            return (Src, Dest).GetHashCode();
        }

        /// <summary>
        /// Скалярное произведение векторов v1 и v2
        /// </summary>
        /// <returns></returns>
        public static double operator ^(Vector v1, Vector v2)
        {
            return v1.Offset.X * v2.Offset.X + v1.Offset.Y * v2.Offset.Y;
        }

        /// <summary>
        /// Косое произведение векторов v1 и v2
        /// </summary>
        /// <returns></returns>
        public static double operator *(Vector v1, Vector v2)
        {
            return v1.Offset.X * v2.Offset.Y - v2.Offset.X * v1.Offset.Y;
        }

        /// <summary>
        /// Создать вектор point1 и point2
        /// </summary>
        /// <returns></returns>
        public static Vector Create(Point src, Point dest)
        {
            return new Vector(src, dest);
        }
        /// <summary>
        /// Создать вектор (x1, y1) -> (x2, y2)
        /// </summary>
        /// <returns></returns>
        public static Vector Create(double x1, double y1, double x2, double y2)
        {
            return new Vector(Point.Create(x1, y1), Point.Create(x2, y2));
        }
        /// <summary>
        /// Создать ненулевой вектор (x1, y1) -> (x2, y2)
        /// </summary>
        /// <returns></returns>
        public static Result<Vector> CreateNonZero(double x1, double y1, double x2, double y2)
        {
            var src = Point.Create(x1, y1);
            var dest = Point.Create(x2, y2);

            if (src == dest)
            {
                return Result.Failure<Vector>("Точки не должны совпадать");
            }

            return new Vector(Point.Create(x1, y1), Point.Create(x2, y2));
        }
    }
}
