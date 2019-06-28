using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IIndividualValidator
    {
        List<string> IsValid(IndividualDomain individual);
    }
}
