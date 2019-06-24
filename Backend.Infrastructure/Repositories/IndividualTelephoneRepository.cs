using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class IndividualTelephoneRepository : IRepository<IndividualTelephone>
    {
        private readonly ConfigurationContext _db;

        public IndividualTelephoneRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public IndividualTelephone Find(Guid id) => _db
            .Individual_Telephone
            .FirstOrDefault(ad => ad.BeneficiaryTelephoneId == id);

        public IEnumerable<IndividualTelephone> Get() => _db
            .Individual_Telephone
            .ToList();

        IndividualTelephone IRepository<IndividualTelephone>.Add(IndividualTelephone beneficiaryTelephone)
        {
            if (beneficiaryTelephone != null)
            {
                beneficiaryTelephone.BeneficiaryTelephoneId = Guid.NewGuid();
                return _db.Individual_Telephone.Add(beneficiaryTelephone).Entity;
            }
            return null;
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

        public IndividualTelephone Update(Guid id, IndividualTelephone telephones)
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
            return _db.SaveChanges() > 0;
        }
    }
}
