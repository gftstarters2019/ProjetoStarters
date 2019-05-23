﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class ContractBeneficiary
    {
        public Guid ContractBeneficiaryId { get; set; }
        public Guid? SignedContractId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public SignedContract SignedContract { get; set; }
        public Beneficiary Beneficiary { get; set; }
    }
}