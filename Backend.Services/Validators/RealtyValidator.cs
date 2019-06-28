using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

namespace Backend.Services.Validators
{
    public class RealtyValidator : IRealtyValidator
    {
        private IDateValidator _dateValidator;
        private INumberValidator _numberValidator;

        public RealtyValidator(IDateValidator dateValidator, INumberValidator numberValidator)
        {
            _dateValidator = dateValidator;
            _numberValidator = numberValidator;
        }
        public List<string> IsValid(RealtyDomain realty)
        {
            var errors = new List<string>();

            errors.AddRange(_dateValidator.IsValid(realty.RealtyConstructionDate));
            errors.AddRange(_numberValidator.IsPositive(realty.RealtyMarketValue.ToString()));
            errors.AddRange(_numberValidator.IsPositive(realty.RealtySaleValue.ToString()));

            return errors;
        }
    }
}
