using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

namespace Backend.Services.Validators
{
    public class TelephoneValidator : ITelephoneValidator
    {
        private readonly INumberValidator _numberValidator;

        public TelephoneValidator(INumberValidator numberValidator)
        {
            _numberValidator = numberValidator;
        }

        public List<string> IsValid(TelephoneDomain telephone)
        {
            return _numberValidator.LengthValidator(telephone.TelephoneNumber, 10);
        }
    }
}
