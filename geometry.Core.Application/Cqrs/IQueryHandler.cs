using System.Threading.Tasks;

namespace geometry.Core.Application.Cqrs
{
    /// <summary>
    /// Обработчик запросов к контексту домена
    /// </summary>
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
