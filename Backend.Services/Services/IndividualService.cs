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
    public class IndividualService : IService<IndividualDomain>
    {
        private IRepository<IndividualEntity> _individualRepository;

        public IndividualService(IRepository<IndividualEntity> individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public IndividualDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IndividualDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<IndividualDomain> GetAll()
        {
            return _individualRepository.Get().Select(ind => ConvertersManager.IndividualConverter.Convert(ind)).ToList();
        }

        public IndividualDomain Save(IndividualDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public IndividualDomain Update(Guid id, IndividualDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
