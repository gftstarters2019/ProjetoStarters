using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IPetValidator
    {
        List<string> IsValid(PetDomain pet);
    }
}
