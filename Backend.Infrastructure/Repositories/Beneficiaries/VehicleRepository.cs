using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class VehicleRepository : IRepository<VehicleEntity>
    {
        private readonly ConfigurationContext _db;

        public VehicleRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public VehicleEntity Add(VehicleEntity vehicle)
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

        public VehicleEntity Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleEntity> Get() => _db
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

        public VehicleEntity Update(Guid id, VehicleEntity vehicle)
        {
            if (vehicle != null)
            {
                // Verifies if Chassis Number is already in DB
                if (_db.Vehicles
                        .Where(vec => vec.VehicleChassisNumber == vehicle.VehicleChassisNumber
                                      && !vec.IsDeleted)
                        .Any())
                    return null;

                var vehicleToUpdate = Find(id);
                if(vehicleToUpdate != null)
                {
                    vehicleToUpdate.IsDeleted = vehicle.IsDeleted;
                    vehicleToUpdate.VehicleBrand = vehicle.VehicleBrand;
                    vehicleToUpdate.VehicleChassisNumber = vehicle.VehicleChassisNumber;
                    vehicleToUpdate.VehicleColor = vehicle.VehicleColor;
                    vehicleToUpdate.VehicleCurrentFipeValue = vehicle.VehicleCurrentFipeValue;
                    vehicleToUpdate.VehicleCurrentMileage = vehicle.VehicleCurrentMileage;
                    vehicleToUpdate.VehicleDoneInspection = vehicle.VehicleDoneInspection;
                    vehicleToUpdate.VehicleManufactoringYear = vehicle.VehicleManufactoringYear;
                    vehicleToUpdate.VehicleModel = vehicle.VehicleModel;
                    vehicleToUpdate.VehicleModelYear = vehicle.VehicleModelYear;

                    return _db.Vehicles.Update(vehicleToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
