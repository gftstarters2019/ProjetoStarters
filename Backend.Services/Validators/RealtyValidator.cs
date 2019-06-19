using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
        public bool IsValid(Realty realty)
        {
            if (!_dateValidator.IsValid(realty.RealtyConstructionDate))
                return false;
            if (!_numberValidator.IsPositive(realty.RealtyMarketValue.ToString()) && !_numberValidator.IsPositive(realty.RealtySaleValue.ToString()))
                return false;
            return true;
        }
    }
}
