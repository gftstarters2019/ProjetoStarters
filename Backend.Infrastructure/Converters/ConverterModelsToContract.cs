using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Converters
{
    public class ConverterModelsToContract
    {
        public IndividualDomain ConvertToIndividual(Core.Models.IndividualEntity model)
        {
            return new IndividualDomain
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

        public AddressDomain ConvertToAddress(Core.Models.AddressEntity model)
        {
            return new AddressDomain
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

        public ContractDomain ConvertToContract(Core.Models.ContractEntity model)
        {
            return new ContractDomain
            {
                ContractCategory = model.ContractCategory,
                ContractDeleted = model.ContractDeleted,
                ContractExpiryDate = model.ContractExpiryDate,
                ContractId = model.ContractId,
                ContractType = model.ContractType
            };
        }

        public SignedContractDomain ConvertToSignedContract(Core.Models.SignedContractEntity model)
        {
            return new SignedContractDomain
            {
                ContractId = model.ContractId,
                ContractIndividualIsActive = model.ContractIndividualIsActive,
                IndividualId = model.IndividualId,
                SignedContractContract = ConvertToContract(model.SignedContractContract),
                SignedContractId = model.SignedContractId,
                SignedContractIndividual = ConvertToIndividual(model.SignedContractIndividual)
            };
        }

        public IEnumerable<IndividualDomain> ConvertToListOfIndividuals(List<Core.Models.IndividualEntity> modelIndividuals)
        {
            foreach (var ind in modelIndividuals)
            {
                yield return new IndividualDomain
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

        public IEnumerable<PetDomain> ConvertToListOfPets(List<Core.Models.PetEntity> modelPets)
        {
            foreach (var pet in modelPets)
            {
                yield return new PetDomain
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

        public IEnumerable<MobileDeviceDomain> ConvertToListOfMobileDevices(List<Core.Models.MobileDeviceEntity> modelMobileDevices)
        {
            foreach (var mb in modelMobileDevices)
            {
                yield return new MobileDeviceDomain
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

        public IEnumerable<RealtyDomain> ConvertToListOfRealties(List<Core.Models.RealtyEntity> modelRealties)
        {
            foreach (var real in modelRealties)
            {
                yield return new RealtyDomain
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

        public IEnumerable<VehicleDomain> ConvertToListOfVehicles(List<Core.Models.VehicleEntity> modelVehicles)
        {
            foreach (var ve in modelVehicles)
            {
                yield return new VehicleDomain
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
