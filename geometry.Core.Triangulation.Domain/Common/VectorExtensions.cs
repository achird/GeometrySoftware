using CSharpFunctionalExtensions;

namespace geometry.Core.Triangulation.Domain.Common
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Проверить пересекаются ли данные векторы
        /// </summary>
        /// <returns>true, если пересекается, иначе false</returns>
        public static bool IsIntersect(this Vector vector1, Vector vector2)
        {
            // Общий делитель
            var denominator = vector1.Reverse() * vector2;
            if (denominator == 0)
            {
                var a = vector1.Src.X * vector1.Dest.Y - vector1.Dest.X * vector1.Src.Y;
                var b = vector2.Src.X * vector2.Dest.Y - vector2.Dest.X * vector2.Src.Y;
                if (a * (vector2.Dest.X - vector2.Src.X) - b * (vector1.Dest.X - vector1.Src.X) == 0 &&
                    a * (vector2.Dest.Y - vector2.Src.Y) - b * (vector1.Dest.Y - vector1.Src.Y) == 0)
                    // "Отрезки пересекаются (совпадают)";
                    return true;
                else
                    // "Отрезки не пересекаются (параллельны)";
                    return false;
            }
            var numeratorA = Vector.Create(vector1.Dest, vector2.Dest) * Vector.Create(vector2.Src, vector2.Dest) / denominator;
            var numeratorB = Vector.Create(vector1.Dest, vector1.Src) * Vector.Create(vector1.Dest, vector2.Dest) / denominator;
            return 0 <= numeratorA && numeratorA <= 1 && 0 <= numeratorB && numeratorB <= 1;
        }

        /// <summary>
        /// Найти точку пересечения прямых заданных двумя векторами
        /// </summary>
        /// <returns></returns>
        public static Result<Point> Intersect(this Vector vector1, Vector vector2)
        {
            // Общий делитель
            var denominator = vector1.Reverse() * vector2;
            if (denominator == 0) 
                return Result.Failure<Point>("Нет точки пересечения");
            return vector1.Dest - vector1.Offset * (Vector.Create(vector1.Dest, vector2.Dest) * Vector.Create(vector2.Src, vector2.Dest) / denominator);
        }
    }
}
