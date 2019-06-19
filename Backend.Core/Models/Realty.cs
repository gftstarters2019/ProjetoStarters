using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Core.Models
{
    public class Realty : Beneficiary
    {
        [MaxLength(50)]
        public string RealtyMunicipalRegistration { get; set; }
        public DateTime RealtyConstructionDate { get; set; }
        public double RealtySaleValue { get; set; }
        public double RealtyMarketValue { get; set; }
        [NotMapped]
        public Address Address { get; set; }
    }
}
