using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IPetValidator
    {
        bool IsValid(PetDomain pet);
    }
}
