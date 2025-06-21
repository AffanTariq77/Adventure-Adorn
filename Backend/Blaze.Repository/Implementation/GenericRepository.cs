using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Blaze.Common.Extensions;
using Blaze.Repository.Contract;

namespace Blaze.Repository.Implementation
{
    public sealed class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public ValueTask<EntityEntry<T>> AddAsync(T entity)
        {
            return GetDbSet().AddAsync(entity);
        }

        public Task AddRangeAsync(List<T> entity)
        {
            return GetDbSet().AddRangeAsync(entity);
        }

        private async Task<T> PrivateFind(params object[] keyValues)
        {
            return await GetDbSet().FindAsync(keyValues);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await PrivateFind(keyValues);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var query = GetDbSet().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public async Task<decimal> SumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> selector)
        {
            var query = GetDbSet().AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            return await query.SumAsync(selector);
        }

        private DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        public async Task RemoveAsync(params object[] keyValues)
        {
            var entity = await PrivateFind(keyValues);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public void Update(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public T Clone(T entity)
        {
            return _context.Entry(entity).CurrentValues.Clone().ToObject() as T;
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = GetDbSet().AsQueryable();

            if (includes != null)
                query = query.IncludeMultiple(includes);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, bool asSplit = true, params Expression<Func<T, object>>[] includes)
        {
            var query = GetDbSet().AsQueryable();

            if (includes != null)
                query = query.IncludeMultiple(includes);

            query = asSplit ? query.AsSplitQuery() : query.AsSingleQuery();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return (await FindAllAsync(predicate, includes)).ToList();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includeStatements)
        {
            var query = GetDbSet().AsQueryable();

            if (includeStatements != null && includeStatements.Any())
            {
                foreach (var includeStatement in includeStatements)
                {
                    query = query.Include(includeStatement);
                }
            }

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return (await FindAllAsync(predicate, includes)).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] includeStatements)
        {
            return (await FindAllAsync(predicate, includeStatements)).FirstOrDefault();
        }

        public IQueryable<T> ToQuery()
        {
            return GetDbSet().AsQueryable();
        }

        public IQueryable<T> ToQuery(Expression<Func<T, bool>> predicate)
        {
            return GetDbSet().Where(predicate).AsQueryable();
        }

        public IQueryable<T> ToQuery(Expression<Func<T, bool>> predicate, string[] includeStatements)
        {
            var query = GetDbSet().AsQueryable();
            if (includeStatements != null && includeStatements.Any())
            {
                foreach (var includeStatement in includeStatements)
                {
                    query = query.Include(includeStatement);
                }
            }
            return query.Where(predicate).AsQueryable();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return GetDbSet().AnyAsync(predicate);
        }

        public EntityEntry<T> Add(T entity)
        {
            return GetDbSet().Add(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetDbSet().ToListAsync();
        }


    }
}
