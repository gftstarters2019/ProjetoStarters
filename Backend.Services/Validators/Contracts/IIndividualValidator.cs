using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IIndividualValidator
    {
        bool IsValid(IndividualDomain individual);
    }
}
