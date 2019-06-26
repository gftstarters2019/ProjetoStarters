using Backend.Core.Domains;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class CompleteContractService : IService<CompleteContractDomain>
    {
        private IRepository<CompleteContractDomain> _completeContractRepository;
        private readonly IContractValidator _contractValidator;

        public CompleteContractService(IRepository<CompleteContractDomain> completeContractRespository, IContractValidator contractValidator)
        {
            _completeContractRepository = completeContractRespository;
            _contractValidator = contractValidator;
        }

        public CompleteContractDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CompleteContractDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CompleteContractDomain> GetAll()
        {
            return _completeContractRepository.Get().ToList();
        }

        public CompleteContractDomain Save(CompleteContractDomain completeContract)
        {
            var errors = string.Empty;

            if (completeContract == null)
                return null;

            var validationErrorsList = _contractValidator.IsValid(completeContract.Contract, completeContract.Individuals, completeContract.MobileDevices, completeContract.Pets, completeContract.Realties, completeContract.Vehicles);

            if (validationErrorsList.Any())
                foreach (var er in validationErrorsList)
                {
                    errors += er;
                }

            if (errors != null)
                throw new Exception(errors);

            var addedContract = _completeContractRepository.Add(completeContract);
            if (addedContract == null)
                throw new Exception("Model not added to DB!");

            return addedContract;
        }

        public CompleteContractDomain Update(Guid id, CompleteContractDomain completeContract)
        {
            if (completeContract == null)
                return null;

            /*
             * Validations
            */

            return _completeContractRepository.Update(id, completeContract);
        }
    }
}
