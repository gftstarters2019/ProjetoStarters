using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories.Interfaces
{
    public interface IFactory<T, U>
    {
        T Create(U u);
    }
}
