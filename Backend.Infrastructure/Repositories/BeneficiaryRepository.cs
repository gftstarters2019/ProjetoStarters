using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryRepository : IRepository<Beneficiary>
    {
        private readonly ConfigurationContext _db;

        public BeneficiaryRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Beneficiary Find(Guid id)
        {
            Beneficiary beneficiary;

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

        public IEnumerable<Beneficiary> Get() => _db
            //.Beneficiaries
            .Individuals
            .ToList();

        public bool Add(Beneficiary beneficiary)
        {
            if(beneficiary != null)
            {
                _db.Add(beneficiary);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var beneficiary = Find(id);
            if(beneficiary != null)
            {
                _db.Remove(beneficiary);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Beneficiary Update(Guid id, Beneficiary beneficiary)
        {
            if(beneficiary != null)
            {
                _db.Update(beneficiary);
                _db.SaveChanges();
            }

            return beneficiary;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Beneficiary FindCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
