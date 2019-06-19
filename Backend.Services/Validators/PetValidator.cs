using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class PetValidator : IPetValidator
    {
        private readonly IDateValidator _dateValidator;

        public PetValidator(IDateValidator dateValidator)
        {
            _dateValidator = dateValidator;
        }
        public bool IsValid(Pet pet)
        {
            if (!_dateValidator.IsValid(pet.PetBirthdate))
                return false;
            return true;
        }
    }
}
