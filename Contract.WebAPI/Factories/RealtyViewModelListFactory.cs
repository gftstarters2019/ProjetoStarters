using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using Contract.WebAPI.ViewModels;
using System.Collections.Generic;

namespace Contract.WebAPI.Factories
{
    public class RealtyViewModelListFactory : IFactory<List<RealtyViewModel>, List<RealtyDomain>>
    {
        public List<RealtyViewModel> Create(List<RealtyDomain> realtyDomains)
        {
            var realtiesViewModel = new List<RealtyViewModel>();

            foreach(var realty in realtyDomains)
            {
                realtiesViewModel.Add(new RealtyViewModel()
                {
                    Id = realty.BeneficiaryId,
                    SaleValue = realty.RealtySaleValue,
                    MunicipalRegistration = realty.RealtyMunicipalRegistration,
                    MarketValue = realty.RealtyMarketValue,
                    ConstructionDate = realty.RealtyConstructionDate,
                    AddressId = realty.Address.AddressId,
                    AddressCity = realty.Address.AddressCity,
                    AddressComplement = realty.Address.AddressComplement,
                    AddressCountry = realty.Address.AddressCountry,
                    AddressNeighborhood = realty.Address.AddressNeighborhood,
                    AddressNumber = realty.Address.AddressNumber,
                    AddressState = realty.Address.AddressState,
                    AddressStreet = realty.Address.AddressStreet,
                    AddressType = realty.Address.AddressType,
                    AddressZipCode = realty.Address.AddressZipCode
                });
            }

            return realtiesViewModel;
        }
    }
}
