using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Application.ModelValidations
{
    public static class AddressValidations
    {
        public static bool AddressIsValid(Address address)
        {
            Regex regexLetters = new Regex("^[a-zA-Z]+$");

            if (!new Regex("^[0-9]+$").IsMatch(address.AddressNumber))
                return false;

            if (!new Regex("^\\d{5}(?:[-\\s]\\d{4})?$").IsMatch(address.AddressZipCode))
                return false;

            if (!regexLetters.IsMatch(address.AddressStreet) || !regexLetters.IsMatch(address.AddressComplement) || !regexLetters.IsMatch(address.AddressNeighborhood) ||
                    !regexLetters.IsMatch(address.AddressCity) || !regexLetters.IsMatch(address.AddressState) || !regexLetters.IsMatch(address.AddressCountry))
                return false;

            return true;
        }
    }
}
