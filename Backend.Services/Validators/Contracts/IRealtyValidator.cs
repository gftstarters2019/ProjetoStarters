using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IRealtyValidator
    {
        List<string> IsValid(RealtyDomain realty);
    }
}
