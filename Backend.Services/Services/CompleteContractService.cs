using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Transactions;

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
            throw new NotImplementedException();
        }

        public CompleteContractDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CompleteContractDomain> GetAll()
        {
            throw new NotImplementedException();
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
