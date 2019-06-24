using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class SignedContractEntity
    {
        [Key]
        public Guid SignedContractId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public IndividualEntity SignedContractIndividual { get; set; }
        public Guid ContractId { get; set; }
        public ContractEntity SignedContractContract { get; set; }
        public bool ContractIndividualIsActive { get; set; }
    }
}