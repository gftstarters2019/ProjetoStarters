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
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            beneficiary.IndividualDeleted = false;

            //act
            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.ContractSignedId;
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
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            //act
            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.ContractSignedId;
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
            var realtyAddress = newAddress;
            var realtyMunicipalRegistration = "abc68dfr98721dxe1";
            var realtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            var realtySaleValue = 800000.00;
            var realtyMarketValue = 1000000.00;

            var mobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            var mobileDeviceBrand = "Motorola";
            var mobileDeviceModel = "Moto G3";
            var mobileDeviceSerialNumber = "513475984000749";
            var mobileDeviceManufactoringYear = new DateTime(2019);
            var mobileDeviceType = MobileDeviceType.Smartphone;
            var mobileDeviceInvoiceValue = 800.00;

            var petId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            var petName = "Robson";
            var petSpecies = PetSpecies.Canis_lupus_familiaris;
            var petBreed = "Pitbull";
            var petBirthdate = new DateTime(2016, 02, 28);

            var vehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
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
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime();
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            var beneficiary = new Individual();
            beneficiary.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            beneficiary.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            beneficiary.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            beneficiary.IndividualCPF = "45301152978";
            beneficiary.IndividualRG = "458559463";
            beneficiary.IndividualEmail = "gftstarters@outlook.com";
            beneficiary.IndividualDeleted = false;

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
            realty.RealtyId = new Guid("8e52b1c0-7eec-492d-b16a-dc35dca134c2");
            realty.RealtyAddress = address;
            realty.RealtyMunicipalRegistration = "abc68dfr98721dxe1";
            realty.RealtyConstructionDate = new DateTime(2019, 05, 15, 7, 0, 0);
            realty.RealtySaleValue = 800000.00;
            realty.RealtyMarketValue = 1000000.00;

            var mobileDevice = new MobileDevice();
            mobileDevice.MobileDeviceId = new Guid("7922815c-de08-4423-b973-a62f97e97586");
            mobileDevice.MobileDeviceBrand = "Motorola";
            mobileDevice.MobileDeviceModel = "Moto G3";
            mobileDevice.MobileDeviceSerialNumber = "513475984000749";
            mobileDevice.MobileDeviceManufactoringYear = new DateTime(2019);
            mobileDevice.MobileDeviceType = MobileDeviceType.Smartphone;
            mobileDevice.MobileDeviceInvoiceValue = 800.00;

            var pet = new Pet();
            pet.PetId = new Guid("b54b58d4-1199-4b85-aeb2-3165b561e418");
            pet.PetName = "Robson";
            pet.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            pet.PetBreed = "Pitbull";
            pet.PetBirthdate = new DateTime(2016, 02, 28);

            var vehicle = new Vehicle();
            vehicle.VehicleId = new Guid("7b0f59cb-310b-43ef-a73a-e4ce09e9ca4d");
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
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;

            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContract.ContractSignedId;
            contractBeneficiary.BeneficiaryId = individual.BeneficiaryId;
            contractBeneficiary.SignedContract = signedContract;
            contractBeneficiary.Beneficiary = individual;

            //assert
            Assert.AreEqual(beneficiary.IndividualId, individualId);
            Assert.AreEqual(beneficiary.BeneficiaryId, beneficiaryId);
            Assert.AreEqual(beneficiary.IndividualBirthdate, individualBirthdate);
            Assert.AreEqual(beneficiary.IndividualCPF, individualCPF);
            Assert.AreEqual(beneficiary.IndividualRG, individualRG);
            Assert.AreEqual(beneficiary.IndividualEmail, individualEmail);
            Assert.AreEqual(beneficiary.IndividualDeleted, individualDeleted);

            Assert.AreEqual(address.AddressId, newAddress.AddressId);
            Assert.AreEqual(address.AddressStreet, newAddress.AddressStreet);
            Assert.AreEqual(address.AddressNumber, newAddress.AddressNumber);
            Assert.AreEqual(address.AddressComplement, newAddress.AddressComplement);
            Assert.AreEqual(address.AddressNeighborhood, newAddress.AddressNeighborhood);
            Assert.AreEqual(address.AddressCity, newAddress.AddressCity);
            Assert.AreEqual(address.AddressState, newAddress.AddressState);
            Assert.AreEqual(address.AddressCountry, newAddress);
            Assert.AreEqual(address.AddressZipCode, newAddress.AddressZipCode);
            Assert.AreEqual(address.AddressType, newAddress.AddressType);

            Assert.AreEqual(realty.RealtyId, realtyId);
            Assert.AreEqual(realty.RealtyAddress, realtyAddress);
            Assert.AreEqual(realty.RealtyMunicipalRegistration, realtyMunicipalRegistration);
            Assert.AreEqual(realty.RealtyConstructionDate, realtyConstructionDate);
            Assert.AreEqual(realty.RealtySaleValue, realtySaleValue);
            Assert.AreEqual(realty.RealtyMarketValue, realtyMarketValue);
        }

        [Test]
        public void WhenCreateAContractHolder_ThenVerifyIfICanFindById()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;


            //act
            var individualId = individual.IndividualId;

            //assert
            Assert.AreEqual(individual.IndividualId, individualId);
        }

        [Test]
        public void WhenCreateContractHolder_AndUpdateHim_ThenVerifyIfHeWasUpdated()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;


            var updatedIndividual = new Individual();

            var beneficiaryId = Guid.NewGuid();
            var individualId = Guid.NewGuid();
            var individualBirthdate = new DateTime();
            var individualCPF = "45301152898";
            var individualRG = "458559466";
            var individualEmail = "gftstarters@outlook.com";
            var individualDeleted = false;

            //act
            updatedIndividual.BeneficiaryId = beneficiaryId;
            updatedIndividual.IndividualId = individualId;
            updatedIndividual.IndividualBirthdate = individualBirthdate;
            updatedIndividual.IndividualCPF = individualCPF;
            updatedIndividual.IndividualRG = individualRG;
            updatedIndividual.IndividualEmail = individualEmail;

            //assert
            Assert.AreNotEqual(individual.IndividualId, updatedIndividual.IndividualId);
            Assert.AreNotEqual(individual.BeneficiaryId, updatedIndividual.BeneficiaryId);
            Assert.AreNotEqual(individual.IndividualBirthdate, updatedIndividual.IndividualBirthdate);
            Assert.AreNotEqual(individual.IndividualCPF, updatedIndividual.IndividualCPF);
            Assert.AreNotEqual(individual.IndividualRG, updatedIndividual.IndividualRG);
            Assert.AreNotEqual(individual.IndividualEmail, updatedIndividual.IndividualEmail);
        }


        [Test]
        public void WhenCreateAContractHolder_AndDeleteHim_ThenVerifyIfHeWasDeleted()
        {
            //arrange
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualDeleted = false;

            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;
            signedContract.ContractIndividualIsActive = true;

            //act
            var signedContractDeleted = signedContract.ContractIndividualIsActive = false;
            var contractDeleted = contract.ContractDeleted = true;
            var individualDeleted = individual.IndividualDeleted = true;

            //assert
            Assert.IsFalse(signedContractDeleted);
            Assert.IsTrue(contractDeleted);
            Assert.IsTrue(individualDeleted);
        }
    }
}