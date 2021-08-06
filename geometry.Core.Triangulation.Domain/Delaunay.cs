using CSharpFunctionalExtensions;
using geometry.Core.Triangulation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace geometry.Core.Triangulation.Domain
{
    
    /// <summary>
    /// Триангуляция Делоне
    /// </summary>
    public class Delaunay : ValueObject<Delaunay>
    {
        private Delaunay(List<Point> points)
        {
            Points = points;
            Triangles = Triangulation(points);
        }

        /// <summary>
        /// Исходный набор точек
        /// </summary>
        public IReadOnlyList<Point> Points { get; }

        /// <summary>
        /// Список треугольников, формирующих триангуляцию
        /// </summary>
        public IReadOnlyList<Triangle> Triangles { get; }

        /// <summary>
        /// Получить список треугольников, формирующих триангуляцию
        /// </summary>
        /// <param name="points">Набор точек для анализа</param>
        /// <returns></returns>
        private List<Triangle> Triangulation(List<Point> points)
        {
            var triangles = new List<Triangle>();
            var liveEdges = new List<Vector>
            {
                HullEdge(points)
            };

            // Добавить "живое" ребро
            void AddEdge(Vector vector)
            {
                if (liveEdges.Contains(vector))
                    liveEdges.Remove(vector);
                else
                    liveEdges.Add(vector.Reverse());
            }

            while (liveEdges.Count != 0)
            {
                var edge = liveEdges.First();
                liveEdges.Remove(edge);
                Mate(edge, points)
                    .Tap(point =>
                    {
                        AddEdge(Vector.Create(point, edge.Src));
                        AddEdge(Vector.Create(edge.Dest, point));
                        triangles.Add(Triangle.Create(edge.Src, edge.Dest, point));
                    });
            }
            return triangles;
        }

        /// <summary>
        /// Найти редро относительно которого все точки находятся справа
        /// </summary>
        /// <param name="points">Набор точек для анализа</param>
        /// <returns></returns>
        private Vector HullEdge(List<Point> points)
        {
            // Поиск точки с минимальной координатой по оси X
            var point = points.Aggregate((currentMin, point) => (point.X < currentMin.X) ? point : currentMin);
            // Поиск ребра
            var vector = Vector.Create(point, points.First());
            foreach (var next in points)
            {
                if (vector.Position(next) != Relative.Right)
                    Vector.CreateNonZero(point, next).Tap(v => vector = v);
            }
            return vector;
        }

        /// <summary>
        /// Найти точку сопряженную с вектором
        /// </summary>
        /// <param name="edge">Вектор</param>
        /// <param name="points">Набор точек для анализа</param>
        /// <returns></returns>
        private Result<Point> Mate(Vector edge, IReadOnlyList<Point> points)
        {
            var optimal = double.MaxValue;
            var optimalPoint = Result.Failure<Point>("Нет сопряженной точки");
            var normalEdge = edge.Normal();
            foreach (var next in points.Where(p => edge.Position(p) == Relative.Right))
            {
                Vector.Create(edge.Dest, next)
                    .Normal()
                    .Intersect(normalEdge)
                    .Tap(circleCenter =>
                    {
                        var newOptimal = (circleCenter.X - normalEdge.Src.X) * normalEdge.Offset.X + (circleCenter.Y - normalEdge.Src.Y) * normalEdge.Offset.Y;
                        if (optimal > newOptimal)
                        {
                            optimal = newOptimal;
                            optimalPoint = Result.Success(next);
                        }
                    });
            }
            return optimalPoint;
        }

        protected override bool EqualsCore(Delaunay other)
        {
            return
                !Points.Except(other.Points).Any() &&
                !other.Points.Except(Points).Any();
        }

        protected override int GetHashCodeCore()
        {
            return Points.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode()));
        }

        /// <summary>
        /// Создать триангуляцию для списка точек
        /// </summary>
        /// <param name="points">Список точек для триангуляции</param>
        /// <returns></returns>
        public static Result<Delaunay> Create(List<Point> points)
        {
            if (points is not null && points.Count < 3 || points.Count > 500)
                return Result.Failure<Delaunay>("Количество точек должно быть от 3 до 500");

            return new Delaunay(points);
        }

        /// <summary>
        /// Создать триангуляцию из произвольного набора точек в заданной области
        /// </summary>
        /// <param name="leftUp">Координаты левого верхнего угола</param>
        /// <param name="rightBottom">Координаты правого нижнего угола</param>
        /// <param name="count">Количество точек</param>
        /// <returns></returns>
        public static Result<Delaunay> CreateRandomTriangulation(Point leftUp, Point rightBottom, int count)
        {
            if (count < 3 || count > 500)
                return Result.Failure<Delaunay>("Количество точек должно быть от 3 до 500");

            var points = new List<Point>();
            var random = new Random();
            points.AddRange(
                from position in Enumerable.Range(1, count)
                select Point.Create(
                    leftUp.X + (rightBottom.X - leftUp.X) * random.NextDouble(),
                    leftUp.Y + (rightBottom.Y - leftUp.Y) * random.NextDouble())
                );

            return new Delaunay(points);
        }
    }
}
