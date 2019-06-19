using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class ContractHolderValidator : IContractHolderValidator
    {
        private readonly IIndividualValidator _individualValidator;
        private readonly IAddressValidator _addressValidator;
        private readonly ITelephoneValidator _telephoneValidator;

        public ContractHolderValidator(IIndividualValidator individualValidator, IAddressValidator addressValidator, ITelephoneValidator telephoneValidator)
        {
            _individualValidator = individualValidator;
            _addressValidator = addressValidator;
            _telephoneValidator = telephoneValidator;
        }

        public bool IsValid(Individual individual, Address address, Telephone telephone)
        {
            if (!_individualValidator.IsValid(individual))
                return false;
            if (!_addressValidator.IsValid(address))
                return false;
            if (!_telephoneValidator.IsValid(telephone))
                return false;
            return true;
        }

    }
}
