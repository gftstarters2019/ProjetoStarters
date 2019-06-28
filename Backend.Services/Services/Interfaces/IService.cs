using System;
using System.Collections.Generic;

namespace Backend.Services.Services.Interfaces
{
    public interface IService<T>
    {
        T Save(T modelToAddToDB);

        T Update(Guid id, T modelToUpdate);

        T Delete(Guid id);

        T Get(Guid id);

        List<T> GetAll();
    }
}
