using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class BeneficiaryAddress
    {
        public Guid BeneficiaryAddressId { get; set; }
        public Guid AddressId { get; set; }
        public AddressEntity Address { get; set; }
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }
    }
}
