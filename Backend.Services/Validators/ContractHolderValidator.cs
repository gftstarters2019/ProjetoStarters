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

            errors.AddRange(_individualValidator.IsValid(individual));

            errors.AddRange(_dateValidator.IsOfAge(individual.IndividualBirthdate));

            if (addresses != null)
            {
                foreach (var item in addresses)
                {
                    errors.AddRange(_addressValidator.IsValid(item));
                }
            }
            if (telephones != null)
            {
                foreach (var item in telephones)
                {
                    errors.AddRange(_telephoneValidator.IsValid(item));
                }
            }
            return errors;            
        }
    }
}
