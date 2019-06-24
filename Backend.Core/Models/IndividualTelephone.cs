using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Models
{
    public class IndividualTelephone
    {
        [Key]
        public Guid BeneficiaryTelephoneId { get; set; }
        public Guid TelephoneId { get; set; }
        public TelephoneEntity Telephone { get; set; }
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }
    }
}
