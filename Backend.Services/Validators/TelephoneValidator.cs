using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Services.Validators
{
    public class TelephoneValidator : ITelephoneValidator
    {
        private readonly INumberValidator _numberValidator;

        public TelephoneValidator(INumberValidator numberValidator)
        {
            _numberValidator = numberValidator;
        }

        public bool IsValid(Telephone telephone)
        {
            if (!_numberValidator.LengthValidator(telephone.TelephoneNumber, 10))
                return false;
            return true;
        }
    }
}
