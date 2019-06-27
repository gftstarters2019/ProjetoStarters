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
            var contractHolderToDelete = _contractHolderRepository.Find(id);
            if (contractHolderToDelete == null)
                return null;

            contractHolderToDelete.Individual.IsDeleted = !contractHolderToDelete.Individual.IsDeleted;
            var deletedBeneficiary = _contractHolderRepository.Update(id, contractHolderToDelete);
            if (deletedBeneficiary != null)
            {
                _contractHolderRepository.Save();
                return deletedBeneficiary;
            }
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

            try
            {
                var addedContractHolder = _contractHolderRepository.Add(contractHolderDomain);
                SendEmail(addedContractHolder);
                return addedContractHolder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendEmail(ContractHolderDomain addedContractHolder)
        {
            new EmailService().SendEmail("Welcome!",
                $"Welcome {addedContractHolder.Individual.IndividualName}!",
                addedContractHolder.Individual.IndividualEmail);
        }

        public ContractHolderDomain Update(Guid id, ContractHolderDomain contractToBeUpdated)
        {
            var errors = string.Empty;

            if (contractToBeUpdated == null)
                return null;

            var validationErrorsList = _contractHolderValidator.IsValid(contractToBeUpdated.Individual, contractToBeUpdated.IndividualAddresses, contractToBeUpdated.IndividualTelephones);

            if (validationErrorsList.Any())
                foreach (var er in validationErrorsList)
                {
                    errors += er;
                }

            if (errors != "")
                throw new Exception(errors);

            return _contractHolderRepository.Update(id, contractToBeUpdated);
        }
    }
}
