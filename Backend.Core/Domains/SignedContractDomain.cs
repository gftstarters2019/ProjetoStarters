using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class SignedContractDomain
    {
        public Guid SignedContractId { get; set; }
        public Guid IndividualId { get; set; }
        public IndividualDomain SignedContractIndividual { get; set; }
        public Guid ContractId { get; set; }
        public ContractDomain SignedContractContract { get; set; }
        public bool ContractIndividualIsActive { get; set; }
    }
}
