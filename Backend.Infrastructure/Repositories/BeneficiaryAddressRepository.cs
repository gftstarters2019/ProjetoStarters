using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryAddressRepository : IRepository<BeneficiaryAddress>
    {
        private readonly ConfigurationContext _db;

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

        public BeneficiaryAddress Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeneficiaryAddress> Get()
        {
            throw new NotImplementedException();
        }

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
