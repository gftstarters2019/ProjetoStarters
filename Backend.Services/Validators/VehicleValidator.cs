using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

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

        public List<string> IsValid(VehicleDomain vehicle)
        {
            var errors = new List<string>();

            errors.AddRange(_dateValidator.IsValid(vehicle.VehicleManufactoringYear));
            errors.AddRange(_dateValidator.IsValid(vehicle.VehicleModelYear));
            errors.AddRange(_numberValidator.IsPositive(vehicle.VehicleCurrentFipeValue.ToString()));
            errors.AddRange(_numberValidator.IsPositive(vehicle.VehicleCurrentMileage.ToString()));

            return errors;
        }
    }
}
