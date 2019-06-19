using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class SignedContract
    {
        public Guid SignedContractId { get; set; }
        public Guid IndividualId { get; set; }
        public Individual SignedContractIndividual { get; set; }
        public Guid ContractId { get; set; }
        public Contract SignedContractContract { get; set; }
        public bool ContractIndividualIsActive { get; set; }
    }
}
