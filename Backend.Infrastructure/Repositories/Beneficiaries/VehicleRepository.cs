using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class VehicleRepository : IReadOnlyRepository<Vehicle>, IWriteRepository<Vehicle>
    {
        private readonly ConfigurationContext _db;

        public VehicleRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(Vehicle t)
        {
            throw new NotImplementedException();
        }

        public Vehicle Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> Get() => _db
            .Vehicles
            .Where(i => !i.IsDeleted)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Vehicle Update(Guid id, Vehicle t)
        {
            throw new NotImplementedException();
        }
    }
}
