using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class TelephoneRepository : IRepository<Telephone>
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

        public bool Add(Telephone telephone)
        {
            if(telephone != null)
            {
                _db.Add(telephone);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var telephone = Find(id);
            if(telephone != null)
            {
                _db.Remove(telephone);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public Telephone Update(Guid id, Telephone telephone)
        {
            if(telephone != null)
            {
                _db.Update(telephone);
                _db.SaveChanges();
            }

            return telephone;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
