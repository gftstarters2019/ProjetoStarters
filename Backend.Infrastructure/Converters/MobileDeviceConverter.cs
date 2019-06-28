using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class MobileDeviceConverter : IConverter<MobileDeviceDomain, MobileDeviceEntity>
    {
        public MobileDeviceDomain Convert(MobileDeviceEntity mobileDeviceEntity)
        {
            if (mobileDeviceEntity == null)

                return null;
            var mobileDeviceDomain = new MobileDeviceDomain()
            {
                BeneficiaryId = mobileDeviceEntity.BeneficiaryId,
                IsDeleted = mobileDeviceEntity.IsDeleted,
                MobileDeviceBrand = mobileDeviceEntity.MobileDeviceBrand,
                MobileDeviceInvoiceValue = mobileDeviceEntity.MobileDeviceInvoiceValue,
                MobileDeviceManufactoringYear = mobileDeviceEntity.MobileDeviceManufactoringYear,
                MobileDeviceModel = mobileDeviceEntity.MobileDeviceModel,
                MobileDeviceSerialNumber = mobileDeviceEntity.MobileDeviceSerialNumber,
                MobileDeviceType = mobileDeviceEntity.MobileDeviceType
            };

            return mobileDeviceDomain;
        }

        public MobileDeviceEntity Convert(MobileDeviceDomain mobileDeviceDomain)
        {
            if (mobileDeviceDomain == null)
                return null;

            var mobileDeviceEntity = new MobileDeviceEntity()
            {
                BeneficiaryId = mobileDeviceDomain.BeneficiaryId,
                IsDeleted = mobileDeviceDomain.IsDeleted,
                MobileDeviceBrand = mobileDeviceDomain.MobileDeviceBrand,
                MobileDeviceInvoiceValue = mobileDeviceDomain.MobileDeviceInvoiceValue,
                MobileDeviceManufactoringYear = mobileDeviceDomain.MobileDeviceManufactoringYear,
                MobileDeviceModel = mobileDeviceDomain.MobileDeviceModel,
                MobileDeviceSerialNumber = mobileDeviceDomain.MobileDeviceSerialNumber,
                MobileDeviceType = mobileDeviceDomain.MobileDeviceType
            };

            return mobileDeviceEntity;
        }
    }
}
