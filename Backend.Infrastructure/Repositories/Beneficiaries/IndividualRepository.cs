using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class IndividualRepository : IRepository<IndividualEntity>
    {
        private readonly ConfigurationContext _db;
        private bool disposed = false;

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

        public IndividualEntity Update(Guid id, IndividualEntity updatedIndividual)
        {
            if (updatedIndividual != null)
            {
                var individualToUpdate = Find(id);
                if (individualToUpdate != null)
                {
                    // Verifies if CPF is already in DB of Individual not deleted
                    if (_db.Individuals
                            .Where(ind => ind.IndividualCPF == updatedIndividual.IndividualCPF && !ind.IsDeleted)
                            .Any() && updatedIndividual.IndividualCPF != individualToUpdate.IndividualCPF)
                        return null;

                    individualToUpdate.IndividualBirthdate = updatedIndividual.IndividualBirthdate;
                    individualToUpdate.IndividualCPF = updatedIndividual.IndividualCPF;
                    individualToUpdate.IndividualEmail = updatedIndividual.IndividualEmail;
                    individualToUpdate.IndividualName = updatedIndividual.IndividualName;
                    individualToUpdate.IndividualRG = updatedIndividual.IndividualRG;
                    individualToUpdate.IsDeleted = updatedIndividual.IsDeleted;

                    return _db.Individuals.Update(individualToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
