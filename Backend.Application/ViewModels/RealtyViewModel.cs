using Backend.Core.Enums;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ViewModels
{
    public class RealtyViewModel : Beneficiary
    {
        public Guid Id { get; set; }
        public string MunicipalRegistration { get; set; }
        public DateTime ConstructionDate { get; set; }
        public double SaleValue { get; set; }
        public double MarketValue { get; set; }

        // Address
        public AddressEntity Address { get; set; }
        public Guid AddressId { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
