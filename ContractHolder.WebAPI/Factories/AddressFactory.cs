using Backend.Core.Models;
using ContractHolder.WebAPI.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractHolder.WebAPI.Factories
{
    public class AddressFactory : IFactoryList<AddressEntity>
    {
        private List<AddressEntity> addresses = null;
        private AddressEntity address = null;

        public AddressFactory()
        {
            addresses = new List<AddressEntity>();
        }

        public List<AddressEntity> CreateList(List<AddressEntity> vm_addresses)
        {
            addresses = new List<AddressEntity>();
            foreach (var ad in vm_addresses)
            {
                address = new AddressEntity();

                address.AddressId = Guid.NewGuid();
                address.AddressCity = ad.AddressCity;
                address.AddressComplement = ad.AddressComplement;
                address.AddressCountry = ad.AddressCountry;
                address.AddressNeighborhood = ad.AddressNeighborhood;
                address.AddressNumber = ad.AddressNumber;
                address.AddressState = ad.AddressState;
                address.AddressStreet = ad.AddressStreet;
                address.AddressType = ad.AddressType;
                address.AddressZipCode = ad.AddressZipCode;

                addresses.Add(address);

            }

            if (addresses.Count != vm_addresses.Count || addresses.Count > 3)
            {
                return null;
            }

            return addresses;
        }
    }
}
