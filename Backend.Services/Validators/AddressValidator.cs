using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Text.RegularExpressions;

namespace Backend.Services.Validators
{
    public class AddressValidator : IAddressValidator
    {
        public bool IsValid(AddressDomain address)
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
