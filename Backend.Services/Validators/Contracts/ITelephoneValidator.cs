using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface ITelephoneValidator
    {
        List<string> IsValid(TelephoneDomain telephone);
    }
}
