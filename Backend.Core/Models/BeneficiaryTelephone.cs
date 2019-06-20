using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class BeneficiaryTelephone
    {
        public Guid BeneficiaryTelephoneId { get; set; }
        public Guid TelephoneId { get; set; }
        public TelephoneEntity Telephone { get; set; }
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }
    }
}
