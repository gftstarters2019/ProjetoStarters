using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class ConverterContractHolderToModels
    {
        public Core.Models.IndividualEntity ConvertToIndividual(IndividualDomain domain)
        {
            return new Core.Models.IndividualEntity
            {
                BeneficiaryId = domain.BeneficiaryId,
                IsDeleted = domain.IsDeleted,
                IndividualCPF = domain.IndividualCPF,
                IndividualName = domain.IndividualName,
                IndividualRG = domain.IndividualRG,
                IndividualEmail = domain.IndividualEmail,
                IndividualBirthdate = domain.IndividualBirthdate
            };
        }

        public IEnumerable<Core.Models.AddressEntity> ConvertToListOfAddresses(List<AddressDomain> domainAdresses)
        {
            foreach (var ad in domainAdresses)
            {
                yield return new Core.Models.AddressEntity
                {

                    AddressId = ad.AddressId,
                    AddressCity = ad.AddressCity,
                    AddressComplement = ad.AddressComplement,
                    AddressCountry = ad.AddressCountry,
                    AddressNeighborhood = ad.AddressNeighborhood,
                    AddressNumber = ad.AddressNumber,
                    AddressState = ad.AddressState,
                    AddressStreet = ad.AddressStreet,
                    AddressType = ad.AddressType,
                    AddressZipCode = ad.AddressZipCode
                };
            }
        }

        public IEnumerable<Core.Models.Telephone> ConvertToListOfTelephones(List<TelephoneDomain> domainTelephones)
        {
            foreach (var tel in domainTelephones)
            {
                yield return new Core.Models.Telephone
                {
                    TelephoneId = tel.TelephoneId,
                    TelephoneNumber = tel.TelephoneNumber,
                    TelephoneType = tel.TelephoneType
                };
            }
        }
    }
}
