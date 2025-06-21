using System;

namespace Blaze.Contract
{
    [Obsolete("Use IRepository<T> instead")]
    public interface IDataRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
    }
}
