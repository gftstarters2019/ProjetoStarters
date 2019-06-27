using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters.Interfaces
{
    public interface IConverter<T, U>
    {
        T Convert(U u);
        U Convert(T t);
    }
}
