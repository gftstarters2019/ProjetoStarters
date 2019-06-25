using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Validators
{
    public class ContractHolderValidator : IContractHolderValidator
    {
        private readonly IIndividualValidator _individualValidator;
        private readonly IDateValidator _dateValidator;
        private readonly IAddressValidator _addressValidator;
        private readonly ITelephoneValidator _telephoneValidator;

        public ContractHolderValidator(IIndividualValidator individualValidator, IAddressValidator addressValidator, ITelephoneValidator telephoneValidator, IDateValidator dateValidator)
        {
            _individualValidator = individualValidator;
            _addressValidator = addressValidator;
            _telephoneValidator = telephoneValidator;
            _dateValidator = dateValidator;
        }

        public List<string> IsValid(IndividualDomain individual, List<AddressDomain> addresses, List<TelephoneDomain> telephones)
        {
            List<string> errors = new List<string>();

            errors = _individualValidator.IsValid(individual);

            //if (!_dateValidator.IsOfAge(individual.IndividualBirthdate))
            //    return false;

            if (addresses != null)
            {
                foreach (var item in addresses)
                {
                    errors.Concat(_addressValidator.IsValid(item));
                }
            }
            //if (telephones != null)
            //{
            //    foreach (var item in telephones)
            //    {
            //        if (!_telephoneValidator.IsValid(item))
            //            return false;
            //    }
            //}
            return errors;            
        }
    }
}
