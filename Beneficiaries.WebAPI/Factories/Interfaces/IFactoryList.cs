using System.Collections.Generic;

namespace Beneficiaries.WebAPI.Factories.Interfaces
{
    public interface IFactoryList<T>
    {
        List<T> CreateList(List<T> t);
    }
}
