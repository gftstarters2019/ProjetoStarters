using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public abstract class BeneficiaryDomain
    {
        public Guid BeneficiaryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
