using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractHolder.WebAPI.Factories.Interfaces
{
    public interface IFactory<T, U>
    {
        T Create(U u);
    }
}
