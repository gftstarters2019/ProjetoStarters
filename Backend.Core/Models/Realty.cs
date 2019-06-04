using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class Realty : Beneficiary
    {
        public Guid RealtyId { get; set; }
        [MaxLength(50)]
        public string RealtyMunicipalRegistration { get; set; }
        public DateTime RealtyConstructionDate { get; set; }
        public double RealtySaleValue { get; set; }
        public double RealtyMarketValue { get; set; }
    }
}
