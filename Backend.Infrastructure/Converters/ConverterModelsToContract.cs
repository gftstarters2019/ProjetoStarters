using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    class ConverterModelsToContract
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

        public Address ConvertToAddress(Core.Models.Address model)
        {
            return new Address
            {
                AddressId = model.AddressId,
                AddressCity = model.AddressCity,
                AddressComplement = model.AddressComplement,
                AddressCountry = model.AddressCountry,
                AddressNeighborhood = model.AddressNeighborhood,
                AddressNumber = model.AddressNumber,
                AddressState = model.AddressState,
                AddressStreet = model.AddressStreet,
                AddressType = model.AddressType,
                AddressZipCode = model.AddressZipCode
            };
        }

        public Contract ConvertToContract(Core.Models.Contract model)
        {
            return new Contract
            {
                ContractCategory = model.ContractCategory,
                ContractDeleted = model.ContractDeleted,
                ContractExpiryDate = model.ContractExpiryDate,
                ContractId = model.ContractId,
                ContractType = model.ContractType
            };
        }

        public SignedContract ConvertToSignedContract(Core.Models.SignedContract model)
        {
            return new SignedContract
            {
                ContractId = model.ContractId,
                ContractIndividualIsActive = model.ContractIndividualIsActive,
                IndividualId = model.IndividualId,
                SignedContractContract = ConvertToContract(model.SignedContractContract),
                SignedContractId = model.SignedContractId,
                SignedContractIndividual = ConvertToIndividual(model.SignedContractIndividual)
            };
        }

        public IEnumerable<Individual> ConvertToListOfIndividuals(List<Core.Models.Individual> modelIndividuals)
        {
            foreach (var ind in modelIndividuals)
            {
                yield return new Individual
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

        public IEnumerable<Pet> ConvertToListOfPets(List<Core.Models.Pet> modelPets)
        {
            foreach (var pet in modelPets)
            {
                yield return new Pet
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

        public IEnumerable<MobileDevice> ConvertToListOfMobileDevices(List<Core.Models.MobileDevice> modelMobileDevices)
        {
            foreach (var mb in modelMobileDevices)
            {
                yield return new MobileDevice
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

        public IEnumerable<Realty> ConvertToListOfRealties(List<Core.Models.Realty> modelRealties)
        {
            foreach (var real in modelRealties)
            {
                yield return new Realty
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

        public IEnumerable<Vehicle> ConvertToListOfVehicles(List<Core.Models.Vehicle> modelVehicles)
        {
            foreach (var ve in modelVehicles)
            {
                yield return new Vehicle
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
