using Backend.Core.Models;
using ContractHolder.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractHolder.WebAPI.Factories
{
    /// <summary>
    /// Factory de Individual
    /// </summary>
    public class IndividualFactory : IFactory<IndividualEntity, ContractHolderViewModel>
    {
        private IndividualEntity individual;

        /// <summary>
        /// Método para criar individual
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public IndividualEntity Create(ContractHolderViewModel vm)
        {
            individual = null;

            individual = new IndividualEntity
            {
                BeneficiaryId = Guid.NewGuid(),
                IsDeleted = false,
                IndividualCPF = vm.individualCPF,
                IndividualName = vm.individualName,
                IndividualRG = vm.individualRG,
                IndividualEmail = vm.individualEmail,
                IndividualBirthdate = vm.individualBirthdate
            };
            
            return individual;
        }
    }
}
