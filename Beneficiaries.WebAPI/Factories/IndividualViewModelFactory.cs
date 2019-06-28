using Backend.Core.Domains;
using Beneficiaries.WebAPI.Factories.Interfaces;
using Beneficiaries.WebAPI.ViewModels;
using System;

namespace Beneficiaries.WebAPI.Factories
{
    public class IndividualViewModelFactory : IFactory<IndividualViewModel, IndividualDomain>
    {
        public IndividualViewModel Create(IndividualDomain individualDomain)
        {
            if (individualDomain == null)
                return null;

            return new IndividualViewModel()
            {
                IndividualId = individualDomain.BeneficiaryId,
                IndividualBirthdate = individualDomain.IndividualBirthdate,
                IndividualCPF = individualDomain.IndividualCPF,
                IndividualEmail = individualDomain.IndividualEmail,
                IndividualName = individualDomain.IndividualName,
                IndividualRG = individualDomain.IndividualRG
            };
        }
    }
}
