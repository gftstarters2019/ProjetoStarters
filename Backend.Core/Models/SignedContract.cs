using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class SignedContract
    {
        [Key]
        public Guid SignedContractId { get; set; }
        public Guid IndividualId { get; set; }
        public Individual SignedContractIndividual { get; set; }
        public Guid ContractId { get; set; }
        public Contract SignedContractContract { get; set; }
        public bool ContractIndividualIsActive { get; set; }
    }
}