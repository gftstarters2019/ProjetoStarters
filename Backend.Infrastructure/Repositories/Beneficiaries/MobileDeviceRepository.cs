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

        public MobileDevice Add(MobileDevice mobile)
        {
            if (mobile != null)
            {
                // Verifies if Serial Number is already in DB of active MobileDevice
                if (_db.MobileDevices
                        .Where(mob => mob.MobileDeviceSerialNumber == mobile.MobileDeviceSerialNumber
                                      && !mob.IsDeleted)
                        .Any())
                    return null;

                mobile.IsDeleted = false;
                mobile.BeneficiaryId = Guid.NewGuid();

                return _db.MobileDevices.Add(mobile).Entity;
            }
            return null;
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
            return _db.SaveChanges() > 0;
        }

        public MobileDevice Update(Guid id, MobileDevice mobileDevice)
        {
            if (mobileDevice != null)
            {
                var mobileDeviceToUpdate = Find(id);
                if (mobileDeviceToUpdate != null)
                {
                    // Verifies if Serial Number is already in DB of active MobileDevice
                    if (_db.MobileDevices
                    .Where(mob => mob.MobileDeviceSerialNumber == mobileDevice.MobileDeviceSerialNumber
                                  && !mob.IsDeleted)
                    .Any())
                        return null;

                    mobileDeviceToUpdate.IsDeleted = mobileDevice.IsDeleted;
                    mobileDeviceToUpdate.MobileDeviceBrand = mobileDevice.MobileDeviceBrand;
                    mobileDeviceToUpdate.MobileDeviceInvoiceValue = mobileDevice.MobileDeviceInvoiceValue;
                    mobileDeviceToUpdate.MobileDeviceManufactoringYear = mobileDevice.MobileDeviceManufactoringYear;
                    mobileDeviceToUpdate.MobileDeviceModel = mobileDevice.MobileDeviceModel;
                    mobileDeviceToUpdate.MobileDeviceSerialNumber = mobileDevice.MobileDeviceSerialNumber;
                    mobileDeviceToUpdate.MobileDeviceType = mobileDevice.MobileDeviceType;

                    return _db.MobileDevices.Update(mobileDeviceToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
