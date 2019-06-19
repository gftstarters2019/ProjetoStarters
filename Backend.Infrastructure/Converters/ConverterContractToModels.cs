using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class ConverterContractToModels
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

        public Core.Models.AddressEntity ConvertToAddress(AddressDomain domain)
        {
            return new Core.Models.AddressEntity
            {
                AddressId = domain.AddressId,
                AddressCity = domain.AddressCity,
                AddressComplement = domain.AddressComplement,
                AddressCountry = domain.AddressCountry,
                AddressNeighborhood = domain.AddressNeighborhood,
                AddressNumber = domain.AddressNumber,
                AddressState = domain.AddressState,
                AddressStreet = domain.AddressStreet,
                AddressType = domain.AddressType,
                AddressZipCode = domain.AddressZipCode
            };
        }

        public Core.Models.ContractEntity ConvertToContract(ContractDomain domain)
        {
            return new Core.Models.ContractEntity
            {
                ContractCategory = domain.ContractCategory,
                ContractDeleted = domain.ContractDeleted,
                ContractExpiryDate = domain.ContractExpiryDate,
                ContractId = domain.ContractId,
                ContractType = domain.ContractType
            };
        }

        public Core.Models.SignedContractEntity ConvertToSignedContract(SignedContractDomain domain)
        {
            return new Core.Models.SignedContractEntity
            {
                ContractId = domain.ContractId,
                ContractIndividualIsActive = domain.ContractIndividualIsActive,
                IndividualId = domain.IndividualId,
                SignedContractContract = ConvertToContract(domain.SignedContractContract),
                SignedContractId = domain.SignedContractId,
                SignedContractIndividual = ConvertToIndividual(domain.SignedContractIndividual)
            };
        }

        public IEnumerable<Core.Models.IndividualEntity> ConvertToListOfIndividuals(List<IndividualDomain> domainIndividuals)
        {
            foreach (var ind in domainIndividuals)
            {
                yield return new Core.Models.IndividualEntity
                {

                    BeneficiaryId = ind.BeneficiaryId,
                    IsDeleted = ind.IsDeleted,
                    IndividualCPF = ind.IndividualCPF,
                    IndividualName = ind.IndividualName,
                    IndividualRG = ind.IndividualRG,
                    IndividualEmail = ind.IndividualEmail,
                    IndividualBirthdate = ind.IndividualBirthdate
                };
            }
        }

        public IEnumerable<Core.Models.PetEntity> ConvertToListOfPets(List<PetDomain> domainPets)
        {
            foreach (var pet in domainPets)
            {
                yield return new Core.Models.PetEntity
                {
                    BeneficiaryId = pet.BeneficiaryId,
                    IsDeleted = pet.IsDeleted,
                    PetBirthdate = pet.PetBirthdate,
                    PetBreed = pet.PetBreed,
                    PetName = pet.PetName,
                    PetSpecies = pet.PetSpecies
                };
            }
        }

        public IEnumerable<Core.Models.MobileDeviceEntity> ConvertToListOfMobileDevices(List<MobileDeviceDomain> domainMobileDevices)
        {
            foreach (var mb in domainMobileDevices)
            {
                yield return new Core.Models.MobileDeviceEntity
                {
                    BeneficiaryId = mb.BeneficiaryId,
                    IsDeleted = mb.IsDeleted,
                    MobileDeviceBrand = mb.MobileDeviceBrand,
                    MobileDeviceInvoiceValue = mb.MobileDeviceInvoiceValue,
                    MobileDeviceManufactoringYear = mb.MobileDeviceManufactoringYear,
                    MobileDeviceModel = mb.MobileDeviceModel,
                    MobileDeviceSerialNumber = mb.MobileDeviceSerialNumber,
                    MobileDeviceType = mb.MobileDeviceType
                };
            }
        }

        public IEnumerable<Core.Models.RealtyEntity> ConvertToListOfRealties(List<RealtyDomain> domainRealties)
        {
            foreach (var real in domainRealties)
            {
                yield return new Core.Models.RealtyEntity
                {
                    BeneficiaryId = real.BeneficiaryId,
                    IsDeleted = real.IsDeleted,
                    Address = ConvertToAddress(real.Address),
                    RealtyConstructionDate = real.RealtyConstructionDate,
                    RealtyMarketValue = real.RealtyMarketValue,
                    RealtyMunicipalRegistration = real.RealtyMunicipalRegistration,
                    RealtySaleValue = real.RealtySaleValue
                };
            }
        }

        public IEnumerable<Core.Models.VehicleEntity> ConvertToListOfVehicles(List<VehicleDomain> domainVehicles)
        {
            foreach (var ve in domainVehicles)
            {
                yield return new Core.Models.VehicleEntity
                {
                    BeneficiaryId = ve.BeneficiaryId,
                    IsDeleted = ve.IsDeleted,
                    VehicleBrand = ve.VehicleBrand,
                    VehicleChassisNumber = ve.VehicleChassisNumber,
                    VehicleColor = ve.VehicleColor,
                    VehicleCurrentFipeValue = ve.VehicleCurrentFipeValue,
                    VehicleCurrentMileage = ve.VehicleCurrentMileage,
                    VehicleDoneInspection = ve.VehicleDoneInspection,
                    VehicleManufactoringYear = ve.VehicleManufactoringYear,
                    VehicleModel = ve.VehicleModel,
                    VehicleModelYear = ve.VehicleModelYear
                };
            }
        }
    }
}
