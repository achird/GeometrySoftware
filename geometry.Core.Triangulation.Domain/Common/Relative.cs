using CSharpFunctionalExtensions;

namespace geometry.Core.Triangulation.Domain.Common
{
    /// <summary>
    /// Относительное положение точки
    /// </summary>
    public class Relative : ValueObject<Relative>
    {
        private Relative(int value, string description)
        {
            Value = value;
            Description = description;
        }

        /// <summary>
        /// Значение
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; private set; }

        protected override bool EqualsCore(Relative other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Находится слева от верктора
        /// </summary>
        public static readonly Relative Left = new(1, "Находится слева от верктора");
        /// <summary>
        /// Находится справа от вектора
        /// </summary>
        public static readonly Relative Right = new(2, "Находится справа от вектора");
        /// <summary>
        /// Совпадает с прямой, образованной вектором
        /// </summary>
        public static readonly Relative Match = new(3, "Совпадает с прямой, образованной вектором");
    }
}
