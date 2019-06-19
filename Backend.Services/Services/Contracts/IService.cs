using System;

namespace Backend.Services.Services.Contracts
{
    public interface IService<T>
    {
        T Save(T modelToAddToDB);

        T Update(Guid id, T modelToUpdate);
    }
}
