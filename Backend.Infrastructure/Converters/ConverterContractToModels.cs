using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    class ConverterContractToModels
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

        public Core.Models.Address ConvertToAddress(Address domain)
        {
            return new Core.Models.Address
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

        public Core.Models.Contract ConvertToContract(Contract domain)
        {
            return new Core.Models.Contract
            {
                ContractCategory = domain.ContractCategory,
                ContractDeleted = domain.ContractDeleted,
                ContractExpiryDate = domain.ContractExpiryDate,
                ContractId = domain.ContractId,
                ContractType = domain.ContractType
            };
        }

        public Core.Models.SignedContract ConvertToSignedContract(SignedContract domain)
        {
            return new Core.Models.SignedContract
            {
                ContractId = domain.ContractId,
                ContractIndividualIsActive = domain.ContractIndividualIsActive,
                IndividualId = domain.IndividualId,
                SignedContractContract = ConvertToContract(domain.SignedContractContract),
                SignedContractId = domain.SignedContractId,
                SignedContractIndividual = ConvertToIndividual(domain.SignedContractIndividual)
            };
        }

        public IEnumerable<Core.Models.Individual> ConvertToListOfIndividuals(List<Individual> domainIndividuals)
        {
            foreach (var ind in domainIndividuals)
            {
                yield return new Core.Models.Individual
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

        public IEnumerable<Core.Models.Pet> ConvertToListOfPets(List<Pet> domainPets)
        {
            foreach (var pet in domainPets)
            {
                yield return new Core.Models.Pet
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

        public IEnumerable<Core.Models.MobileDevice> ConvertToListOfMobileDevices(List<MobileDevice> domainMobileDevices)
        {
            foreach (var mb in domainMobileDevices)
            {
                yield return new Core.Models.MobileDevice
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

        public IEnumerable<Core.Models.Realty> ConvertToListOfRealties(List<Realty> domainRealties)
        {
            foreach (var real in domainRealties)
            {
                yield return new Core.Models.Realty
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

        public IEnumerable<Core.Models.Vehicle> ConvertToListOfVehicles(List<Vehicle> domainVehicles)
        {
            foreach (var ve in domainVehicles)
            {
                yield return new Core.Models.Vehicle
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
