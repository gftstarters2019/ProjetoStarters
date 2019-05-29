using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class AddressRepository : IReadOnlyRepository<Address>, IWriteRepository<Address>
    {
        private readonly ConfigurationContext _db;

        public AddressRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Address Find(Guid id) => _db
            .Addresses
            .FirstOrDefault(ad => ad.AddressId == id);

        public IEnumerable<Address> Get() => _db
            .Addresses
            .ToList();

        public void Add(Address address)
        {
            if (address != null)
            {
                _db.Add(address);
                _db.SaveChanges();
            }
        }

        public Address Remove(Address address)
        {
            if (address != null)
            {
                _db.Remove(address);
                _db.SaveChanges();
            }

            return address;
        }

        public Address Update(Address address)
        {
            if (address != null)
            {
                _db.Update(address);
                _db.SaveChanges();
            }

            return address;
        }
    }
}
