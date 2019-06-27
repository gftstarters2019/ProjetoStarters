using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;
using System;

namespace Backend.Infrastructure.Converters
{
    public class ContractConverter : IConverter<ContractDomain, ContractEntity>
    {
        public ContractDomain Convert(ContractEntity contractEntity)
        {
            if (contractEntity == null)
                return null;

            var contractDomain = new ContractDomain()
            {
                ContractId = contractEntity.ContractId,
                ContractType = contractEntity.ContractType,
                ContractCategory = contractEntity.ContractCategory,
                ContractExpiryDate = contractEntity.ContractExpiryDate,
                ContractDeleted = contractEntity.ContractDeleted
            };

            return contractDomain;
        }

        public ContractEntity Convert(ContractDomain contractDomain)
        {
            if (contractDomain == null)
                return null;

            var contractEntity = new ContractEntity()
            {
                ContractId = contractDomain.ContractId,
                ContractCategory = contractDomain.ContractCategory,
                ContractDeleted = contractDomain.ContractDeleted,
                ContractExpiryDate = contractDomain.ContractExpiryDate,
                ContractType = contractDomain.ContractType
            };

            return contractEntity;
        }
    }
}
