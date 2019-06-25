using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class RealtyDomain : Beneficiary
    {
        public string RealtyMunicipalRegistration { get; set; }
        public DateTime RealtyConstructionDate { get; set; }
        public double RealtySaleValue { get; set; }
        public double RealtyMarketValue { get; set; }

        public AddressDomain Address { get; set; }
    }
}
