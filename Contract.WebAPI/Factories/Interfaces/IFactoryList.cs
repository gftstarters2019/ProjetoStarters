using System.Collections.Generic;

namespace Contract.WebAPI.Factories.Interfaces
{
    public interface IFactoryList<T>
    {
        List<T> CreateList(List<T> t);
    }
}
