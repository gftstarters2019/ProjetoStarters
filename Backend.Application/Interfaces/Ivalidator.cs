namespace Backend.Application.Interfaces
{ 
    public interface IValidator
    {
        void ValidateCPF();
        void ValidateRG();
        void ValidateDate();
    }
}