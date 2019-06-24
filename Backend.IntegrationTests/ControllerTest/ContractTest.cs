using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.IntegrationTests.ControllerTest;
using Backend.IntegrationTests.Helper;
using Contract.WebAPI.ViewModels;
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
            contract.Individuals = new List<IndividualDomain>();
            contract.ExpiryDate = new DateTime(2020, 10, 5);
            contract.IsActive = true;

            IndividualDomain contractHolder = new IndividualDomain();
            contractHolder.IndividualName = "Caio Silva Ferreira";
            contractHolder.IndividualEmail = "CaioSilva@rhyta.com";
            contractHolder.IndividualCPF = CpfGenerator.GenerateCpf();
            contractHolder.IndividualRG = "335373793";
            contractHolder.IndividualBirthdate = new DateTime(1966, 12, 28);

            IndividualDomain firstBeneficiary = new IndividualDomain();
            firstBeneficiary.IndividualName = "Bianca Correia Fernandes";
            firstBeneficiary.IndividualEmail = "BiancaCorreia@teleworm.com";
            firstBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            firstBeneficiary.IndividualRG = "335838108";
            firstBeneficiary.IndividualBirthdate = new DateTime(1978, 6, 19);
            contract.Individuals.Add(firstBeneficiary);

            IndividualDomain secondBeneficiary = new IndividualDomain();
            secondBeneficiary.IndividualName = "Cau� Costa Lima";
            secondBeneficiary.IndividualEmail = "CauaCosta@jourrapide.com";
            secondBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            secondBeneficiary.IndividualRG = "109014509";
            secondBeneficiary.IndividualBirthdate = new DateTime(1993, 4, 21);
            contract.Individuals.Add(secondBeneficiary);

            IndividualDomain thirdBeneficiary = new IndividualDomain();
            thirdBeneficiary.IndividualName = "Livia Pereira Costa";
            thirdBeneficiary.IndividualEmail = "LiviaPereira@dayrep.com";
            thirdBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            thirdBeneficiary.IndividualRG = "102653604";
            thirdBeneficiary.IndividualBirthdate = new DateTime(1957, 7, 24);
            contract.Individuals.Add(thirdBeneficiary);

            IndividualDomain fourthBeneficiary = new IndividualDomain();
            fourthBeneficiary.IndividualName = "Beatrice Fernandes Barbosa";
            fourthBeneficiary.IndividualEmail = "BeatriceFernandes@teleworm.us";
            fourthBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            fourthBeneficiary.IndividualRG = "484661632";
            fourthBeneficiary.IndividualBirthdate = new DateTime(1955, 9, 3);
            contract.Individuals.Add(fourthBeneficiary);

            IndividualDomain fifthBeneficiary = new IndividualDomain();
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
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.BeneficiaryId;

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
            contract.Pets = new List<PetDomain>();
            contract.ExpiryDate = new DateTime(2032, 1, 1);
            contract.IsActive = false;

            IndividualDomain contractHolder = new IndividualDomain();
            contractHolder.IndividualName = "Isabela Rocha Costa";
            contractHolder.IndividualEmail = "IsabelaRocha@rhyta.com";
            contractHolder.IndividualCPF = CpfGenerator.GenerateCpf();
            contractHolder.IndividualRG = "156188326";
            contractHolder.IndividualBirthdate = new DateTime(1953, 11, 4);

            PetDomain firstBeneficiary = new PetDomain();
            firstBeneficiary.PetName = "Joe";
            firstBeneficiary.PetBreed = "Mixed";
            firstBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            firstBeneficiary.PetBirthdate = new DateTime(2005, 7, 20);
            contract.Pets.Add(firstBeneficiary);

            PetDomain secondBeneficiary = new PetDomain();
            secondBeneficiary.PetName = "Lobo";
            secondBeneficiary.PetBreed = "Belgian Sheepdog";
            secondBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            secondBeneficiary.PetBirthdate = new DateTime(2010, 10, 25);
            contract.Pets.Add(secondBeneficiary);

            PetDomain thirdBeneficiary = new PetDomain();
            thirdBeneficiary.PetName = "Dasha";
            thirdBeneficiary.PetBreed = "Chihuahua";
            thirdBeneficiary.PetSpecies = PetSpecies.Canis_lupus_familiaris;
            thirdBeneficiary.PetBirthdate = new DateTime(2016, 4, 1);
            contract.Pets.Add(thirdBeneficiary);

            PetDomain fourthBeneficiary = new PetDomain();
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
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.BeneficiaryId;

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
            contract.MobileDevices = new List<MobileDeviceDomain>();
            contract.Type = (ContractType)6;
            contract.Category = (ContractCategory)2;
            contract.ExpiryDate = new DateTime(2019, 12, 30);
            contract.IsActive = true;

            IndividualDomain contractHolder = new IndividualDomain();
            contractHolder.IndividualName = "Eduardo Barbosa";
            contractHolder.IndividualEmail = "EduardoGoncalves@dayrep.com";
            contractHolder.IndividualCPF = CpfGenerator.GenerateCpf();
            contractHolder.IndividualRG = "240875278";
            contractHolder.IndividualBirthdate = new DateTime(1970, 8, 12);

            MobileDeviceDomain firstBeneficiary = new MobileDeviceDomain();
            firstBeneficiary.MobileDeviceType = MobileDeviceType.Smartphone;
            firstBeneficiary.MobileDeviceBrand = "Motorola";
            firstBeneficiary.MobileDeviceModel = "MotoZ";
            firstBeneficiary.MobileDeviceSerialNumber = "sjktZgP2";
            firstBeneficiary.MobileDeviceManufactoringYear = new DateTime(2017, 6, 25);
            firstBeneficiary.MobileDeviceInvoiceValue = 1099.90;
            contract.MobileDevices.Add(firstBeneficiary);

            MobileDeviceDomain secondBeneficiary = new MobileDeviceDomain();
            secondBeneficiary.MobileDeviceType = MobileDeviceType.Tablet;
            secondBeneficiary.MobileDeviceBrand = "Apple";
            secondBeneficiary.MobileDeviceModel = "IPad Pro";
            secondBeneficiary.MobileDeviceSerialNumber = "6HTCXhUT";
            secondBeneficiary.MobileDeviceManufactoringYear = new DateTime(2018, 9, 11);
            secondBeneficiary.MobileDeviceInvoiceValue = 4499.99;
            contract.MobileDevices.Add(secondBeneficiary);

            MobileDeviceDomain thirdBeneficiary = new MobileDeviceDomain();
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
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.BeneficiaryId;

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
            contract.Vehicles = new List<VehicleDomain>();
            contract.IsActive = true;

            IndividualDomain contractHolder = new IndividualDomain();
            contractHolder.IndividualName = "Felipe Araujo Souza";
            contractHolder.IndividualEmail = "FelipeAraujo@dayrep.com";
            contractHolder.IndividualCPF = CpfGenerator.GenerateCpf();
            contractHolder.IndividualRG = "463205522";
            contractHolder.IndividualBirthdate = new DateTime(1992, 11, 28);

            VehicleDomain firstBeneficiary = new VehicleDomain();
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

            VehicleDomain secondBeneficiary = new VehicleDomain();
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
            var contractHolderPostApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await contractHolderPost.Content.ReadAsStringAsync());
            contract.ContractHolderId = contractHolderPostApiResponse.BeneficiaryId;

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