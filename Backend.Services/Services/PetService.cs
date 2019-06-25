using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters;
using Backend.Infrastructure.Repositories.Interfaces;
using Backend.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services.Services
{
    public class PetService : IService<PetDomain>
    {
        private IRepository<PetEntity> _petRepository;

        public PetService(IRepository<PetEntity> petRepository)
        {
            _petRepository = petRepository;
        }

        public PetDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public PetDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PetDomain> GetAll()
        {
            return _petRepository.Get().Select(pet => ConvertersManager.PetConverter.Convert(pet)).ToList();
        }

        public PetDomain Save(PetDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public PetDomain Update(Guid id, PetDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
