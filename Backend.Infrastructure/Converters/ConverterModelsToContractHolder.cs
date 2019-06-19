using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class ConverterModelsToContractHolder
    {
        public Individual ConvertToIndividual(Core.Models.Individual model)
        {
            return new Individual
            {
                BeneficiaryId = model.BeneficiaryId,
                IsDeleted = model.IsDeleted,
                IndividualCPF = model.IndividualCPF,
                IndividualName = model.IndividualName,
                IndividualRG = model.IndividualRG,
                IndividualEmail = model.IndividualEmail,
                IndividualBirthdate = model.IndividualBirthdate
            };
        }

        public IEnumerable<Address> ConvertToListOfAddresses(List<Core.Models.Address> modelAdresses)
        {
            foreach (var ad in modelAdresses)
            {
                yield return new Address
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

        public IEnumerable<Telephone> ConvertToListOfTelephones(List<Core.Models.Telephone> modelTelephones)
        {
            foreach (var tel in modelTelephones)
            {
                yield return new Telephone
                {
                    TelephoneId = Guid.NewGuid(),
                    TelephoneNumber = tel.TelephoneNumber,
                    TelephoneType = tel.TelephoneType
                };
            }
        }
    }
}
