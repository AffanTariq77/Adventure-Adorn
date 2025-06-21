using Blaze.Domain.Entities;
using Blaze.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Blaze.Repository.Implementation
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly BlazeContext _dbContext;

        public UnitOfWorkRepository(BlazeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext Context => throw new NotImplementedException();


        public IRepository<User> UserRepository => new GenericRepository<User>(_dbContext);

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new GenericRepository<T>(_dbContext);
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
