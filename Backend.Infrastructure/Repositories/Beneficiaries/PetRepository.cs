using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly ConfigurationContext _db;

        public PetRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Pet Add(Pet pet)
        {
            if (pet != null)
            {
                pet.IsDeleted = false;
                pet.BeneficiaryId = Guid.NewGuid();
                return _db.Pets.Add(pet).Entity;
            }
            return null;
        }

        public Pet Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> Get() => _db
            .Pets
            .Where(i => !i.IsDeleted)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public Pet Update(Guid id, Pet pet)
        {
            if(pet != null)
            {
                var petToUpdate = Find(id);
                if(petToUpdate != null)
                {
                    petToUpdate.IsDeleted = pet.IsDeleted;
                    petToUpdate.PetBirthdate = pet.PetBirthdate;
                    petToUpdate.PetBreed = pet.PetBreed;
                    petToUpdate.PetName = pet.PetName;
                    petToUpdate.PetSpecies = pet.PetSpecies;

                    return _db.Pets.Update(petToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
