using CSharpFunctionalExtensions;
using geometry.Core.Triangulation.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace geometry.Core.Triangulation.Domain
{
    
    /// <summary>
    /// Триангуляция Делоне
    /// </summary>
    public class Delaunay
    {
        public Delaunay()
        {
        }

        /// <summary>
        /// Получить список треугольников, формирующих триангуляцию
        /// </summary>
        /// <param name="points">Набор точек для анализа</param>
        /// <returns></returns>
        public IList<Triangle> Triangulation(IList<Point> points)
        {
            var triangles = new List<Triangle>();
            var liveEdges = new List<Vector>();
            var hullEdge = HullEdge(points);
            liveEdges.Add(hullEdge);

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
                Mate(edge, points).Tap(point =>
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
        private Vector HullEdge(IList<Point> points)
        {
            var source = points.First();
            foreach (var point in points)
            {
                if (point.X < source.X)
                    source = point;
            }
            points.Remove(source);
            var vector = Vector.Create(source, points.First());
            foreach (var next in points)
            {
                var relative = vector.Position(next);
                if (relative == Relative.Left || relative == Relative.Match)
                    vector = Vector.Create(source, next);
            }
            return vector;
        }

        /// <summary>
        /// Найти точку сопряженную с вектором
        /// </summary>
        /// <param name="edge">Вектор</param>
        /// <param name="points">Набор точек для анализа</param>
        /// <returns></returns>
        private Result<Point> Mate(Vector edge, IList<Point> points)
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

    }
}
