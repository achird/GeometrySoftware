namespace geometry.Core.Application.Cqrs
{
    /// <summary>
    /// Команда к контексту домена, возвращающая результат
    /// </summary>
    public interface ICommand<TResult>
    {
    }

    /// <summary>
    /// Команда к контексту домена
    /// </summary>
    public interface ICommand
    {
    }
}
