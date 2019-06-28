using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IRealtyValidator
    {
        bool IsValid(RealtyDomain realty);
    }
}
