using Backend.Core.Enums;
using System;

namespace Beneficiaries.WebAPI.ViewModels
{
    public class RealtyViewModel
    {
        public Guid Id { get; set; }
        public string MunicipalRegistration { get; set; }
        public DateTime ConstructionDate { get; set; }
        public double SaleValue { get; set; }
        public double MarketValue { get; set; }

        // Address
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
