using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Backend.Services.Services
{
    public class CompleteContractService : IService<CompleteContract>
    {
        private IRepository<CompleteContract> _completeContractRepository;

        public CompleteContractService(IRepository<CompleteContract> completeContractRespository)
        {
            _completeContractRepository = completeContractRespository;
        }

        public CompleteContract Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CompleteContract Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CompleteContract> GetAll()
        {
            throw new NotImplementedException();
        }

        public CompleteContract Save(CompleteContract completeContract)
        {
            if (completeContract == null)
                return null;

            /*
             * Validations
            */

            return _completeContractRepository.Add(completeContract);
        }

        public CompleteContract Update(Guid id, CompleteContract completeContract)
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
