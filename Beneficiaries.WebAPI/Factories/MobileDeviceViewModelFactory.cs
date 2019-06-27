using Backend.Core.Domains;
using Beneficiaries.WebAPI.Factories.Interfaces;
using Beneficiaries.WebAPI.ViewModels;

namespace Beneficiaries.WebAPI.Factories
{
    public class MobileDeviceViewModelFactory : IFactory<MobileDeviceViewModel, MobileDeviceDomain>
    {
        public MobileDeviceViewModel Create(MobileDeviceDomain mobileDeviceDomain)
        {
            if (mobileDeviceDomain == null)
                return null;

            return new MobileDeviceViewModel()
            {
                BeneficiaryId = mobileDeviceDomain.BeneficiaryId,
                MobileDeviceBrand = mobileDeviceDomain.MobileDeviceBrand,
                MobileDeviceInvoiceValue = mobileDeviceDomain.MobileDeviceInvoiceValue,
                MobileDeviceManufactoringYear = mobileDeviceDomain.MobileDeviceManufactoringYear,
                MobileDeviceModel = mobileDeviceDomain.MobileDeviceModel,
                MobileDeviceSerialNumber = mobileDeviceDomain.MobileDeviceSerialNumber,
                MobileDeviceType = mobileDeviceDomain.MobileDeviceType
            };
        }
    }
}
