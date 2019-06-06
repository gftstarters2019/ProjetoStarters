using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class MobileDeviceRepository : IReadOnlyRepository<MobileDevice>, IWriteRepository<MobileDevice>
    {
        private readonly ConfigurationContext _db;

        public MobileDeviceRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(MobileDevice t)
        {
            throw new NotImplementedException();
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

        public MobileDevice Update(Guid id, MobileDevice t)
        {
            throw new NotImplementedException();
        }
    }
}
