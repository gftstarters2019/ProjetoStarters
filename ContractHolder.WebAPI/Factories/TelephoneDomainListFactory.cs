using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;
using System;
using System.Collections.Generic;

namespace ContractHolder.WebAPI.Factories
{
    public class TelephoneDomainListFactory : IFactory<List<TelephoneDomain>, ContractHolderViewModel>
    {
        public List<TelephoneDomain> Create(ContractHolderViewModel contractHolderViewModel)
        {
            var telephoneList = new List<TelephoneDomain>();

            foreach(var telephone in contractHolderViewModel.individualTelephones)
            {
                telephoneList.Add(new TelephoneDomain()
                {
                    TelephoneId = telephone.TelephoneId != Guid.Empty ? telephone.TelephoneId : Guid.Empty,
                    TelephoneNumber = telephone.TelephoneNumber,
                    TelephoneType = telephone.TelephoneType
                });
            }

            return telephoneList;
        }
    }
}
