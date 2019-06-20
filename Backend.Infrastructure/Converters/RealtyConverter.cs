using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;

namespace Backend.Infrastructure.Converters
{
    public class RealtyConverter : IConverter<RealtyDomain, RealtyEntity>
    {
        public RealtyDomain Convert(RealtyEntity realtyEntity)
        {
            if (realtyEntity == null)
                return null;

            var realtyDomain = new RealtyDomain()
            {
                BeneficiaryId = realtyEntity.BeneficiaryId,
                IsDeleted = realtyEntity.IsDeleted,
                RealtyConstructionDate = realtyEntity.RealtyConstructionDate,
                RealtyMarketValue = realtyEntity.RealtyMarketValue,
                RealtyMunicipalRegistration = realtyEntity.RealtyMunicipalRegistration,
                RealtySaleValue = realtyEntity.RealtySaleValue,
                Address = ConvertersManager.AddressConverter.Convert(realtyEntity.Address)
            };

            return realtyDomain;
        }

        public RealtyEntity Convert(RealtyDomain realtyDomain)
        {
            if (realtyDomain == null)
                return null;

            var realtyEntity = new RealtyEntity()
            {
                BeneficiaryId = realtyDomain.BeneficiaryId,
                IsDeleted = realtyDomain.IsDeleted,
                RealtyConstructionDate = realtyDomain.RealtyConstructionDate,
                RealtyMarketValue = realtyDomain.RealtyMarketValue,
                RealtyMunicipalRegistration = realtyDomain.RealtyMunicipalRegistration,
                RealtySaleValue = realtyDomain.RealtySaleValue,
                Address = ConvertersManager.AddressConverter.Convert(realtyDomain.Address)
            };

            return realtyEntity;
        }
    }
}
