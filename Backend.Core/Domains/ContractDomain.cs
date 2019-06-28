using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class ContractDomain
    {
        public Guid ContractId { get; set; }
        public ContractType ContractType { get; set; }
        public ContractCategory ContractCategory { get; set; }
        public DateTime ContractExpiryDate { get; set; }
        public bool ContractDeleted { get; set; }
    }
}
