using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class ConverterContractHolderToModels
    {
        public Core.Models.Individual ConvertToIndividual(Individual domain)
        {
            return new Core.Models.Individual
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

        public IEnumerable<Core.Models.Address> ConvertToListOfAddresses(List<Address> domainAdresses)
        {
            foreach (var ad in domainAdresses)
            {
                yield return new Core.Models.Address
                {

                    AddressId = Guid.NewGuid(),
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

        public IEnumerable<Core.Models.Telephone> ConvertToListOfTelephones(List<Telephone> domainTelephones)
        {
            foreach (var tel in domainTelephones)
            {
                yield return new Core.Models.Telephone
                {
                    TelephoneId = Guid.NewGuid(),
                    TelephoneNumber = tel.TelephoneNumber,
                    TelephoneType = tel.TelephoneType
                };
            }
        }
    }
}
