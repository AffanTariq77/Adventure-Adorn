using Blaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blaze.Repository.Contract
{
    public interface IUnitOfWorkRepository
    {
        DbContext Context { get; }
        IRepository<User> UserRepository { get; }


        [Obsolete("Stop using it and use CommitAsync instead")]
        int Commit();
        Task<int> CommitAsync();
    }
}
