using Backend.Core.Domains;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class ContractHolderService : IService<ContractHolderDomain>
    {
        private IRepository<ContractHolderDomain> _contractHolderRepository;

        public ContractHolderService(IRepository<ContractHolderDomain> contractHolderRepository)
        {
            _contractHolderRepository = contractHolderRepository;
        }

        public ContractHolderDomain Delete(Guid id)
        {
            var contractHolderToDelete = _contractHolderRepository.Find(id);
            if (contractHolderToDelete == null)
                return null;

            contractHolderToDelete.Individual.IsDeleted = !contractHolderToDelete.Individual.IsDeleted;
            var deletedBeneficiary = _contractHolderRepository.Update(id, contractHolderToDelete);
            if (_contractHolderRepository.Save())
                return deletedBeneficiary;
            return null;
        }

        public ContractHolderDomain Get(Guid id)
        {
            return _contractHolderRepository.Find(id);
        }

        public List<ContractHolderDomain> GetAll()
        {
            return _contractHolderRepository.Get().ToList();
        }

        public ContractHolderDomain Save(ContractHolderDomain contractHolderDomain)
        {
            if (contractHolderDomain == null)
                return null;

            /*
             * Validations
            */

            return _contractHolderRepository.Add(contractHolderDomain);
        }

        public ContractHolderDomain Update(Guid id, ContractHolderDomain contractToBeUpdated)
        {
            if (contractToBeUpdated == null)
                return null;

            /*
             * Validations
            */

            return _contractHolderRepository.Update(id, contractToBeUpdated);
        }
    }
}
