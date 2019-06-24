using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;

namespace Backend.Services.Validators
{
    public class TelephoneValidator : ITelephoneValidator
    {
        private readonly INumberValidator _numberValidator;

        public TelephoneValidator(INumberValidator numberValidator)
        {
            _numberValidator = numberValidator;
        }

        public bool IsValid(TelephoneDomain telephone)
        {
            if (!_numberValidator.LengthValidator(telephone.TelephoneNumber, 10))
                return false;
            return true;
        }
    }
}
