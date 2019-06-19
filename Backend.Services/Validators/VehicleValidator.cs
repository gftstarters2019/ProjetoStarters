using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class VehicleValidator : IVehicleValidator
    {
        private readonly IDateValidator _dateValidator;
        private readonly INumberValidator _numberValidator;

        public VehicleValidator(IDateValidator dateValidator, INumberValidator numberValidator)
        {
            _dateValidator = dateValidator;
            _numberValidator = numberValidator;
        }

        public bool IsValid(Vehicle vehicle)
        {
            if (!_dateValidator.IsValid(vehicle.VehicleManufactoringYear) && !_dateValidator.IsValid(vehicle.VehicleModelYear))
                return false;
            if (!_numberValidator.IsPositive(vehicle.VehicleCurrentFipeValue.ToString()) && !_numberValidator.IsPositive(vehicle.VehicleCurrentMileage.ToString()))
                return false;
            return true;
        }
    }
}
