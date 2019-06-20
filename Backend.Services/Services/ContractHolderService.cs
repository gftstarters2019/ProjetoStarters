using Backend.Core.Domains;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Services.Services.Contracts;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public ContractHolderDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ContractHolderDomain> GetAll()
        {
            throw new NotImplementedException();
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

        public ContractHolderDomain Update(Guid id, ContractHolderDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
