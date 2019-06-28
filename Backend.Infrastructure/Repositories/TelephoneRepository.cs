using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class TelephoneRepository : IRepository<TelephoneEntity>
    {
        private readonly ConfigurationContext _db;
        private bool disposed = false;

        public TelephoneRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public TelephoneEntity Find(Guid id) => _db
            .Telephones
            .FirstOrDefault(tel => tel.TelephoneId == id);

        public IEnumerable<TelephoneEntity> Get() => _db
            .Telephones
            .ToList();

        public TelephoneEntity Add(TelephoneEntity telephone)
        {
            if (telephone != null)
            {
                telephone.TelephoneId = Guid.NewGuid();
                return _db.Telephones.Add(telephone).Entity;
            }
            return null;
        }

        public bool Remove(Guid id)
        {
            var telephone = Find(id);
            if(telephone != null)
            {
                _db.Remove(telephone);
                return true;
            }

            return false;
        }

        public TelephoneEntity Update(Guid id, TelephoneEntity telephone)
        {
            if(telephone != null)
            {
                _db.Update(telephone);
            }

            return telephone;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
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
    }
}
