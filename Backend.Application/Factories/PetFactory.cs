using Backend.Application.Factories.Interfaces;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories
{
    public class PetFactory : IFactoryList<Pet>
    {
        private List<Address> addresses = null;

        public List<Pet> CreateList(List<Pet> pets)
        {
            foreach (var pet in pets)
            {
                if (PetIsValid(pet))
                {

                }
            }
                throw new NotImplementedException();
        }

        public Pet Create(ContractViewModel vm)
        {
            //if(PetIsValid())
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies if Pet is valid
        /// </summary>
        /// <param name="pet">Pet to be verified</param>
        /// <returns>If Pet is valid</returns>
        public static bool PetIsValid(Pet pet)
        {
            if (!DateIsValid(pet.PetBirthdate))
                return false;
            return true;
        }

        /// <summary>
        /// Verifies if date is not future date
        /// </summary>
        /// <returns>If date is valid</returns>
        public static bool DateIsValid(DateTime date)
        {
            return date != null ? date > DateTime.Today : false;
        }
    }
}
