using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ModelValidations
{
    public static class VehicleValidations
    {
        public static bool VehicleIsValid(Vehicle vehicle)
        {
            if (!ValidationsHelper.DateIsValid(vehicle.VehicleManufactoringYear))
                return false;

            if (!ValidationsHelper.DateIsValid(vehicle.VehicleModelYear))
                return false;

            if (vehicle.VehicleCurrentFipeValue < 0 && vehicle.VehicleCurrentMileage <= 0)
                return false;

            return true;
        }
    }
}
