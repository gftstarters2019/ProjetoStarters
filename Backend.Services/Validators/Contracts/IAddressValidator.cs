using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IAddressValidator
    {
        List<string> IsValid(AddressDomain address);
    }
}
