using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class AddressRepository : IRepository<Address>
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

        public Address Add(Address address)
        {
            if (address != null)
            {
                address.AddressId = Guid.NewGuid();
                return _db.Add(address).Entity;
            }
            return null;
        }

        public bool Remove(Guid id)
        {
            var address = Find(id);
            if (address != null)
            {
                _db.Remove(address);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public Address Update(Guid id, Address address)
        {
            if (address != null)
            {
                var addressToUpdate = Find(id);
                if(addressToUpdate != null)
                {
                    addressToUpdate.AddressCity = address.AddressCity;
                    addressToUpdate.AddressComplement = address.AddressComplement;
                    addressToUpdate.AddressCountry = address.AddressCountry;
                    addressToUpdate.AddressNeighborhood = address.AddressNeighborhood;
                    addressToUpdate.AddressNumber = address.AddressNumber;
                    addressToUpdate.AddressState = address.AddressState;
                    addressToUpdate.AddressStreet = address.AddressStreet;
                    addressToUpdate.AddressType = address.AddressType;
                    addressToUpdate.AddressZipCode = address.AddressZipCode;

                    return _db.Addresses.Update(addressToUpdate).Entity;
                }
            }
            return null;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
