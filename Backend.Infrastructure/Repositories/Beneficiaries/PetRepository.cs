using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class PetRepository : IReadOnlyRepository<Pet>, IWriteRepository<Pet>
    {
        private readonly ConfigurationContext _db;

        public PetRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(Pet pet)
        {
            if (pet != null)
            {
                _db.Pets.Add(pet);
                if (_db.SaveChanges() == 1)
                    return true;
            }
            return false;
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

        public Pet Update(Guid id, Pet t)
        {
            throw new NotImplementedException();
        }
    }
}
