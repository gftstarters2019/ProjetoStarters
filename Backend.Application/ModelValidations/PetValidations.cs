using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ModelValidations
{
    public static class PetValidations
    {
        /// <summary>
        /// Verifies if Pet is valid
        /// </summary>
        /// <param name="pet">Pet to be verified</param>
        /// <returns>If Pet is valid</returns>
        public static bool PetIsValid(PetEntity pet)
        {
            if (!ValidationsHelper.DateIsValid(pet.PetBirthdate))
                return false;
            return true;
        }
    }
}
