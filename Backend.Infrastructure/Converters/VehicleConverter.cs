using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;
using System;

namespace Backend.Infrastructure.Converters
{
    public class VehicleConverter : IConverter<VehicleDomain, VehicleEntity>
    {
        public VehicleDomain Convert(VehicleEntity vehicleEntity)
        {
            if (vehicleEntity == null)
                return null;

            var vehicleDomain = new VehicleDomain()
            {
                BeneficiaryId = vehicleEntity.BeneficiaryId,
                IsDeleted = vehicleEntity.IsDeleted,
                VehicleBrand = vehicleEntity.VehicleBrand,
                VehicleChassisNumber = vehicleEntity.VehicleChassisNumber,
                VehicleColor = vehicleEntity.VehicleColor,
                VehicleCurrentFipeValue = vehicleEntity.VehicleCurrentFipeValue,
                VehicleCurrentMileage = vehicleEntity.VehicleCurrentMileage,
                VehicleDoneInspection = vehicleEntity.VehicleDoneInspection,
                VehicleManufactoringYear = vehicleEntity.VehicleManufactoringYear,
                VehicleModel = vehicleEntity.VehicleModel,
                VehicleModelYear = vehicleEntity.VehicleModelYear
            };

            return vehicleDomain;
        }

        public VehicleEntity Convert(VehicleDomain vehicleDomain)
        {
            if (vehicleDomain == null)
                return null;

            var vehicleEntity = new VehicleEntity()
            {
                BeneficiaryId = vehicleDomain.BeneficiaryId,
                IsDeleted = vehicleDomain.IsDeleted,
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

            return vehicleEntity;
        }
    }
}
