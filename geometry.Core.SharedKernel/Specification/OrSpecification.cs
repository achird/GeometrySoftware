﻿using System;
using System.Linq.Expressions;

namespace geometry.Core.SharedKernel.Base.Specification
{
    /// <summary>
    /// Обработчик действия OR для спецификации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> spec1;
        private readonly Specification<T> spec2;

        public OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            this.spec2 = spec2;
            this.spec1 = spec1;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression1 = spec1.ToExpression();
            var expression2 = spec2.ToExpression();
            var param = Expression.Parameter(typeof(T));
            var body = Expression.OrElse(expression1.Body, expression2.Body);
            body = (BinaryExpression)new ParameterReplacer(param).Visit(body);
            var expression = Expression.Lambda<Func<T, bool>>(body, param);

            return expression;
        }
    }
}
