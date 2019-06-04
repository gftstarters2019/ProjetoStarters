using Backend.Application.Interfaces;
using Backend.Core.Enums;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class MobileDeviceFactory : IFactory<MobileDevice>
    {
        public MobileDevice Create(Guid mobileDeviceId, string mobileDeviceBrand, string mobileDeviceModel, string mobileDeviceSerialNumber, DateTime mobileDeviceManufactoringYear, MobileDeviceType mobileDeviceType, double mobileDeviceInvoiceValue)
        {
            var mobileDevice = new MobileDevice();
            mobileDevice.MobileDeviceId = Guid.NewGuid();
            mobileDevice.MobileDeviceBrand = mobileDeviceBrand;
            mobileDevice.MobileDeviceModel = mobileDeviceModel;
            mobileDevice.MobileDeviceSerialNumber = mobileDeviceSerialNumber;
            mobileDevice.MobileDeviceManufactoringYear = mobileDeviceManufactoringYear;
            mobileDevice.MobileDeviceType = mobileDeviceType;
            mobileDevice.MobileDeviceInvoiceValue = mobileDeviceInvoiceValue;

            return mobileDevice;
        }
                        
        public MobileDevice Create()
        {
            return new MobileDevice();
        }
    }
}