using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Services.Services.Contracts;
using System;
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
