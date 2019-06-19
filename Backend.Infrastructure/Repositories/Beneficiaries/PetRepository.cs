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
            throw new NotImplementedException();
        }

        public Pet Update(Guid id, Pet t)
        {
            throw new NotImplementedException();
        }
    }
}
