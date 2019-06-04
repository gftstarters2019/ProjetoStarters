using Backend.Application.Interfaces;
using Backend.Core.Enums;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class PetFactory : IFactory<Pet>
    {
        public Pet Create(Guid petId, string petName, PetSpecies petSpecies, string petBreed, DateTime PetBirthdate)
        {
            var pet = new Pet();
            pet.PetId = Guid.NewGuid();
            pet.PetName = petName;
            pet.PetSpecies = petSpecies;
            pet.PetBreed = petBreed;
            pet.PetBirthdate = PetBirthdate;

            return pet;

        }
        public Pet Create()
        {
           return new Pet();
        }
    }
}