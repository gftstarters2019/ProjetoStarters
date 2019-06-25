using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class BeneficiaryAddress
    {
        [Key]
        public Guid BeneficiaryAddressId { get; set; }
        public Guid AddressId { get; set; }
        public AddressEntity Address { get; set; }
        public Guid BeneficiaryId { get; set; }
        public BeneficiaryEntity Beneficiary { get; set; }
    }
}
