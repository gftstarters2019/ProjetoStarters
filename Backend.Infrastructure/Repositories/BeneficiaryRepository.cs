using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class BeneficiaryRepository : IReadOnlyRepository<Address>, IWriteRepository<Address>
    {
        public BeneficiaryRepository()
        {

        }

        public Address Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> Get()
        {
            throw new NotImplementedException();
        }

        public void Add(Address individual)
        {

        }

        public Address Remove(Address t)
        {
            throw new NotImplementedException();
        }

        public Address Update(Address t)
        {
            throw new NotImplementedException();
        }
    }
}
