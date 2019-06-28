using System;
using System.Collections.Generic;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;

namespace Backend.Services.Services
{
    public class BeneficiaryService : IService<BeneficiaryEntity>
    {
        private IRepository<BeneficiaryEntity> _beneficiaryRepository;

        public BeneficiaryService(IRepository<BeneficiaryEntity> beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }

        public BeneficiaryEntity Delete(Guid id)
        {
            var beneficiaryToBeDeleted = _beneficiaryRepository.Find(id);
            if (beneficiaryToBeDeleted == null)
                return null;

            beneficiaryToBeDeleted.IsDeleted = !beneficiaryToBeDeleted.IsDeleted;
            var deletedBeneficiary = _beneficiaryRepository.Update(id, beneficiaryToBeDeleted);
            if (deletedBeneficiary == null)
                return null;
            _beneficiaryRepository.Save();
            return deletedBeneficiary;
        }

        public BeneficiaryEntity Get(Guid id)
        {
            return _beneficiaryRepository.Find(id);
        }

        public List<BeneficiaryEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public BeneficiaryEntity Save(BeneficiaryEntity modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public BeneficiaryEntity Update(Guid id, BeneficiaryEntity modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
