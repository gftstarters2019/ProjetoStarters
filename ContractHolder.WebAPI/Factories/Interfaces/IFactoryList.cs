using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractHolder.WebAPI.Factories.Interfaces
{
    public interface IFactoryList<T>
    {
        List<T> CreateList(List<T> t);
    }
}
