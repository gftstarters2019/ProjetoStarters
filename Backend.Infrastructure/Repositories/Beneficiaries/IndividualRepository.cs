using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Backend.Infrastructure.Repositories
{
    public class IndividualRepository : IRepository<IndividualEntity>
    {
        private readonly ConfigurationContext _db;

        public IndividualRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public IndividualEntity Add(IndividualEntity individual)
        {
            if (individual != null)
            {
                // Verifies if CPF is already in DB of Individual not deleted
                if (_db.Individuals
                        .Where(ind => ind.IndividualCPF == individual.IndividualCPF && !ind.IsDeleted)
                        .Any())
                    return null;

                individual.IsDeleted = false;
                individual.BeneficiaryId = Guid.NewGuid();
                return _db.Individuals.Add(individual).Entity;
            }
            return null;
        }

        public IndividualEntity Find(Guid id) => _db.Individuals.Where(ind => ind.BeneficiaryId == id).FirstOrDefault();

        public IEnumerable<IndividualEntity> Get() => _db
            .Individuals
            .Where(i => !i.IsDeleted)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public IndividualEntity Update(Guid id, IndividualEntity individual)
        {
            if (individual != null)
            {
                var individualToUpdate = Find(id);
                if (individualToUpdate != null)
                {
                    // Verifies if CPF is already in DB of Individual not deleted
                    if (_db.Individuals
                            .Where(ind => ind.IndividualCPF == individual.IndividualCPF && !ind.IsDeleted)
                            .Any() && individual.IndividualCPF != individualToUpdate.IndividualCPF)
                        return null;

                    individualToUpdate.IndividualBirthdate = individual.IndividualBirthdate;
                    individualToUpdate.IndividualCPF = individual.IndividualCPF;
                    individualToUpdate.IndividualEmail = individual.IndividualEmail;
                    individualToUpdate.IndividualName = individual.IndividualName;
                    individualToUpdate.IndividualRG = individual.IndividualRG;
                    individualToUpdate.IsDeleted = individual.IsDeleted;

                    return _db.Individuals.Update(individualToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
