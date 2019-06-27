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
        private readonly IDateValidator _dateValidator;
        private readonly IAddressValidator _addressValidator;
        private readonly ITelephoneValidator _telephoneValidator;

        public ContractHolderValidator(IIndividualValidator individualValidator, IAddressValidator addressValidator, ITelephoneValidator telephoneValidator)
        {
            _individualValidator = individualValidator;
            _addressValidator = addressValidator;
            _telephoneValidator = telephoneValidator;
        }

        public bool IsValid(Individual individual, List<Address> addresses, List<Telephone> telephones)
        {
            if (!_individualValidator.IsValid(individual))
                return false;
            if (!_dateValidator.IsOfAge(individual.IndividualBirthdate))
                return false;
            if (addresses != null)
            {
                foreach (var item in addresses)
                {
                    if (!_addressValidator.IsValid(item))
                        return false;
                }
            }
            if (telephones != null)
            {
                foreach (var item in telephones)
                {
                    if (!_telephoneValidator.IsValid(item))
                        return false;
                }
            }
            return true;
        }

    }
}
