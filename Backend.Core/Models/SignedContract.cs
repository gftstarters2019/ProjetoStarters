using System;
using System.Collections.Generic;

namespace Backend.Core.Models
{
    public class SignedContract
    {
        public Guid ContractSignedId { get; set; }
        public Individual ContractSignedIndividual { get; set; }
        public Contract ContractSignedContract { get; set; }
        public List<Beneficiary> ContractSignedBeneficiary { get; set; }
        public bool ContractIndividualIsActive { get; set; }
    }
}