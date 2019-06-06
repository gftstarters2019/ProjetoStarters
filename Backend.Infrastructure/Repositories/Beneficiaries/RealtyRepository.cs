using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class RealtyRepository : IReadOnlyRepository<Realty>, IWriteRepository<Realty>
    {
        private readonly ConfigurationContext _db;

        public RealtyRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(Realty realty)
        {
            if (realty != null)
            {
                _db.Realties.Add(realty);
                if (_db.SaveChanges() == 1)
                    return true;
            }
            return false;
        }

        public Realty Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Realty> Get() => _db
            .Realties
            .Where(i => !i.IsDeleted)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Realty Update(Guid id, Realty t)
        {
            throw new NotImplementedException();
        }
    }
}
