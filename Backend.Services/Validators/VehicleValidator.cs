using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;

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

        public bool IsValid(VehicleDomain vehicle)
        {
            if (!_dateValidator.IsValid(vehicle.VehicleManufactoringYear) && !_dateValidator.IsValid(vehicle.VehicleModelYear))
                return false;
            if (!_numberValidator.IsPositive(vehicle.VehicleCurrentFipeValue.ToString()) && !_numberValidator.IsPositive(vehicle.VehicleCurrentMileage.ToString()))
                return false;
            return true;
        }
    }
}
