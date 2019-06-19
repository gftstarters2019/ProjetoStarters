using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
        public bool IsValid(MobileDevice mobileDevice)
        {
            if (!_dateValidator.IsValid(mobileDevice.MobileDeviceManufactoringYear))
                return false;
            if (!_numberValidator.IsPositive(mobileDevice.MobileDeviceInvoiceValue.ToString()))
                return false;
            return true;
        }
    }
}
