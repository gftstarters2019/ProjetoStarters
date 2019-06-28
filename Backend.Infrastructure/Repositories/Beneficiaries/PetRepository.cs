using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class PetRepository : IRepository<PetEntity>
    {
        private readonly ConfigurationContext _db;
        private bool disposed = false;

        public PetRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public PetEntity Add(PetEntity pet)
        {
            if (pet != null)
            {
                pet.IsDeleted = false;
                pet.BeneficiaryId = Guid.NewGuid();
                return _db.Pets.Add(pet).Entity;
            }
            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public PetEntity Find(Guid id) => _db.Pets.Where(pet => pet.BeneficiaryId == id).FirstOrDefault();

        public IEnumerable<PetEntity> Get() => _db
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

        public PetEntity Update(Guid id, PetEntity pet)
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
