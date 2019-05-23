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
    public class BeneficiaryRepository : IReadOnlyRepository<Beneficiary>, IWriteRepository<Beneficiary>
    {
        private readonly ConfigurationContext _db;

        public BeneficiaryRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Beneficiary Find(Guid id) => _db
            .Beneficiaries
            .FirstOrDefault(ben => ben.BeneficiaryId == id);

        public IEnumerable<Beneficiary> Get() => _db
            .Beneficiaries
            .ToList();

        public void Add(Beneficiary beneficiary)
        {
            if(beneficiary != null)
            {
                _db.Add(beneficiary);
                _db.SaveChanges();
            }
        }

        public Beneficiary Remove(Beneficiary beneficiary)
        {
            if(beneficiary != null)
            {
                _db.Remove(beneficiary);
                _db.SaveChanges();
            }
            return beneficiary;
        }

        public Beneficiary Update(Beneficiary beneficiary)
        {
            if(beneficiary != null)
            {
                _db.Update(beneficiary);
                _db.SaveChanges();
            }

            return beneficiary;
        }
    }
}
