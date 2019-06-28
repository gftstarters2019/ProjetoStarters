using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

namespace Backend.Services.Validators
{
    public class MobileDeviceValidator  : IMobileDeviceValidator
    {
        private readonly IDateValidator _dateValidator;
        private readonly INumberValidator _numberValidator;

        public MobileDeviceValidator(IDateValidator dateValidator, INumberValidator numberValidator)
        {
            _dateValidator = dateValidator;
            _numberValidator = numberValidator;
        }
        public List<string> IsValid(MobileDeviceDomain mobileDevice)
        {
            var errors = new List<string>();

            errors.AddRange(_dateValidator.IsValid(mobileDevice.MobileDeviceManufactoringYear));
            errors.AddRange(_numberValidator.IsPositive(mobileDevice.MobileDeviceInvoiceValue.ToString()));

            return errors;
        }
    }
}
