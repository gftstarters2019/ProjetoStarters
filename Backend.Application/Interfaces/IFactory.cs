using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Interfaces
{
    interface IFactory<T>
    {
        T Create();
    }
}
