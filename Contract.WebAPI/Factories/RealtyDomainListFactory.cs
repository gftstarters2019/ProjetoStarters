using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using Contract.WebAPI.ViewModels;
using System;
using System.Collections.Generic;

namespace Contract.WebAPI.Factories
{
    public class RealtyDomainListFactory : IFactory<List<RealtyDomain>, List<RealtyViewModel>>
    {
        public List<RealtyDomain> Create(List<RealtyViewModel> realtyViewModels)
        {
            var realtiesDomain = new List<RealtyDomain>();

            foreach (var realty in realtyViewModels)
            {
                realtiesDomain.Add(new RealtyDomain()
                {
                    BeneficiaryId = realty.Id,
                    RealtyConstructionDate = realty.ConstructionDate,
                    RealtyMarketValue = realty.MarketValue,
                    RealtyMunicipalRegistration = realty.MunicipalRegistration,
                    RealtySaleValue = realty.SaleValue,
                    Address = new AddressDomain()
                    {
                        AddressId = realty.AddressId,
                        AddressCity = realty.AddressCity,
                        AddressComplement = realty.AddressComplement,
                        AddressCountry = realty.AddressCountry,
                        AddressNeighborhood = realty.AddressNeighborhood,
                        AddressNumber = realty.AddressNumber,
                        AddressState = realty.AddressState,
                        AddressStreet = realty.AddressStreet,
                        AddressType = realty.AddressType,
                        AddressZipCode = realty.AddressZipCode
                    }
                });
            }

            return realtiesDomain;
        }
    }
}
