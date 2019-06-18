using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class MobileDeviceRepository : IRepository<MobileDevice>
    {
        private readonly ConfigurationContext _db;

        public MobileDeviceRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(MobileDevice mobile)
        {
            if (mobile != null)
            {
                _db.MobileDevices.Add(mobile);
                if (_db.SaveChanges() == 1)
                    return true;
            }
            return false;
        }

        public MobileDevice Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MobileDevice> Get() => _db
            .MobileDevices
            .Where(i => !i.IsDeleted)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public MobileDevice Update(Guid id, MobileDevice t)
        {
            throw new NotImplementedException();
        }
    }
}
