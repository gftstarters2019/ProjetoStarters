namespace Backend.Application.Interfaces
{ 
    public interface IvalidatorIndividual
    {
        void ValidateCPF();
        void ValidateRG();
        void ValidateDate();
    }
}