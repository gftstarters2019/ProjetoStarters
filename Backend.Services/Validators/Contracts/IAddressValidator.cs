using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IAddressValidator
    {
        bool IsValid(AddressDomain address);
    }
}
