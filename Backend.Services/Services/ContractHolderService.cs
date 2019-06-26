using Backend.Core.Domains;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using Backend.Services.Validators;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class ContractHolderService : IService<ContractHolderDomain>
    {
        private IRepository<ContractHolderDomain> _contractHolderRepository;
        private readonly IContractHolderValidator _contractHolderValidator;

        public ContractHolderService(IRepository<ContractHolderDomain> contractHolderRepository, IContractHolderValidator contractHolderValidator)
        {
            _contractHolderRepository = contractHolderRepository;
            _contractHolderValidator = contractHolderValidator;
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
            return _contractHolderRepository.Get().ToList();
        }

        public ContractHolderDomain Save(ContractHolderDomain contractHolderDomain)
        {
            var errors = string.Empty;

            if (contractHolderDomain == null)
                return null;

            var validationErrorsList = _contractHolderValidator.IsValid(contractHolderDomain.Individual,contractHolderDomain.IndividualAddresses,contractHolderDomain.IndividualTelephones);

            if(validationErrorsList.Any())
                foreach (var er in validationErrorsList)
                {
                    errors += er;
                }

            if (errors != "")
                throw new Exception(errors);


            var addedContractHolder = _contractHolderRepository.Add(contractHolderDomain);
            if (addedContractHolder == null)
                throw new Exception("Model not added to DB");
            return addedContractHolder;
        }

        public ContractHolderDomain Update(Guid id, ContractHolderDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
