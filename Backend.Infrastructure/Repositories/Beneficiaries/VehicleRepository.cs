using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly ConfigurationContext _db;

        public VehicleRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Vehicle Add(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                // Verifies if Chassis Number is already in DB
                if (_db.Vehicles
                        .Where(vec => vec.VehicleChassisNumber == vehicle.VehicleChassisNumber
                                      && !vec.IsDeleted)
                        .Any())
                    return null;

                vehicle.IsDeleted = false;
                vehicle.BeneficiaryId = Guid.NewGuid();

                return _db.Vehicles.Add(vehicle).Entity;
            }
            return null;
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

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public Vehicle Update(Guid id, Vehicle t)
        {
            throw new NotImplementedException();
        }
    }
}
