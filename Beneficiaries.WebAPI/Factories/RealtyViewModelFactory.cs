using Backend.Core.Domains;
using Backend.Core.Enums;
using Beneficiaries.WebAPI.Factories.Interfaces;
using Beneficiaries.WebAPI.ViewModels;
using System;

namespace Beneficiaries.WebAPI.Factories
{
    public class RealtyViewModelFactory : IFactory<RealtyViewModel, RealtyDomain>
    {
        public RealtyViewModel Create(RealtyDomain realtyDomain)
        {
            if (realtyDomain == null)
                return null;

            return new RealtyViewModel()
            {
                Id = realtyDomain.BeneficiaryId,
                SaleValue = realtyDomain.RealtySaleValue,
                MunicipalRegistration = realtyDomain.RealtyMunicipalRegistration,
                MarketValue = realtyDomain.RealtyMarketValue,
                ConstructionDate = realtyDomain.RealtyConstructionDate,
                AddressId = realtyDomain.Address == null ? Guid.Empty : realtyDomain.Address.AddressId,
                AddressCity = realtyDomain?.Address?.AddressCity,
                AddressComplement = realtyDomain?.Address?.AddressComplement,
                AddressCountry = realtyDomain?.Address?.AddressCountry,
                AddressNeighborhood = realtyDomain?.Address?.AddressNeighborhood,
                AddressNumber = realtyDomain?.Address?.AddressNumber,
                AddressState = realtyDomain?.Address?.AddressState,
                AddressStreet = realtyDomain?.Address?.AddressStreet,
                AddressType = realtyDomain.Address == null ? (AddressType)1 : realtyDomain.Address.AddressType,
                AddressZipCode = realtyDomain?.Address?.AddressZipCode
            };
        }
    }
}
