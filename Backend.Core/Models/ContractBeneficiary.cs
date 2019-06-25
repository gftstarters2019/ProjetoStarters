using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class ContractBeneficiary
    {
        public Guid ContractBeneficiaryId { get; set; }
        public Guid SignedContractId { get; set; }
        public SignedContractEntity SignedContract { get; set; }
        public Guid BeneficiaryId { get; set; }
        public BeneficiaryEntity Beneficiary { get; set; }
    }
}