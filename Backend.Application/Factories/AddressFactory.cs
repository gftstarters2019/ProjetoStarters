using Backend.Application.Factories.Interfaces;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Application.Factories
{
    public class AddressFactory : IFactoryList<Address>
    {
        private List<Address> addresses = null;
        private Address address = null;

        public AddressFactory()
        {
            addresses = new List<Address>();
        }

        public List<Address> CreateList(List<Address> vm_addresses)
        {
            foreach (var ad in vm_addresses)
            {
                if (Validate(ad))
                {
                    address = new Address();

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
            }

            if (addresses.Count != vm_addresses.Count || addresses.Count > 3)
            {
                addresses.Clear();
                return addresses;
            }

            return addresses;
        }

        /// <summary>
        /// Validações de Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool Validate(Address address)
        {
            Regex regexLetters = new Regex("^[a-zA-Z-ã]+\\s?[a-zA-Z-ã]+\\s?\\w+$");
            Regex regexState = new Regex("^[[a-zA-Z]+$");

            if (!new Regex("^[0-9]+$").IsMatch(address.AddressNumber))
                return false;

            if (!new Regex("^\\d+$").IsMatch(address.AddressZipCode) || address.AddressZipCode.Length != 8)
                return false;

            if (!regexState.IsMatch(address.AddressState) || address.AddressState.Length != 2)
                return false;

            if (!regexLetters.IsMatch(address.AddressStreet) || !regexLetters.IsMatch(address.AddressComplement) || !regexLetters.IsMatch(address.AddressNeighborhood) ||
                    !regexLetters.IsMatch(address.AddressCity) || !regexLetters.IsMatch(address.AddressCountry))
                return false;

            return true;
        }
    }
}
