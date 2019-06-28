using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;
using System;
using System.Collections.Generic;

namespace ContractHolder.WebAPI.Factories
{
    public class AddressDomainListFactory : IFactory<List<AddressDomain>, ContractHolderViewModel>
    {
        public List<AddressDomain> Create(ContractHolderViewModel contractHolderViewModel)
        {
            var addressList = new List<AddressDomain>();
            foreach(var address in contractHolderViewModel.individualAddresses)
            {
                addressList.Add(new AddressDomain()
                {
                    AddressId = address.AddressId != Guid.Empty ? address.AddressId : Guid.Empty,
                    AddressCity = address.AddressCity,
                    AddressComplement = address.AddressComplement,
                    AddressCountry = address.AddressCountry,
                    AddressNeighborhood = address.AddressNeighborhood,
                    AddressNumber = address.AddressNumber,
                    AddressState = address.AddressState,
                    AddressStreet = address.AddressStreet,
                    AddressType = address.AddressType,
                    AddressZipCode = address.AddressZipCode
                });
            }

            return addressList;
        }
    }
}
