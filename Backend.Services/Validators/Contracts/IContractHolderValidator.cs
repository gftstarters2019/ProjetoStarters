﻿using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IContractHolderValidator
    {
        List<string> IsValid(IndividualDomain individual, List<AddressDomain> addresses, List<TelephoneDomain> telephones);
    }
}
