using Backend.Core.Domains;
using Beneficiaries.WebAPI.Factories.Interfaces;
using Beneficiaries.WebAPI.ViewModels;

namespace Beneficiaries.WebAPI.Factories
{
    public class VehicleViewModelFactory : IFactory<VehicleViewModel, VehicleDomain>
    {
        public VehicleViewModel Create(VehicleDomain vehicleDomain)
        {
            if (vehicleDomain == null)
                return null;

            return new VehicleViewModel()
            {
                BeneficiaryId = vehicleDomain.BeneficiaryId,
                VehicleBrand = vehicleDomain.VehicleBrand,
                VehicleChassisNumber = vehicleDomain.VehicleChassisNumber,
                VehicleColor = vehicleDomain.VehicleColor,
                VehicleCurrentFipeValue = vehicleDomain.VehicleCurrentFipeValue,
                VehicleCurrentMileage = vehicleDomain.VehicleCurrentMileage,
                VehicleDoneInspection = vehicleDomain.VehicleDoneInspection,
                VehicleManufactoringYear = vehicleDomain.VehicleManufactoringYear,
                VehicleModel = vehicleDomain.VehicleModel,
                VehicleModelYear = vehicleDomain.VehicleModelYear
            };
        }
    }
}
