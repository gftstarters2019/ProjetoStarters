using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class IndividualRepository : IRepository<Individual>
    {
        private readonly ConfigurationContext _db;

        public IndividualRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Individual Add(Individual individual)
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

        public Individual Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Individual> Get() => _db
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

        public Individual Update(Guid id, Individual individual)
        {
            if (individual != null)
            {
                var individualToUpdate = Find(id);
                if (individualToUpdate != null)
                {
                    // Verifies if CPF is already in DB of Individual not deleted
                    if (_db.Individuals
                            .Where(ind => ind.IndividualCPF == individual.IndividualCPF && !ind.IsDeleted)
                            .Any())
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
