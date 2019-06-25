using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using Contract.WebAPI.ViewModels;

namespace Contract.WebAPI.Factories
{
    public class ContractViewModelFactory : IFactory<ContractViewModel, CompleteContractDomain>
    {
        public ContractViewModel Create(CompleteContractDomain completeContractDomain)
        {
            return new ContractViewModel()
            {
                Category = completeContractDomain.Contract.ContractCategory,
                ContractHolder = completeContractDomain.SignedContract.SignedContractIndividual,
                ContractHolderId = completeContractDomain.SignedContract.IndividualId,
                ExpiryDate = completeContractDomain.Contract.ContractExpiryDate,
                Individuals = completeContractDomain.Individuals,
                Pets = completeContractDomain.Pets,
                MobileDevices = completeContractDomain.MobileDevices,
                Realties = FactoriesManager.RealtyViewModelList.Create(completeContractDomain.Realties),
                Vehicles = completeContractDomain.Vehicles,
                IsActive = completeContractDomain.SignedContract.ContractIndividualIsActive,
                SignedContractId = completeContractDomain.SignedContract.SignedContractId,
                Type = completeContractDomain.Contract.ContractType
            };
        }
    }
}
