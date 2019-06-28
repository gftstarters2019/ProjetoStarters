using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Services.Validators
{
    public class AddressValidator : IAddressValidator
    {
        public List<string> IsValid(AddressDomain address)
        {
            Regex regexLetters = new Regex("^[a-zA-Z-ãâáõóôçÇ]+(([',. -][a-zA-Z-ãâáõóôçÇ])?[a-zA-Z-ãâáõóôçÇ]*)*$");
            var errors = new List<string>();

            if (!new Regex("^[0-9]+$").IsMatch(address.AddressNumber) || address.AddressNumber.Length > 7)
                errors.Add($"{address.AddressNumber}: Numero do imóvel incorreto! ");

            if (!new Regex("^\\d{5}(?:[-\\s]\\d{3})?$").IsMatch(address.AddressZipCode))
                errors.Add($"{address.AddressZipCode}:Zip inválido! ");

            if (address.AddressComplement != null)
                if (!regexLetters.IsMatch(address.AddressComplement))
                    errors.Add($"{address.AddressComplement}: Complemento Inválido! ");

            if (!regexLetters.IsMatch(address.AddressNeighborhood))
                errors.Add($"{address.AddressNeighborhood}: Bairro Inválido! ");

            if (!regexLetters.IsMatch(address.AddressCity))
                errors.Add($"{address.AddressCity}: Cidade Inválida! ");

            if (!new Regex("^[[A-Z]+$").IsMatch(address.AddressState) || address.AddressState.Length != 2)
                errors.Add($"{address.AddressState}: Estado Inválido! ");

            if (!regexLetters.IsMatch(address.AddressCountry))
                errors.Add($"{address.AddressCountry}: País Inválido! ");

            return errors;
        }
    }
}
