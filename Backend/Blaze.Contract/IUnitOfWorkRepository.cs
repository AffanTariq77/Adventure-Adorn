using Blaze.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Blaze.Contract
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
