using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;
using System;

namespace ContractHolder.WebAPI.Factories
{
    /// <summary>
    /// Factory de IndividualDomain
    /// </summary>
    public class IndividualDomainFactory : IFactory<IndividualDomain, ContractHolderViewModel>
    {
        /// <summary>
        /// Método para criar individual
        /// </summary>
        /// <returns></returns>
        public IndividualDomain Create(ContractHolderViewModel contractHolderViewModel)
        {
            var individual = new IndividualDomain
            {
                BeneficiaryId = Guid.Empty,
                IsDeleted = contractHolderViewModel.isDeleted,
                IndividualCPF = contractHolderViewModel.individualCPF,
                IndividualName = contractHolderViewModel.individualName,
                IndividualRG = contractHolderViewModel.individualRG,
                IndividualEmail = contractHolderViewModel.individualEmail,
                IndividualBirthdate = contractHolderViewModel.individualBirthdate,
            };
            
            return individual;
        }
    }
}
