using Backend.Core.Enums;
using System;
using System.Collections.Generic;

namespace Backend.Core.Models
{
    public class Contract
    {
        public Guid ContractId { get; set; }
        public ContractType ContractType { get; set; }
        public ContractCategory ContractCategory { get; set; }
        public DateTime ContractExpiryDate { get; set; }
        public DateTime ContractInitialDate { get; set; }
        public Individual ContractIndividual { get; set; }
        public bool ContractDeleted { get; set; }
    }
}
