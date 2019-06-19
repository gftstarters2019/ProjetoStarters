using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public abstract class Beneficiary
    {
        public Guid BeneficiaryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
