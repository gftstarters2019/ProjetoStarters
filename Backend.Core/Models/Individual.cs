using Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class Individual : IBeneficiary
    {
        public Guid IndividualId { get; set; }
        public Guid BeneficiaryId { get; set; }
        //public IBeneficiary Beneficiary { get; set; }
        public string IndividualName { get; set; }
        [MaxLength (30)]
        public string IndividualCPF { get; set; }
        public string IndividualRG { get; set; }
        public string IndividualEmail { get; set; }
        public DateTime IndividualBirthdate { get; set; }
        public List<Telephone> IndividualTelephones { get; set; }
        //public List<Guid> IndividualTelephonesId { get; set; }
        public List<Address> IndividualAddresses { get; set; }
        //public List<Guid> IndividualAddressesId { get; set; }
    }
}
