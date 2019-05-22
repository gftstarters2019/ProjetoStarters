using Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class Realty : IBeneficiary
    {
        public Guid RealtyId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public IBeneficiary Beneficiary { get; set; }
        public Address RealtyAddress { get; set; }
        public Guid RealtyAddressId { get; set; }
        public string RealtyMunicipalRegistration { get; set; }
        public DateTime RealtyConstructionDate { get; set; }
        public double RealtySaleValue { get; set; }
        public double RealtyMarketValue { get; set; }
    }
}
