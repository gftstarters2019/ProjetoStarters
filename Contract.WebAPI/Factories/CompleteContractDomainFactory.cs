using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using Contract.WebAPI.ViewModels;
using System;

namespace Contract.WebAPI.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class CompleteContractDomainFactory : IFactory<CompleteContractDomain, ContractViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CompleteContractDomain Create(ContractViewModel contractViewModel)
        {
            var contract = new ContractDomain()
            {
                ContractCategory = contractViewModel.Category,
                ContractDeleted = false,
                ContractExpiryDate = contractViewModel.ExpiryDate,
                ContractType = contractViewModel.Type,
                ContractId = Guid.Empty
            };
            return new CompleteContractDomain()
            {
                Contract = contract,
                SignedContract = new SignedContractDomain()
                {
                    ContractId = Guid.Empty,
                    ContractIndividualIsActive = contractViewModel.IsActive,
                    IndividualId = contractViewModel.ContractHolderId,
                    SignedContractContract = contract,
                    SignedContractId = contractViewModel.SignedContractId
                },
                Individuals = contractViewModel.Individuals,
                MobileDevices = contractViewModel.MobileDevices,
                Pets = contractViewModel.Pets,
                Realties = FactoriesManager.RealtyDomainList.Create(contractViewModel.Realties),
                Vehicles = contractViewModel.Vehicles,
            };
        }
    }
}
