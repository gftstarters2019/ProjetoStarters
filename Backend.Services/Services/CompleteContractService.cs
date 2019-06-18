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

        public bool Save(CompleteContract modelToAddToDB)
        {
            if (modelToAddToDB == null)
                return false;

            /*
             * Validations
            */

            return _completeContractRepository.Add(modelToAddToDB);
        }
    }
}
