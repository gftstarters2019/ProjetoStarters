using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories.Contracts
{
    public interface IRepository<T>
    {
        T Find(Guid id);
        IEnumerable<T> Get();
        T Add(T t);
        bool Remove(Guid id);
        T Update(Guid id, T t);
        bool Save();
    }
}
