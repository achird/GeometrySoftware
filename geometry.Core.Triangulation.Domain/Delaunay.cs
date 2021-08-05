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
        public Delaunay(List<Point> points)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));
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
            points.Remove(point);
            // Поиск ребра
            var vector = Vector.Create(point, points.First());
            foreach (var next in points)
            {
                if (vector.Position(next) != Relative.Right)
                    vector = Vector.Create(point, next);
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
    }
}
