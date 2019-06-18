namespace Backend.Services.Services.Contracts
{
    public interface IService<T>
    {
        bool Save(T modelToAddToDB);
    }
}
