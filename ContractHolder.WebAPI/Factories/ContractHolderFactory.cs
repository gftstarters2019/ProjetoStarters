using Backend.Core.Domains;
using Contract.WebAPI.Factories.Interfaces;
using ContractHolder.WebAPI.ViewModels;

namespace ContractHolder.WebAPI.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractHolderFactory : IFactory<ContractHolderDomain, ContractHolderViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractHolderViewModel"></param>
        /// <returns></returns>
        public ContractHolderDomain Create(ContractHolderViewModel contractHolderViewModel)
        {
            var contractHolderDomain = new ContractHolderDomain()
            {
                Individual = FactoriesManager.IndividualDomain.Create(contractHolderViewModel),
                IndividualAddresses = FactoriesManager.AddressDomainList.Create(contractHolderViewModel),
                IndividualTelephones = FactoriesManager.TelephoneDomainList.Create(contractHolderViewModel),
            };

            return contractHolderDomain;
        }
    }
}
