using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories.Contracts
{
    public interface IReadOnlyRepository<T>
    {
        T Find(Guid id);
        IEnumerable<T> Get();
    }
}
