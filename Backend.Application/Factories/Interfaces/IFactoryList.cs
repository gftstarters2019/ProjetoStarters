using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories.Interfaces
{
    public interface IFactoryList <T>
    {
        List<T> CreateList(List<T> t);
    }
}
