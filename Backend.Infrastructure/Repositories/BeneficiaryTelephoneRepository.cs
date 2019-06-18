using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryTelephoneRepository : IRepository<BeneficiaryTelephone>
    {
        private readonly ConfigurationContext _db;

        public BeneficiaryTelephoneRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public BeneficiaryTelephone Find(Guid id) => _db
            .Individual_Telephone
            .FirstOrDefault(ad => ad.BeneficiaryTelephoneId == id);

        public IEnumerable<BeneficiaryTelephone> Get() => _db
            .Individual_Telephone
            .ToList();

        public bool Add(BeneficiaryTelephone telephones)
        {
            if (telephones != null)
            {
                _db.Add(telephones);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var telephones = Find(id);
            if (telephones != null)
            {
                _db.Remove(telephones);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public BeneficiaryTelephone Update(Guid id, BeneficiaryTelephone telephones)
        {
            if (telephones != null)
            {
                _db.Update(telephones);
                _db.SaveChanges();
            }

            return telephones;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
