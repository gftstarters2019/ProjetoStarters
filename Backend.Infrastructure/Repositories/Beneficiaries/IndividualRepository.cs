using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class IndividualRepository : IReadOnlyRepository<Individual>, IWriteRepository<Individual>
    {
        private readonly ConfigurationContext _db;

        public IndividualRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(Individual t)
        {
            throw new NotImplementedException();
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

        public Individual Update(Guid id, Individual t)
        {
            throw new NotImplementedException();
        }
    }
}
