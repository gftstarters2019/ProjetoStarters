using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;

namespace Backend.Services.Validators
{
    public class PetValidator : IPetValidator
    {
        private readonly IDateValidator _dateValidator;

        public PetValidator(IDateValidator dateValidator)
        {
            _dateValidator = dateValidator;
        }
        public bool IsValid(PetDomain pet)
        {
            if (!_dateValidator.IsValid(pet.PetBirthdate))
                return false;
            return true;
        }
    }
}
