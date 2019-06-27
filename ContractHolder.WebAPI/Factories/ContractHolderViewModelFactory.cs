using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;
using System;

namespace ContractHolder.WebAPI.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractHolderViewModelFactory : IFactory<ContractHolderViewModel, ContractHolderDomain>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractHolderDomain"></param>
        /// <returns></returns>
        public ContractHolderViewModel Create(ContractHolderDomain contractHolderDomain)
        {
            var contractHolderViewModel = new ContractHolderViewModel()
            {
                individualId = contractHolderDomain.Individual.BeneficiaryId,
                individualAddresses = contractHolderDomain.IndividualAddresses,
                individualTelephones = contractHolderDomain.IndividualTelephones,
                individualBirthdate = contractHolderDomain.Individual.IndividualBirthdate,
                individualCPF = contractHolderDomain.Individual.IndividualCPF,
                individualEmail = contractHolderDomain.Individual.IndividualEmail,
                individualName = contractHolderDomain.Individual.IndividualName,
                individualRG = contractHolderDomain.Individual.IndividualRG,
                isDeleted = contractHolderDomain.Individual.IsDeleted
            };

            return contractHolderViewModel;
        }
    }
}
