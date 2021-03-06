﻿using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class SignedContractRepository : IRepository<SignedContractEntity>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<ContractEntity> _contractRepository;
        private bool disposed = false;

        public SignedContractRepository(ConfigurationContext db,
                                        IRepository<ContractEntity> contractRepository)
        {
            _db = db;
            _contractRepository = contractRepository;
        }

        public SignedContractEntity Add(SignedContractEntity signedContract)
        {
            var signedContractContractHolder = _db
                                               .Individuals
                                               .Where(ind => ind.BeneficiaryId == signedContract.BeneficiaryId)
                                               .FirstOrDefault();
            if (signedContractContractHolder == null)
                return null;

            signedContract.SignedContractId = Guid.NewGuid();

            return _db.SignedContracts.Add(signedContract).Entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public SignedContractEntity Find(Guid id)
        {
            var signedContract = _db.SignedContracts.FirstOrDefault(sc => sc.SignedContractId == id);
            if (signedContract == null)
                return null;

            signedContract.SignedContractContract = _contractRepository.Find(signedContract.ContractId);
            return signedContract;
        }

        public IEnumerable<SignedContractEntity> Get() => _db
            .SignedContracts
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public SignedContractEntity Update(Guid id, SignedContractEntity signedContract)
        {
            if (signedContract != null)
            {
                var signedContractToUpdate = Find(id);
                if (signedContractToUpdate != null)
                {
                    signedContractToUpdate.ContractIndividualIsActive = signedContract.ContractIndividualIsActive;
                    signedContractToUpdate.BeneficiaryId = signedContract.BeneficiaryId;
                    return _db.SignedContracts.Update(signedContractToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
