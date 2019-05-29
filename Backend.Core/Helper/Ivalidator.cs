namespace Backend.Core.Helper
{ 
    public interface IValidator
    {
        void ValidateCPF();
        void ValidateRG();
        void ValidateDate();
    }
}