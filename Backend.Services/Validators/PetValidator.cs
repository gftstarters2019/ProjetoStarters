using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

namespace Backend.Services.Validators
{
    public class PetValidator : IPetValidator
    {
        private readonly IDateValidator _dateValidator;

        public PetValidator(IDateValidator dateValidator)
        {
            _dateValidator = dateValidator;
        }
        public List<string> IsValid(PetDomain pet)
        {
            var errors = new List<string>();

            errors.AddRange(_dateValidator.IsValid(pet.PetBirthdate));

            return errors;
        }
    }
}
