using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories.Contracts
{
    public interface IWriteRepository<T>
    {
        bool Add(T t);
        T Remove(T t);
        T Update(T t);
    }
}
