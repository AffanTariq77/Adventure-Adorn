using System.Linq.Expressions;

namespace Blaze.Contract
{
    public interface IReadOnlyRepository<T>
    {
        Task<T> FindAsync(params object[] keyValues);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}