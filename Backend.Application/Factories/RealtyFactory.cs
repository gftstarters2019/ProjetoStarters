using Backend.Application.Interfaces;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class RealtyFactory : IFactory<Realty>
    {
        public Realty Create(Guid realtyId, Guid addressId, string realtyMunicipalRegistration, DateTime realtyConstructionDate, double realtySaleValue, double realtyMarketValue)
        {
            var realty = new Realty();
            realty.RealtyId = realtyId;
            realty.AddressId = addressId;
            realty.RealtyMunicipalRegistration = realtyMunicipalRegistration;
            realty.RealtyConstructionDate = realtyConstructionDate;
            realty.RealtySaleValue = realtySaleValue;
            realty.RealtyMarketValue = realtyMarketValue;

            return realty;

        }
        public Realty Create()
        {
           return new Realty();
        }
    }
}