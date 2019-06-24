using Backend.Application.ViewModels;
using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.IntegrationTests;
using Backend.IntegrationTests.ControllerTest;
using Backend.IntegrationTests.Helper;
using ContractHolder.WebAPI;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ContractTest : BaseIntegrationTest
    {
        private const string url = "/api/Contract";
        private const string urlContractHolder = "/api/ContractHolder";

        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContracts()
        {

            // act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<ContractViewModel>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfIndividuals_ThenICanGetContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.DentalPlan;
            contract.Category = ContractCategory.Silver;
            contract.Individuals = new List<Individual>();
            contract.ExpiryDate = new DateTime(2020, 10, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Caio Silva Ferreira";
            contractHolder.individualEmail = "CaioSilva@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "335373793";
            contractHolder.individualBirthdate = new DateTime(1966, 12, 28);

            Individual firstBeneficiary = new Individual();
            firstBeneficiary.IndividualName = "Bianca Correia Fernandes";
            firstBeneficiary.IndividualEmail = "BiancaCorreia@teleworm.com";
            firstBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            firstBeneficiary.IndividualRG = "335838108";
            firstBeneficiary.IndividualBirthdate = new DateTime(1978, 6, 19);
            contract.Individuals.Add(firstBeneficiary);

            Individual secondBeneficiary = new Individual();
            secondBeneficiary.IndividualName = "Cau� Costa Lima";
            secondBeneficiary.IndividualEmail = "CauaCosta@jourrapide.com";
            secondBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            secondBeneficiary.IndividualRG = "109014509";
            secondBeneficiary.IndividualBirthdate = new DateTime(1993, 4, 21);
            contract.Individuals.Add(secondBeneficiary);

            Individual thirdBeneficiary = new Individual();
            thirdBeneficiary.IndividualName = "Livia Pereira Costa";
            thirdBeneficiary.IndividualEmail = "LiviaPereira@dayrep.com";
            thirdBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            thirdBeneficiary.IndividualRG = "102653604";
            thirdBeneficiary.IndividualBirthdate = new DateTime(1957, 7, 24);
            contract.Individuals.Add(thirdBeneficiary);

            Individual fourthBeneficiary = new Individual();
            fourthBeneficiary.IndividualName = "Beatrice Fernandes Barbosa";
            fourthBeneficiary.IndividualEmail = "BeatriceFernandes@teleworm.us";
            fourthBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            fourthBeneficiary.IndividualRG = "484661632";
            fourthBeneficiary.IndividualBirthdate = new DateTime(1955, 9, 3);
            contract.Individuals.Add(fourthBeneficiary);

            Individual fifthBeneficiary = new Individual();
            fifthBeneficiary.IndividualName = "J�lio Souza Barros";
            fifthBeneficiary.IndividualEmail = "JulioSouza@rhyta.com";
            fifthBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            fifthBeneficiary.IndividualRG = "275136383";
            fifthBeneficiary.IndividualBirthdate = new DateTime(1962, 5, 1);
            contract.Individuals.Add(fifthBeneficiary);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractHolderPost = await client.PostAsync($"{urlContractHolder}", contentString);
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.individualId;

            jsonContent = JsonConvert.SerializeObject(contract);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractPost = await client.PostAsync($"{url}", contentString);
            var contractPostApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await contractPost.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<ContractViewModel>(getApiResponse);
            Assert.AreEqual(contract.ContractHolderId, getApiResponse.ContractHolderId);
            Assert.AreEqual(contract.ExpiryDate, getApiResponse.ExpiryDate);
            Assert.AreEqual(contract.Type, getApiResponse.Type);
            Assert.AreEqual(contract.Category, getApiResponse.Category);
            Assert.AreEqual(contract.IsActive, getApiResponse.IsActive);
            Assert.AreEqual(contractPostApiResponse.SignedContractId, getApiResponse.SignedContractId);
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfPets_ThenICanRequestContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.AnimalHealthPlan;
            contract.Category = ContractCategory.Gold;
            contract.Pets = new List<Pet>();
            contract.ExpiryDate = new DateTime(2032, 1, 1);
            contract.IsActive = false;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Isabela Rocha Costa";
            contractHolder.individualEmail = "IsabelaRocha@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "156188326";
            contractHolder.individualBirthdate = new DateTime(1953, 11, 4);

            Pet firstBeneficiary = new Pet();
            firstBeneficiary.PetName = "Joe";
            firstBeneficiary.PetBreed = "Mixed";
            firstBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            firstBeneficiary.PetBirthdate = new DateTime(2005, 7, 20);
            contract.Pets.Add(firstBeneficiary);

            Pet secondBeneficiary = new Pet();
            secondBeneficiary.PetName = "Lobo";
            secondBeneficiary.PetBreed = "Belgian Sheepdog";
            secondBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            secondBeneficiary.PetBirthdate = new DateTime(2010, 10, 25);
            contract.Pets.Add(secondBeneficiary);

            Pet thirdBeneficiary = new Pet();
            thirdBeneficiary.PetName = "Dasha";
            thirdBeneficiary.PetBreed = "Chihuahua";
            thirdBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            thirdBeneficiary.PetBirthdate = new DateTime(2016, 4, 1);
            contract.Pets.Add(thirdBeneficiary);

            Pet fourthBeneficiary = new Pet();
            fourthBeneficiary.PetName = "Axel";
            fourthBeneficiary.PetBreed = "Jack Russell Terrier";
            fourthBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            fourthBeneficiary.PetBirthdate = new DateTime(2012, 2, 28);
            contract.Pets.Add(fourthBeneficiary);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractHolderPost = await client.PostAsync($"{urlContractHolder}", contentString);
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.individualId;

            jsonContent = JsonConvert.SerializeObject(contract);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractPost = await client.PostAsync($"{url}", contentString);
            var contractPostApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await contractPost.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractViewModel>(getApiResponse);
            Assert.AreEqual(contract.ContractHolderId, getApiResponse.ContractHolderId);
            Assert.AreEqual(contract.ExpiryDate, getApiResponse.ExpiryDate);
            Assert.AreEqual(contract.Type, getApiResponse.Type);
            Assert.AreEqual(contract.Category, getApiResponse.Category);
            Assert.AreEqual(contract.IsActive, getApiResponse.IsActive);
            Assert.AreEqual(contractPostApiResponse.SignedContractId, getApiResponse.SignedContractId);
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfMobileDevices_ThenICanUpdateAContractRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.MobileDevices = new List<MobileDevice>();
            contract.Type = (ContractType)6;
            contract.Category = (ContractCategory)2;
            contract.ExpiryDate = new DateTime(2019, 12, 30);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Eduardo Barbosa";
            contractHolder.individualEmail = "EduardoGoncalves@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "240875278";
            contractHolder.individualBirthdate = new DateTime(1970, 8, 12);

            MobileDevice firstBeneficiary = new MobileDevice();
            firstBeneficiary.MobileDeviceType = MobileDeviceType.Smartphone;
            firstBeneficiary.MobileDeviceBrand = "Motorola";
            firstBeneficiary.MobileDeviceModel = "MotoZ";
            firstBeneficiary.MobileDeviceSerialNumber = "sjktZgP2";
            firstBeneficiary.MobileDeviceManufactoringYear = new DateTime(2017, 6, 25);
            firstBeneficiary.MobileDeviceInvoiceValue = 1099.90;
            contract.MobileDevices.Add(firstBeneficiary);

            MobileDevice secondBeneficiary = new MobileDevice();
            secondBeneficiary.MobileDeviceType = MobileDeviceType.Tablet;
            secondBeneficiary.MobileDeviceBrand = "Apple";
            secondBeneficiary.MobileDeviceModel = "IPad Pro";
            secondBeneficiary.MobileDeviceSerialNumber = "6HTCXhUT";
            secondBeneficiary.MobileDeviceManufactoringYear = new DateTime(2018, 9, 11);
            secondBeneficiary.MobileDeviceInvoiceValue = 4499.99;
            contract.MobileDevices.Add(secondBeneficiary);

            MobileDevice thirdBeneficiary = new MobileDevice();
            thirdBeneficiary.MobileDeviceType = MobileDeviceType.Laptop;
            thirdBeneficiary.MobileDeviceBrand = "Asus";
            thirdBeneficiary.MobileDeviceModel = "Pro SX";
            thirdBeneficiary.MobileDeviceSerialNumber = "qb4FsTyY";
            thirdBeneficiary.MobileDeviceManufactoringYear = new DateTime(2019, 2, 6);
            thirdBeneficiary.MobileDeviceInvoiceValue = 29.90;
            contract.MobileDevices.Add(thirdBeneficiary);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractHolderPost = await client.PostAsync($"{urlContractHolder}", contentString);
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.individualId;

            jsonContent = JsonConvert.SerializeObject(contract);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractPost = await client.PostAsync($"{url}", contentString);
            var contractPostApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await contractPost.Content.ReadAsStringAsync());
            
            contractPostApiResponse.ExpiryDate = new DateTime(1999, 5, 6);
            contractPostApiResponse.IsActive = false;

            jsonContent = JsonConvert.SerializeObject(contractPostApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{contractPostApiResponse.SignedContractId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractViewModel>(getApiResponse);
            Assert.AreEqual(putApiResponse.Category, getApiResponse.Category);
            Assert.AreEqual(putApiResponse.ExpiryDate, getApiResponse.ExpiryDate);
            Assert.AreEqual(putApiResponse.IsActive, getApiResponse.IsActive);
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostAndGet_ThenICanDeleteAContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.VehicleInsurance;
            contract.Category = ContractCategory.Diamond;
            contract.ExpiryDate = new DateTime(2021, 2, 1);
            contract.Vehicles = new List<Vehicle>();
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Felipe Araujo Souza";
            contractHolder.individualEmail = "FelipeAraujo@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "463205522";
            contractHolder.individualBirthdate = new DateTime(1992, 11, 28);

            Vehicle firstBeneficiary = new Vehicle();
            firstBeneficiary.VehicleBrand = "Daewoo";
            firstBeneficiary.VehicleModel = "Leganza";
            firstBeneficiary.VehicleChassisNumber = "XDI0923";
            firstBeneficiary.VehicleColor = Color.Gray;
            firstBeneficiary.VehicleCurrentFipeValue = 16978.00;
            firstBeneficiary.VehicleCurrentMileage = 100000;
            firstBeneficiary.VehicleDoneInspection = true;
            firstBeneficiary.VehicleManufactoringYear = new DateTime(2000, 1, 1);
            firstBeneficiary.VehicleModelYear = new DateTime(2000, 1, 1);
            contract.Vehicles.Add(firstBeneficiary);

            Vehicle secondBeneficiary = new Vehicle();
            secondBeneficiary.VehicleBrand = "Ford";
            secondBeneficiary.VehicleModel = "Fiesta";
            secondBeneficiary.VehicleChassisNumber = "LOP8761";
            secondBeneficiary.VehicleColor = Color.Silver;
            secondBeneficiary.VehicleCurrentFipeValue = 19870.00;
            secondBeneficiary.VehicleCurrentMileage = 200000;
            secondBeneficiary.VehicleDoneInspection = true;
            secondBeneficiary.VehicleManufactoringYear = new DateTime(2003, 1, 1);
            secondBeneficiary.VehicleModelYear = new DateTime(2003, 1, 1);
            contract.Vehicles.Add(secondBeneficiary);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractHolderPost = await client.PostAsync($"{urlContractHolder}", contentString);
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.individualId;

            jsonContent = JsonConvert.SerializeObject(contract);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var contractPost = await client.PostAsync($"{url}", contentString);
            var contractPostApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await contractPost.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{getApiResponse.SignedContractId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<ContractViewModel>(await deleteResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractViewModel>(deleteApiResponse);
            Assert.AreEqual(contract.Category,  deleteApiResponse.Category);
            Assert.AreEqual(contract.ExpiryDate, deleteApiResponse.ExpiryDate);
            Assert.AreEqual(contract.ContractHolderId, deleteApiResponse.ContractHolderId);
            Assert.AreEqual(contract.Type, deleteApiResponse.Type);
        }
    }
}