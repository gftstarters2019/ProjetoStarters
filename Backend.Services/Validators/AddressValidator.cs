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
            Regex regexLetters = new Regex("^[a-zA-Z]+$");
            var errors = new List<string>();

            if (!new Regex("^[0-9]+$").IsMatch(address.AddressNumber))
                errors.Add($"{address.AddressNumber}: Numero do imóvel incorreto! ");

            if (!new Regex("^\\d{5}(?:[-\\s]\\d{4})?$").IsMatch(address.AddressZipCode))
                errors.Add($"{address.AddressZipCode}:Zip inválido! ");

            if (!regexLetters.IsMatch(address.AddressStreet))
                errors.Add($"{address.AddressStreet}: Rua Inválida! ");

            if (!regexLetters.IsMatch(address.AddressComplement))
                errors.Add($"{address.AddressComplement}: Complemento Inválido! ");

            if (!regexLetters.IsMatch(address.AddressNeighborhood))
                errors.Add($"{address.AddressNeighborhood}: Bairro Inválido! ");

            if (!regexLetters.IsMatch(address.AddressCity))
                errors.Add($"{address.AddressCity}: Cidade Inválida! ");

            if (!regexLetters.IsMatch(address.AddressState))
                errors.Add($"{address.AddressState}: Estado Inválido! ");

            if (!regexLetters.IsMatch(address.AddressCountry))
                errors.Add($"{address.AddressCountry}: País Inválido! ");

            return errors;
        }
    }
}
