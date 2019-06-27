using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface ITelephoneValidator
    {
        bool IsValid(TelephoneDomain telephone);
    }
}
