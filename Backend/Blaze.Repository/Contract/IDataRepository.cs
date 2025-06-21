using System;

namespace Blaze.Repository.Contract
{
    [Obsolete("Use IRepository<T> instead")]
    public interface IDataRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
    }
}
