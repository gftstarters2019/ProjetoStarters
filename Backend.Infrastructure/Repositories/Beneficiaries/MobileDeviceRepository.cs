using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class MobileDeviceRepository : IRepository<MobileDeviceEntity>
    {
        private readonly ConfigurationContext _db;

        public MobileDeviceRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public MobileDeviceEntity Add(MobileDeviceEntity mobile)
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

        public MobileDeviceEntity Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MobileDeviceEntity> Get() => _db
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

        public MobileDeviceEntity Update(Guid id, MobileDeviceEntity mobileDevice)
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
