using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class IndividualConverter : IConverter<IndividualDomain, IndividualEntity>
    {
        public IndividualDomain Convert(IndividualEntity individualEntity)
        {
            if (individualEntity == null)
                return null;

            var individualDomain = new IndividualDomain()
            {
                BeneficiaryId = individualEntity.BeneficiaryId,
                IndividualBirthdate = individualEntity.IndividualBirthdate,
                IndividualCPF = individualEntity.IndividualCPF,
                IndividualEmail = individualEntity.IndividualEmail,
                IndividualName = individualEntity.IndividualName,
                IndividualRG = individualEntity.IndividualRG,
                IsDeleted = individualEntity.IsDeleted
            };

            return individualDomain;
        }

        public IndividualEntity Convert(IndividualDomain individualDomain)
        {
            if (individualDomain == null)
                return null;

            var individualEntity = new IndividualEntity()
            {
                BeneficiaryId = individualDomain.BeneficiaryId,
                IndividualBirthdate = individualDomain.IndividualBirthdate,
                IndividualCPF = individualDomain.IndividualCPF,
                IndividualEmail = individualDomain.IndividualEmail,
                IndividualName = individualDomain.IndividualName,
                IndividualRG = individualDomain.IndividualRG,
                IsDeleted = individualDomain.IsDeleted
            };

            return individualEntity;
        }
    }
}
