using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class VehicleService : IService<VehicleDomain>
    {
        private IRepository<VehicleEntity> _vehicleRepository;

        public VehicleService(IRepository<VehicleEntity> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public VehicleDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public VehicleDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDomain> GetAll()
        {
            return _vehicleRepository.Get().Select(veh => ConvertersManager.VehicleConverter.Convert(veh)).ToList();
        }

        public VehicleDomain Save(VehicleDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public VehicleDomain Update(Guid id, VehicleDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
