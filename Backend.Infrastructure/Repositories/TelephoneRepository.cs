using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class TelephoneRepository : IReadOnlyRepository<Telephone>, IWriteRepository<Telephone>
    {
        private readonly ConfigurationContext _db;

        public TelephoneRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Telephone Find(Guid id) => _db
            .Telephones
            .FirstOrDefault(tel => tel.TelephoneId == id);

        public IEnumerable<Telephone> Get() => _db
            .Telephones
            .ToList();

        public void Add(Telephone telephone)
        {
            if(telephone != null)
            {
                _db.Add(telephone);
                _db.SaveChanges();
            }
        }

        public Telephone Remove(Telephone telephone)
        {
            if(telephone != null)
            {
                _db.Remove(telephone);
                _db.SaveChanges();
            }

            return telephone;
        }

        public Telephone Update(Telephone telephone)
        {
            if(telephone != null)
            {
                _db.Update(telephone);
                _db.SaveChanges();
            }

            return telephone;
        }
    }
}
