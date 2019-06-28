using Backend.Core.Domains;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class CompleteContractService : IService<CompleteContractDomain>
    {
        private IRepository<CompleteContractDomain> _completeContractRepository;

        public CompleteContractService(IRepository<CompleteContractDomain> completeContractRespository)
        {
            _completeContractRepository = completeContractRespository;
        }

        public CompleteContractDomain Delete(Guid id)
        {
            var contractToBeDeleted = _completeContractRepository.Find(id);
            if (contractToBeDeleted == null)
                return null;

            contractToBeDeleted.Contract.ContractDeleted = !contractToBeDeleted.Contract.ContractDeleted;
            var deletedContract = _completeContractRepository.Update(id, contractToBeDeleted);
            if (deletedContract == null)
                return null;
            _completeContractRepository.Save();
            return deletedContract;
        }

        public CompleteContractDomain Get(Guid id)
        {
            return _completeContractRepository.Find(id);
        }

        public List<CompleteContractDomain> GetAll()
        {
            return _completeContractRepository.Get().ToList();
        }

        public CompleteContractDomain Save(CompleteContractDomain completeContract)
        {
            if (completeContract == null)
                return null;

            /*
             * Validations
            */

            return _completeContractRepository.Add(completeContract);
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
