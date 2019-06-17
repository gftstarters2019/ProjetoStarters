using Backend.Core.Enums;
using Backend.Core.Models;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class BeneficiaryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenCreateAnIndividual_ThenVerifyIfHeIsABeneficiary()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            //beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            
            //act
            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.SignedContractId;
            contractBeneficiary.BeneficiaryId = beneficiary.BeneficiaryId;
            contractBeneficiary.SignedContract = signedContract;
            contractBeneficiary.Beneficiary = beneficiary;


            //assert
            Assert.AreEqual(beneficiary.BeneficiaryId, contractBeneficiary.BeneficiaryId);
            Assert.AreEqual(beneficiary, contractBeneficiary.Beneficiary);

        }

        [Test]
        public void WhenCreateAnIndividual_AndCreateAContractHolder_ThenVerifyIfHeIsAlsoABeneficiary()
        {
            //arange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            //act
            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.SignedContractId;
            contractBeneficiary.BeneficiaryId = individual.BeneficiaryId;
            contractBeneficiary.SignedContract = signedContract;
            contractBeneficiary.Beneficiary = individual;

            //assert
            Assert.AreEqual(individual.BeneficiaryId, contractBeneficiary.BeneficiaryId);
            Assert.AreEqual(individual, contractBeneficiary.Beneficiary);
        }

        [Test]
        public void WhenCreateABeneficiary_ThenVerifyIfICanReadHisProperties()
        {
            //arrange
            var beneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            var individualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            var individualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            var individualCPF = "45301152897";
            var individualRG = "458559462";
            var individualEmail = "gftstarters2019@outlook.com";
            var individualDeleted = false;

            var newAddress = new Address();
            newAddress.AddressId = new Guid("029ec5eb-a126-45e4-981c-2db4e380c9eb");
            newAddress.AddressStreet = "Rua Sales Fonseca";
            newAddress.AddressNumber = "50";
            newAddress.AddressComplement = "N/A";
            newAddress.AddressNeighborhood = "Jardim Santo Amaro";
            newAddress.AddressCity = "Sorocaba";
            newAddress.AddressState = "São Paulo";
            newAddress.AddressCountry = "Brasil";
            newAddress.AddressZipCode = "18011236";
            newAddress.AddressType = AddressType.Commercial;

            var realtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            var realtyBeneficiaryId = new Guid("080eef5c-6d09-441b-af39-5802d9201701");
            var realtyAddress = newAddress;
            var realtyMunicipalRegistration = "abc68dfr98721dxe1";
            var realtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            var realtySaleValue = 800000.00;
            var realtyMarketValue = 1000000.00;

            var mobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            var mobileDeviceBeneficiaryId = new Guid("98d052e2-10bf-4d26-8aa2-39c1e72800ea");
            var mobileDeviceBrand = "Motorola";
            var mobileDeviceModel = "Moto G3";
            var mobileDeviceSerialNumber = "513475984000749";
            var mobileDeviceManufactoringYear = new DateTime(2019);
            var mobileDeviceType = MobileDeviceType.Smartphone;
            var mobileDeviceInvoiceValue = 800.00;

            var petId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            var petBeneficiaryId = new Guid("0ed6cf6e-c95a-4635-8d47-a751d2b8953d");
            var petName = "Robson";
            var petSpecies = PetSpecies.Canis_lupus_familiaris;
            var petBreed = "Pitbull";
            var petBirthdate = new DateTime(2016, 02, 28);

            var vehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
            var vehicleBeneficiaryId = new Guid("694d2c31-78e0-4d9e-b952-4042ebf7f59d");
            var vehicleBrand = "Chevrolet";
            var vehicleModel = "Celta";
            var vehicleManufactoringYear = new DateTime(2006);
            var vehicleColor = Color.White;
            var vehicleModelYear = new DateTime(2005);
            var vehicleChassisNumber = "2T2BK1BA8FC259487";
            var vehicleCurrentMileage = 100000;
            var vehicleCurrentFipeValue = 25000;
            var vehicleDoneInspection = true;

            //act
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime();
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            //beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152897";
            beneficiary.IndividualRG = "458559462";
            beneficiary.IndividualEmail = "gftstarters2019@outlook.com";
            
            var address = new Address();
            address.AddressId = new Guid("029ec5eb-a126-45e4-981c-2db4e380c9eb");
            address.AddressStreet = "Rua Sales Fonseca";
            address.AddressNumber = "50";
            address.AddressComplement = "N/A";
            address.AddressNeighborhood = "Jardim Santo Amaro";
            address.AddressCity = "Sorocaba";
            address.AddressState = "São Paulo";
            address.AddressCountry = "Brasil";
            address.AddressZipCode = "18011236";
            address.AddressType = AddressType.Commercial;

            var realty = new Realty();
            //realty.RealtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            realty.BeneficiaryId = new Guid("080eef5c-6d09-441b-af39-5802d9201701");
            //realty.RealtyAddress = address;
            realty.RealtyMunicipalRegistration = "abc68dfr98721dxe1";
            realty.RealtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            realty.RealtySaleValue = 800000.00;
            realty.RealtyMarketValue = 1000000.00;

            var mobileDevice = new MobileDevice();
            //mobileDevice.MobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            mobileDevice.BeneficiaryId = new Guid("98d052e2-10bf-4d26-8aa2-39c1e72800ea");
            mobileDevice.MobileDeviceBrand = "Motorola";
            mobileDevice.MobileDeviceModel = "Moto G3";
            mobileDevice.MobileDeviceSerialNumber = "513475984000749";
            mobileDevice.MobileDeviceManufactoringYear = new DateTime(2019);
            mobileDevice.MobileDeviceType = MobileDeviceType.Smartphone;
            mobileDevice.MobileDeviceInvoiceValue = 800.00;

            var pet = new Pet();
            //pet.PetId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            pet.BeneficiaryId = new Guid("0ed6cf6e-c95a-4635-8d47-a751d2b8953d");
            pet.PetName = "Robson";
            pet.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            pet.PetBreed = "Pitbull";
            pet.PetBirthdate = new DateTime(2016, 02, 28);

            var vehicle = new Vehicle();
            //vehicle.VehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
            vehicle.BeneficiaryId = new Guid("694d2c31-78e0-4d9e-b952-4042ebf7f59d");
            vehicle.VehicleBrand = "Chevrolet";
            vehicle.VehicleModel = "Celta";
            vehicle.VehicleManufactoringYear = new DateTime(2006);
            vehicle.VehicleColor = Color.White;
            vehicle.VehicleModelYear = new DateTime(2005);
            vehicle.VehicleChassisNumber = "2T2BK1BA8FC259487";
            vehicle.VehicleCurrentMileage = 100000;
            vehicle.VehicleCurrentFipeValue = 25000;
            vehicle.VehicleDoneInspection = true;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.SignedContractId;
            contractBeneficiary.BeneficiaryId = individual.BeneficiaryId;
            contractBeneficiary.SignedContract = signedContract;
            contractBeneficiary.Beneficiary = individual;

            //assert
            //Assert.AreEqual(beneficiary.IndividualId, individualId);
            Assert.AreEqual(beneficiary.BeneficiaryId, beneficiaryId);
            Assert.AreEqual(beneficiary.IndividualBirthdate, individualBirthdate);
            Assert.AreEqual(beneficiary.IndividualCPF, individualCPF);
            Assert.AreEqual(beneficiary.IndividualRG, individualRG);
            Assert.AreEqual(beneficiary.IndividualEmail, individualEmail);
            
            //Assert.AreEqual(realty.RealtyId, realtyId);
            Assert.AreEqual(realty.BeneficiaryId, realtyBeneficiaryId);
            //Assert.AreEqual(realty.RealtyAddress, realtyAddress);
            Assert.AreEqual(realty.RealtyMunicipalRegistration, realtyMunicipalRegistration);
            Assert.AreEqual(realty.RealtyConstructionDate, realtyConstructionDate);
            Assert.AreEqual(realty.RealtySaleValue, realtySaleValue);
            Assert.AreEqual(realty.RealtyMarketValue, realtyMarketValue);

            //Assert.AreEqual(mobileDevice.MobileDeviceId, mobileDeviceId);
            Assert.AreEqual(mobileDevice.BeneficiaryId, mobileDeviceBeneficiaryId);
            Assert.AreEqual(mobileDevice.MobileDeviceBrand, mobileDeviceBrand);
            Assert.AreEqual(mobileDevice.MobileDeviceModel, mobileDeviceModel);
            Assert.AreEqual(mobileDevice.MobileDeviceSerialNumber, mobileDeviceSerialNumber);
            Assert.AreEqual(mobileDevice.MobileDeviceManufactoringYear, mobileDeviceManufactoringYear);
            Assert.AreEqual(mobileDevice.MobileDeviceType, mobileDeviceType);
            Assert.AreEqual(mobileDevice.MobileDeviceInvoiceValue, mobileDeviceInvoiceValue);

            //Assert.AreEqual(pet.PetId, petId);
            Assert.AreEqual(pet.BeneficiaryId, petBeneficiaryId);
            Assert.AreEqual(pet.PetName, petName);
            Assert.AreEqual(pet.PetSpecies, petSpecies);
            Assert.AreEqual(pet.PetBreed, petBreed);
            Assert.AreEqual(pet.PetBirthdate, petBirthdate);

            //Assert.AreEqual(vehicle.VehicleId, vehicleId);
            Assert.AreEqual(vehicle.BeneficiaryId, vehicleBeneficiaryId);
            Assert.AreEqual(vehicle.VehicleBrand, vehicleBrand);
            Assert.AreEqual(vehicle.VehicleModel, vehicleModel);
            Assert.AreEqual(vehicle.VehicleManufactoringYear, vehicleManufactoringYear);
            Assert.AreEqual(vehicle.VehicleColor, vehicleColor);
            Assert.AreEqual(vehicle.VehicleModelYear, vehicleModelYear);
            Assert.AreEqual(vehicle.VehicleChassisNumber, vehicleChassisNumber);
            Assert.AreEqual(vehicle.VehicleCurrentMileage, vehicleCurrentMileage);
            Assert.AreEqual(vehicle.VehicleCurrentFipeValue, vehicleCurrentFipeValue);
            Assert.AreEqual(vehicle.VehicleDoneInspection, vehicleDoneInspection);

        }

        [Test]
        public void WhenCreateABeneficiary_ThenVerifyIfICanFindById()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            //beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            
            var address = new Address();
            address.AddressId = new Guid("029ec5eb-a126-45e4-981c-2db4e380c9eb");
            address.AddressStreet = "Rua Sales Fonseca";
            address.AddressNumber = "50";
            address.AddressComplement = "N/A";
            address.AddressNeighborhood = "Jardim Santo Amaro";
            address.AddressCity = "Sorocaba";
            address.AddressState = "São Paulo";
            address.AddressCountry = "Brasil";
            address.AddressZipCode = "18011236";
            address.AddressType = AddressType.Commercial;

            var realty = new Realty();
            //realty.RealtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            realty.BeneficiaryId = new Guid("fe0216e8-e70a-4c7c-a5ae-f0b8e1319885");
            //realty.RealtyAddress = address;
            realty.RealtyMunicipalRegistration = "abc68dfr98721dxe1";
            realty.RealtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            realty.RealtySaleValue = 800000.00;
            realty.RealtyMarketValue = 1000000.00;

            var mobileDevice = new MobileDevice();
            //mobileDevice.MobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            mobileDevice.BeneficiaryId = new Guid("98d052e2-10bf-4d26-8aa2-39c1e72800ea");
            mobileDevice.MobileDeviceBrand = "Motorola";
            mobileDevice.MobileDeviceModel = "Moto G3";
            mobileDevice.MobileDeviceSerialNumber = "513475984000749";
            mobileDevice.MobileDeviceManufactoringYear = new DateTime(2019);
            mobileDevice.MobileDeviceType = MobileDeviceType.Smartphone;
            mobileDevice.MobileDeviceInvoiceValue = 800.00;

            var pet = new Pet();
            //pet.PetId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            pet.BeneficiaryId = new Guid("0ed6cf6e-c95a-4635-8d47-a751d2b8953d");
            pet.PetName = "Robson";
            pet.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            pet.PetBreed = "Pitbull";
            pet.PetBirthdate = new DateTime(2016, 02, 28);

            var vehicle = new Vehicle();
            //vehicle.VehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
            vehicle.BeneficiaryId = new Guid("694d2c31-78e0-4d9e-b952-4042ebf7f59d");
            vehicle.VehicleBrand = "Chevrolet";
            vehicle.VehicleModel = "Celta";
            vehicle.VehicleManufactoringYear = new DateTime(2006);
            vehicle.VehicleColor = Color.White;
            vehicle.VehicleModelYear = new DateTime(2005);
            vehicle.VehicleChassisNumber = "2T2BK1BA8FC259487";
            vehicle.VehicleCurrentMileage = 100000;
            vehicle.VehicleCurrentFipeValue = 25000;
            vehicle.VehicleDoneInspection = true;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;


            //act
            var individualBeneficiaryId = beneficiary.BeneficiaryId;
            var realtyBeneficiaryId = realty.BeneficiaryId;
            var mobileDeviceBeneficiaryId = mobileDevice.BeneficiaryId;
            var petBeneficiaryId = pet.BeneficiaryId;
            var vehicleBeneficiaryId = vehicle.BeneficiaryId;


            //assert
            Assert.AreEqual(beneficiary.BeneficiaryId, individualBeneficiaryId);
            Assert.AreEqual(realty.BeneficiaryId, realtyBeneficiaryId);
            Assert.AreEqual(mobileDevice.BeneficiaryId, mobileDeviceBeneficiaryId);
            Assert.AreEqual(pet.BeneficiaryId, petBeneficiaryId);
            Assert.AreEqual(vehicle.BeneficiaryId, vehicleBeneficiaryId);
        }

        [Test]
        public void WhenCreateBeneficiary_AndUpdateIt_ThenVerifyIfItWasUpdated()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
           // beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            
            var address = new Address();
            address.AddressId = new Guid("029ec5eb-a126-45e4-981c-2db4e380c9eb");
            address.AddressStreet = "Rua Sales Fonseca";
            address.AddressNumber = "50";
            address.AddressComplement = "N/A";
            address.AddressNeighborhood = "Jardim Santo Amaro";
            address.AddressCity = "Sorocaba";
            address.AddressState = "São Paulo";
            address.AddressCountry = "Brasil";
            address.AddressZipCode = "18011236";
            address.AddressType = AddressType.Commercial;

            var realty = new Realty();
            //realty.RealtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            realty.BeneficiaryId = new Guid("080eef5c-6d09-441b-af39-5802d9201701");
            //realty.RealtyAddress = address;
            realty.RealtyMunicipalRegistration = "abc68dfr98721dxe1";
            realty.RealtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            realty.RealtySaleValue = 800000.00;
            realty.RealtyMarketValue = 1000000.00;

            var mobileDevice = new MobileDevice();
            //mobileDevice.MobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            mobileDevice.BeneficiaryId = new Guid("98d052e2-10bf-4d26-8aa2-39c1e72800ea");
            mobileDevice.MobileDeviceBrand = "Motorola";
            mobileDevice.MobileDeviceModel = "Moto G3";
            mobileDevice.MobileDeviceSerialNumber = "513475984000749";
            mobileDevice.MobileDeviceManufactoringYear = new DateTime(2019);
            mobileDevice.MobileDeviceType = MobileDeviceType.Smartphone;
            mobileDevice.MobileDeviceInvoiceValue = 800.00;

            var pet = new Pet();
            //pet.PetId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            pet.BeneficiaryId = new Guid("0ed6cf6e-c95a-4635-8d47-a751d2b8953d");
            pet.PetName = "Robson";
            pet.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            pet.PetBreed = "Pitbull";
            pet.PetBirthdate = new DateTime(2016, 02, 28);

            var vehicle = new Vehicle();
            //vehicle.VehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
            vehicle.BeneficiaryId = new Guid("694d2c31-78e0-4d9e-b952-4042ebf7f59d");
            vehicle.VehicleBrand = "Chevrolet";
            vehicle.VehicleModel = "Celta";
            vehicle.VehicleManufactoringYear = new DateTime(2006);
            vehicle.VehicleColor = Color.White;
            vehicle.VehicleModelYear = new DateTime(2005);
            vehicle.VehicleChassisNumber = "2T2BK1BA8FC259487";
            vehicle.VehicleCurrentMileage = 100000;
            vehicle.VehicleCurrentFipeValue = 25000;
            vehicle.VehicleDoneInspection = true;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            //act
            var updatedBeneficiary = new Individual();
            updatedBeneficiary.BeneficiaryId = Guid.NewGuid();
            //updatedBeneficiary.IndividualId = Guid.NewGuid();
            updatedBeneficiary.IndividualBirthdate = new DateTime(2018, 05, 28, 7, 0, 0);
            updatedBeneficiary.IndividualCPF = "45301152975";
            updatedBeneficiary.IndividualRG = "458559464";
            updatedBeneficiary.IndividualEmail = "gftstarters15@outlook.com";
            
            var newAddress = new Address();
            newAddress.AddressId = Guid.NewGuid();
            newAddress.AddressStreet = "Rua João Picarpo";
            newAddress.AddressNumber = "45";
            newAddress.AddressComplement = "Casa 08";
            newAddress.AddressNeighborhood = "Vila Gabriel";
            newAddress.AddressCity = "Uberlândia";
            newAddress.AddressState = "Minas Gerais";
            newAddress.AddressCountry = "Brasil";
            newAddress.AddressZipCode = "18011777";
            newAddress.AddressType = AddressType.Home;

            var updatedRealty = new Realty();
            //updatedRealty.RealtyId = Guid.NewGuid();
            updatedRealty.BeneficiaryId = Guid.NewGuid();
            //updatedRealty.RealtyAddress = newAddress;
            updatedRealty.RealtyMunicipalRegistration = "abc68dfy98721dze1";
            updatedRealty.RealtyConstructionDate = new DateTime(2018, 05, 15, 7, 0, 0);
            updatedRealty.RealtySaleValue = 850000.00;
            updatedRealty.RealtyMarketValue = 2000000.00;

            var updatedMobileDevice = new MobileDevice();
            //updatedMobileDevice.MobileDeviceId = Guid.NewGuid();
            updatedMobileDevice.BeneficiaryId = Guid.NewGuid();
            updatedMobileDevice.MobileDeviceBrand = "Lenovo";
            updatedMobileDevice.MobileDeviceModel = "Ideapad 320";
            updatedMobileDevice.MobileDeviceSerialNumber = "622819984111749";
            updatedMobileDevice.MobileDeviceManufactoringYear = new DateTime(2014);
            updatedMobileDevice.MobileDeviceType = MobileDeviceType.Laptop;
            updatedMobileDevice.MobileDeviceInvoiceValue = 2800.00;

            var updatedPet = new Pet();
            //updatedPet.PetId = Guid.NewGuid();
            updatedPet.BeneficiaryId = Guid.NewGuid();
            updatedPet.PetName = "Tyson";
            updatedPet.PetSpecies = PetSpecies.Felis_catus;
            updatedPet.PetBreed = "Persa";
            updatedPet.PetBirthdate = new DateTime(2015, 05, 12);

            var updatedVehicle = new Vehicle();
            //updatedVehicle.VehicleId = Guid.NewGuid();
            updatedVehicle.BeneficiaryId = Guid.NewGuid();
            updatedVehicle.VehicleBrand = "Ford";
            updatedVehicle.VehicleModel = "Focus";
            updatedVehicle.VehicleManufactoringYear = new DateTime(2013);
            updatedVehicle.VehicleColor = Color.Blue;
            updatedVehicle.VehicleModelYear = new DateTime(2014);
            updatedVehicle.VehicleChassisNumber = "4B2BL1CY8XC879555";
            updatedVehicle.VehicleCurrentMileage = 50000;
            updatedVehicle.VehicleCurrentFipeValue = 32000;
            updatedVehicle.VehicleDoneInspection = false;

            //assert
            //Assert.AreNotEqual(updatedBeneficiary.IndividualId, beneficiary.IndividualId);
            Assert.AreNotEqual(updatedBeneficiary.BeneficiaryId, beneficiary.BeneficiaryId);
            Assert.AreNotEqual(updatedBeneficiary.IndividualBirthdate, beneficiary.IndividualBirthdate);
            Assert.AreNotEqual(updatedBeneficiary.IndividualCPF, beneficiary.IndividualCPF);
            Assert.AreNotEqual(updatedBeneficiary.IndividualRG, beneficiary.IndividualRG);
            Assert.AreNotEqual(updatedBeneficiary.IndividualEmail, beneficiary.IndividualEmail);

            //Assert.AreNotEqual(updatedRealty.RealtyId, realty.RealtyId);
            Assert.AreNotEqual(updatedRealty.BeneficiaryId, realty.BeneficiaryId);
            //Assert.AreNotEqual(updatedRealty.RealtyAddress, realty.RealtyAddress);
            Assert.AreNotEqual(updatedRealty.RealtyMunicipalRegistration, realty.RealtyMunicipalRegistration);
            Assert.AreNotEqual(updatedRealty.RealtyConstructionDate, realty.RealtyConstructionDate);
            Assert.AreNotEqual(updatedRealty.RealtySaleValue, realty.RealtySaleValue);
            Assert.AreNotEqual(updatedRealty.RealtyMarketValue, realty.RealtyMarketValue);

            //Assert.AreNotEqual(updatedMobileDevice.MobileDeviceId, mobileDevice.MobileDeviceId);
            Assert.AreNotEqual(updatedMobileDevice.BeneficiaryId, mobileDevice.BeneficiaryId);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceBrand, mobileDevice.MobileDeviceBrand);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceModel, mobileDevice.MobileDeviceModel);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceSerialNumber, mobileDevice.MobileDeviceSerialNumber);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceManufactoringYear, mobileDevice.MobileDeviceManufactoringYear);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceType, mobileDevice.MobileDeviceType);
            Assert.AreNotEqual(updatedMobileDevice.MobileDeviceInvoiceValue, mobileDevice.MobileDeviceInvoiceValue);

            //Assert.AreNotEqual(updatedPet.PetId, pet.PetId);
            Assert.AreNotEqual(updatedPet.BeneficiaryId, pet.BeneficiaryId);
            Assert.AreNotEqual(updatedPet.PetName, pet.PetName);
            Assert.AreNotEqual(updatedPet.PetSpecies, pet.PetSpecies);
            Assert.AreNotEqual(updatedPet.PetBreed, pet.PetBreed);
            Assert.AreNotEqual(updatedPet.PetBirthdate, pet.PetBirthdate);

            //Assert.AreNotEqual(updatedVehicle.VehicleId, vehicle.VehicleId);
            Assert.AreNotEqual(updatedVehicle.BeneficiaryId, vehicle.BeneficiaryId);
            Assert.AreNotEqual(updatedVehicle.VehicleBrand, vehicle.VehicleBrand);
            Assert.AreNotEqual(updatedVehicle.VehicleModel, vehicle.VehicleModel);
            Assert.AreNotEqual(updatedVehicle.VehicleManufactoringYear, vehicle.VehicleManufactoringYear);
            Assert.AreNotEqual(updatedVehicle.VehicleColor, vehicle.VehicleColor);
            Assert.AreNotEqual(updatedVehicle.VehicleModelYear, vehicle.VehicleModelYear);
            Assert.AreNotEqual(updatedVehicle.VehicleChassisNumber, vehicle.VehicleChassisNumber);
            Assert.AreNotEqual(updatedVehicle.VehicleCurrentMileage, vehicle.VehicleCurrentMileage);
            Assert.AreNotEqual(updatedVehicle.VehicleCurrentFipeValue, vehicle.VehicleCurrentFipeValue);
            Assert.AreNotEqual(updatedVehicle.VehicleDoneInspection, vehicle.VehicleDoneInspection);
        }


        [Test]
        public void WhenCreateABeneficiary_AndDeleteIt_ThenVerifyIfItWasDeleted()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            
            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            //beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            
            var address = new Address();
            address.AddressId = new Guid("029ec5eb-a126-45e4-981c-2db4e380c9eb");
            address.AddressStreet = "Rua Sales Fonseca";
            address.AddressNumber = "50";
            address.AddressComplement = "N/A";
            address.AddressNeighborhood = "Jardim Santo Amaro";
            address.AddressCity = "Sorocaba";
            address.AddressState = "São Paulo";
            address.AddressCountry = "Brasil";
            address.AddressZipCode = "18011236";
            address.AddressType = AddressType.Commercial;

            var realty = new Realty();
            //realty.RealtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            realty.BeneficiaryId = new Guid("080eef5c-6d09-441b-af39-5802d9201701");
            //realty.RealtyAddress = address;
            realty.RealtyMunicipalRegistration = "abc68dfr98721dxe1";
            realty.RealtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            realty.RealtySaleValue = 800000.00;
            realty.RealtyMarketValue = 1000000.00;

            var mobileDevice = new MobileDevice();
            //mobileDevice.MobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            mobileDevice.BeneficiaryId = new Guid("98d052e2-10bf-4d26-8aa2-39c1e72800ea");
            mobileDevice.MobileDeviceBrand = "Motorola";
            mobileDevice.MobileDeviceModel = "Moto G3";
            mobileDevice.MobileDeviceSerialNumber = "513475984000749";
            mobileDevice.MobileDeviceManufactoringYear = new DateTime(2019);
            mobileDevice.MobileDeviceType = MobileDeviceType.Smartphone;
            mobileDevice.MobileDeviceInvoiceValue = 800.00;

            var pet = new Pet();
            //pet.PetId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            pet.BeneficiaryId = new Guid("0ed6cf6e-c95a-4635-8d47-a751d2b8953d");
            pet.PetName = "Robson";
            pet.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            pet.PetBreed = "Pitbull";
            pet.PetBirthdate = new DateTime(2016, 02, 28);

            var vehicle = new Vehicle();
            //vehicle.VehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
            vehicle.BeneficiaryId = new Guid("694d2c31-78e0-4d9e-b952-4042ebf7f59d");
            vehicle.VehicleBrand = "Chevrolet";
            vehicle.VehicleModel = "Celta";
            vehicle.VehicleManufactoringYear = new DateTime(2006);
            vehicle.VehicleColor = Color.White;
            vehicle.VehicleModelYear = new DateTime(2005);
            vehicle.VehicleChassisNumber = "2T2BK1BA8FC259487";
            vehicle.VehicleCurrentMileage = 100000;
            vehicle.VehicleCurrentFipeValue = 25000;
            vehicle.VehicleDoneInspection = true;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            //act
            //var individualBeneficiaryDeleted = beneficiary.BeneficiaryDeleted = true;
            //var realtyBeneficiaryDeleted = realty.BeneficiaryDeleted = true;
            //var mobileDeviceBeneficiaryDeleted = mobileDevice.BeneficiaryDeleted = true;
            //var petBeneficiaryDeleted = pet.BeneficiaryDeleted = true;
            //var vehicleBeneficaryDeleted = vehicle.BeneficiaryDeleted = true;

            //assert
            //Assert.IsTrue(individualBeneficiaryDeleted);
            //Assert.IsTrue(realtyBeneficiaryDeleted);
            //Assert.IsTrue(mobileDeviceBeneficiaryDeleted);
            //Assert.IsTrue(petBeneficiaryDeleted);
            //Assert.IsTrue(vehicleBeneficaryDeleted);
        }
    }
}