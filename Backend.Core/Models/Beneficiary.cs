using System;
using System.Collections.Generic;

namespace Backend.Core.Models
{
    public abstract class Beneficiary
    {
        Guid BeneficiaryId { get; set; }
        public List<SignedContract> BeneficiarySignedContracts { get; set; }
    }
}
