using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Find(Guid id);
        IEnumerable<T> Get();
        T Add(T t);
        bool Remove(Guid id);
        T Update(Guid id, T t);
        bool Save();
    }
}
