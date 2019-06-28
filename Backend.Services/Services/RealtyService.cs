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
    public class RealtyService : IService<RealtyDomain>
    {
        private IRepository<RealtyEntity> _realtyRepository;

        public RealtyService(IRepository<RealtyEntity> realtyRepository)
        {
            _realtyRepository = realtyRepository;
        }

        public RealtyDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public RealtyDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<RealtyDomain> GetAll()
        {
            return _realtyRepository.Get().Select(real => ConvertersManager.RealtyConverter.Convert(real)).ToList();
        }

        public RealtyDomain Save(RealtyDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public RealtyDomain Update(Guid id, RealtyDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
