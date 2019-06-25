using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryRepository : IRepository<BeneficiaryEntity>
    {
        private readonly ConfigurationContext _db;

        public BeneficiaryRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public BeneficiaryEntity Find(Guid id)
        {
            BeneficiaryEntity beneficiary;

            // Individual
            beneficiary = _db
                            .Individuals
                            .FirstOrDefault(ben => ben.BeneficiaryId == id);
            if (beneficiary != null)
                return beneficiary;
            
            // Pet
            beneficiary = _db
                            .Pets
                            .FirstOrDefault(ben => ben.BeneficiaryId == id);
            if (beneficiary != null)
                return beneficiary;

            // Mobile Device
            beneficiary = _db
                            .MobileDevices
                            .FirstOrDefault(ben => ben.BeneficiaryId == id);
            if (beneficiary != null)
                return beneficiary;

            // Realty
            beneficiary = _db
                            .Realties
                            .FirstOrDefault(ben => ben.BeneficiaryId == id);
            if (beneficiary != null)
                return beneficiary;

            // Vehicle
            beneficiary = _db
                            .Vehicles
                            .FirstOrDefault(ben => ben.BeneficiaryId == id);
            if (beneficiary != null)
                return beneficiary;

            return null;
        }

        public IEnumerable<BeneficiaryEntity> Get() => _db
            .Individuals
            .ToList();
        
        public bool Remove(Guid id)
        {
            var beneficiary = Find(id);
            if(beneficiary != null)
            {
                _db.Remove(beneficiary);
                return true;
            }
            return false;
        }

        public BeneficiaryEntity Update(Guid id, BeneficiaryEntity beneficiary)
        {
            if(beneficiary != null)
                _db.Update(beneficiary);

            return beneficiary;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public BeneficiaryEntity Add(BeneficiaryEntity t)
        {
            throw new NotImplementedException();
        }
    }
}
