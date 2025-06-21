using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blaze.Repository.Contract
{
    public interface IRepository<T> where T: class, new()
    {
        ValueTask<EntityEntry<T>> AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task RemoveAsync(params object[] keyValues);
        void Update(T entity);
        Task UpdateAsync(T entity);
        T Clone(T entity);
        Task<T> FindAsync(params object[] keyValues);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<decimal> SumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> selector);
        /// <summary>
        /// User asSplit = true for performance reasons. In very rare cases, you may need to use asSplit = false.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asSplit"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, bool asSplit = false, params Expression<Func<T, object>>[] includes);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includeStatements);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] includeStatements);
        IQueryable<T> ToQuery();
        IQueryable<T> ToQuery(Expression<Func<T, bool>> predicate);
        IQueryable<T> ToQuery(Expression<Func<T, bool>> predicate, string[] includeStatements);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        EntityEntry<T> Add(T entity);
        Task<IEnumerable<T>> GetAllAsync();

    }
}
