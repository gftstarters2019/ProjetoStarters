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
    public class ContractHolderRepository : IReadOnlyRepository<Individual>, IWriteRepository<Individual>
    {
        private readonly ConfigurationContext _db;

        public ContractHolderRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Individual Find(Guid id) => _db
            .Individuals
            .FirstOrDefault(ind => ind.IndividualId == id);

        public IEnumerable<Individual> Get() => _db
            .Individuals
            .ToList();

        public void Add(Individual individual)
        {
            if(individual != null)
            {
                _db.Add(individual);
                _db.SaveChanges();
            }
        }

        public Individual Remove(Individual individual)
        {
            if(individual != null)
            {
                _db.Remove(individual);
                _db.SaveChanges();
            }

            return individual;
        }

        public Individual Update(Individual individual)
        {
            if(individual != null)
            {
                _db.Update(individual);
                _db.SaveChanges();
            }

            return individual;
        }
    }
}
