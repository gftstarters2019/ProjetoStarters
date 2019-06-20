using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;

namespace Backend.Infrastructure.Converters
{
    public class SignedContractConverter : IConverter<SignedContractDomain, SignedContractEntity>
    {
        public SignedContractDomain Convert(SignedContractEntity signedContractEntity)
        {
            if (signedContractEntity == null)
                return null;

            var signedContractDomain = new SignedContractDomain()
            {
                SignedContractId = signedContractEntity.SignedContractId,
                ContractId = signedContractEntity.ContractId,
                IndividualId = signedContractEntity.IndividualId,
                ContractIndividualIsActive = signedContractEntity.ContractIndividualIsActive,
                SignedContractContract = ConvertersManager.ContractConverter.Convert(
                    signedContractEntity.SignedContractContract),
                SignedContractIndividual = ConvertersManager.IndividualConverter.Convert(
                    signedContractEntity.SignedContractIndividual)
            };

            return signedContractDomain;
        }

        public SignedContractEntity Convert(SignedContractDomain signedContractDomain)
        {
            if (signedContractDomain == null)
                return null;

            var signedContractEntity = new SignedContractEntity()
            {
                SignedContractId = signedContractDomain.SignedContractId,
                ContractId = signedContractDomain.ContractId,
                IndividualId = signedContractDomain.IndividualId,
                ContractIndividualIsActive = signedContractDomain.ContractIndividualIsActive,
                SignedContractContract = ConvertersManager.ContractConverter.Convert(
                    signedContractDomain.SignedContractContract),
                SignedContractIndividual = ConvertersManager.IndividualConverter.Convert(
                    signedContractDomain.SignedContractIndividual)
            };

            return signedContractEntity;
        }
    }
}