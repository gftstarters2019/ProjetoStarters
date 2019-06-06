using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories.Contracts
{
    public interface IWriteRepository<T>
    {
        bool Add(T t);
        T Remove(Guid id);
        T Update(Guid id, T t);
    }
}
