using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;

namespace Backend.Infrastructure.Converters
{
    public class AddressConverter : IConverter<AddressDomain, AddressEntity>
    {
        public AddressDomain Convert(AddressEntity addressEntity)
        {
            if (addressEntity == null)
                return null;

            var addressDomain = new AddressDomain()
            {
                AddressId = addressEntity.AddressId,
                AddressCity = addressEntity.AddressCity,
                AddressComplement = addressEntity.AddressComplement,
                AddressCountry = addressEntity.AddressCountry,
                AddressNeighborhood = addressEntity.AddressNeighborhood,
                AddressNumber = addressEntity.AddressNumber,
                AddressState = addressEntity.AddressState,
                AddressStreet = addressEntity.AddressStreet,
                AddressType = addressEntity.AddressType,
                AddressZipCode = addressEntity.AddressZipCode
            };

            return addressDomain;
        }

        public AddressEntity Convert(AddressDomain addressDomain)
        {
            if (addressDomain == null)
                return null;

            var addressEntity = new AddressEntity()
            {
                AddressId = addressDomain.AddressId,
                AddressCity = addressDomain.AddressCity,
                AddressComplement = addressDomain.AddressComplement,
                AddressCountry = addressDomain.AddressCountry,
                AddressNeighborhood = addressDomain.AddressNeighborhood,
                AddressNumber = addressDomain.AddressNumber,
                AddressState = addressDomain.AddressState,
                AddressStreet = addressDomain.AddressStreet,
                AddressType = addressDomain.AddressType,
                AddressZipCode = addressDomain.AddressZipCode
            };

            return addressEntity;
        }
    }
}
