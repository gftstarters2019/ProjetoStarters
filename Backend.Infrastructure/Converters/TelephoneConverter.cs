using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;

namespace Backend.Infrastructure.Converters
{
    public class TelephoneConverter : IConverter<TelephoneDomain, TelephoneEntity>
    {
        public TelephoneDomain Convert(TelephoneEntity telephoneEntity)
        {
            if (telephoneEntity == null)
                return null;

            var telephoneDomain = new TelephoneDomain()
            {
                TelephoneId = telephoneEntity.TelephoneId,
                TelephoneNumber = telephoneEntity.TelephoneNumber,
                TelephoneType = telephoneEntity.TelephoneType
            };

            return telephoneDomain;
        }

        public TelephoneEntity Convert(TelephoneDomain telephoneDomain)
        {
            if (telephoneDomain == null)
                return null;

            var telephoneEntity = new TelephoneEntity()
            {
                TelephoneId = telephoneDomain.TelephoneId,
                TelephoneNumber = telephoneDomain.TelephoneNumber,
                TelephoneType = telephoneDomain.TelephoneType
            };

            return telephoneEntity;
        }
    }
}
