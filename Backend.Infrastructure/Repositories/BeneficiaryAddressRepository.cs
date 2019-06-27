using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryAddressRepository : IRepository<BeneficiaryAddress>
    {
        private readonly ConfigurationContext _db;
        private bool disposed = false;

        public BeneficiaryAddressRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public BeneficiaryAddress Add(BeneficiaryAddress beneficiaryAddress)
        {
            if (beneficiaryAddress != null)
            {
                beneficiaryAddress.BeneficiaryAddressId = Guid.NewGuid();
                return _db.Beneficiary_Address.Add(beneficiaryAddress).Entity;
            }
            return null;
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

        public BeneficiaryAddress Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeneficiaryAddress> Get() => _db.Beneficiary_Address;

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public BeneficiaryAddress Update(Guid id, BeneficiaryAddress t)
        {
            throw new NotImplementedException();
        }
    }
}
