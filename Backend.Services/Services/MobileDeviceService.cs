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
    public class MobileDeviceService : IService<MobileDeviceDomain>
    {
        private IRepository<MobileDeviceEntity> _mobileDeviceRepository;

        public MobileDeviceService(IRepository<MobileDeviceEntity> mobileDeviceRepository)
        {
            _mobileDeviceRepository = mobileDeviceRepository;
        }

        public MobileDeviceDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public MobileDeviceDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MobileDeviceDomain> GetAll()
        {
            return _mobileDeviceRepository.Get().Select(ben => ConvertersManager.MobileDeviceConverter.Convert(ben)).ToList();
        }

        public MobileDeviceDomain Save(MobileDeviceDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public MobileDeviceDomain Update(Guid id, MobileDeviceDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
