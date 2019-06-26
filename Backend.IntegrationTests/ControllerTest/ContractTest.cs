using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.IntegrationTests.ControllerTest;
using Backend.IntegrationTests.Helper;
using Contract.WebAPI.ViewModels;
using ContractHolder.WebAPI.ViewModels;
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

        #region Get All
        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContracts()
        {
            // act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<ContractViewModel>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }
        #endregion

        #region Get By Id
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfIndividuals_ThenICanGetContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.DentalPlan;
            contract.Category = ContractCategory.Silver;
            contract.Individuals = new List<IndividualDomain>();
            contract.Realties = new List<RealtyViewModel>();
            contract.ExpiryDate = new DateTime(2020, 10, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Caio Silva Ferreira";
            contractHolder.individualEmail = "CaioSilva@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "335373793";
            contractHolder.individualBirthdate = new DateTime(1966, 12, 28);

            IndividualDomain firstBeneficiary = new IndividualDomain();
            firstBeneficiary.IndividualName = "Bianca Correia Fernandes";
            firstBeneficiary.IndividualEmail = "BiancaCorreia@teleworm.com";
            firstBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            firstBeneficiary.IndividualRG = "335838108";
            firstBeneficiary.IndividualBirthdate = new DateTime(1978, 6, 19);
            contract.Individuals.Add(firstBeneficiary);

            IndividualDomain secondBeneficiary = new IndividualDomain();
            secondBeneficiary.IndividualName = "Cauã Costa Lima";
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
            fifthBeneficiary.IndividualName = "Júlio Souza Barros";
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

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{getApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }
        #endregion

        #region Post
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfPets_ThenICanRequestContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.AnimalHealthPlan;
            contract.Category = ContractCategory.Gold;
            contract.Pets = new List<PetDomain>();
            contract.Realties = new List<RealtyViewModel>();
            contract.ExpiryDate = new DateTime(2032, 1, 1);
            contract.IsActive = false;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Isabela Rocha Costa";
            contractHolder.individualEmail = "IsabelaRocha@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "156188326";
            contractHolder.individualBirthdate = new DateTime(1953, 11, 4);

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
            thirdBeneficiary.PetBreed = "Siamese";
            thirdBeneficiary.PetSpecies = PetSpecies.Felis_catus;
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
            Assert.IsNotNull(contractPostApiResponse);
            Assert.IsInstanceOf<ContractViewModel>(contractPostApiResponse);
            Assert.AreEqual(contract.ContractHolderId, getApiResponse.ContractHolderId);
            Assert.AreEqual(contract.ExpiryDate, getApiResponse.ExpiryDate);
            Assert.AreEqual(contract.Type, getApiResponse.Type);
            Assert.AreEqual(contract.Category, getApiResponse.Category);
            Assert.AreEqual(contract.IsActive, getApiResponse.IsActive);
            Assert.AreEqual(contractPostApiResponse.SignedContractId, getApiResponse.SignedContractId);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{getApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfRealties_ThenICanGetContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.RealStateInsurance;
            contract.Category = ContractCategory.Silver;
            contract.Realties = new List<RealtyViewModel>();
            contract.ExpiryDate = new DateTime(2020, 10, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Julia Santos Souza";
            contractHolder.individualEmail = "JuliaSantos@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "474475906";
            contractHolder.individualBirthdate = new DateTime(1978, 5, 11);

            RealtyViewModel firstBeneficiary = new RealtyViewModel();
            firstBeneficiary.MunicipalRegistration = AlphanumericGenerator.RandomString(10);
            firstBeneficiary.MarketValue = 120000;
            firstBeneficiary.SaleValue = 116000.54;
            firstBeneficiary.ConstructionDate = new DateTime(1980, 10, 11);
            firstBeneficiary.AddressStreet = "Rua Ermando Matrângulo";
            firstBeneficiary.AddressNumber = "835";
            firstBeneficiary.AddressNeighborhood = "Jardim Athenas";
            firstBeneficiary.AddressZipCode = "14161010";
            firstBeneficiary.AddressCity = "Sertãozinho";
            firstBeneficiary.AddressState = "SP";
            firstBeneficiary.AddressCountry = "Brasil";
            firstBeneficiary.AddressType = AddressType.Commercial;
            contract.Realties.Add(firstBeneficiary);

            RealtyViewModel secondBeneficiary = new RealtyViewModel();
            secondBeneficiary.MunicipalRegistration = AlphanumericGenerator.RandomString(10);
            secondBeneficiary.MarketValue = 586000;
            secondBeneficiary.SaleValue = 735000.99;
            secondBeneficiary.ConstructionDate = new DateTime(2016, 1, 29);
            secondBeneficiary.AddressStreet = "Rua Almir Celso";
            secondBeneficiary.AddressNumber = "383";
            secondBeneficiary.AddressNeighborhood = "Jangurussu";
            secondBeneficiary.AddressZipCode = "60866665";
            secondBeneficiary.AddressCity = "Fortaleza";
            secondBeneficiary.AddressState = "CE";
            secondBeneficiary.AddressCountry = "Brasil";
            secondBeneficiary.AddressType = AddressType.Home;
            contract.Realties.Add(secondBeneficiary);

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

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{getApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }
        #endregion

        #region Update
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfMobileDevices_ThenICanUpdateAContractRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.MobileDevices = new List<MobileDeviceDomain>();
            contract.Type = ContractType.MobileDeviceInsurance; //(ContractType)6;
            contract.Category = ContractCategory.Silver; //(ContractCategory)2;
            contract.Realties = new List<RealtyViewModel>();
            contract.ExpiryDate = new DateTime(2019, 12, 30);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Eduardo Barbosa";
            contractHolder.individualEmail = "EduardoGoncalves@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "240875278";
            contractHolder.individualBirthdate = new DateTime(1970, 8, 12);

            MobileDeviceDomain firstBeneficiary = new MobileDeviceDomain();
            firstBeneficiary.MobileDeviceType = MobileDeviceType.Smartphone;
            firstBeneficiary.MobileDeviceBrand = "Motorola";
            firstBeneficiary.MobileDeviceModel = "MotoZ";
            firstBeneficiary.MobileDeviceSerialNumber = AlphanumericGenerator.RandomString(8);
            firstBeneficiary.MobileDeviceManufactoringYear = new DateTime(2017, 6, 25);
            firstBeneficiary.MobileDeviceInvoiceValue = 1099.90;
            contract.MobileDevices.Add(firstBeneficiary);

            MobileDeviceDomain secondBeneficiary = new MobileDeviceDomain();
            secondBeneficiary.MobileDeviceType = MobileDeviceType.Tablet;
            secondBeneficiary.MobileDeviceBrand = "Apple";
            secondBeneficiary.MobileDeviceModel = "IPad Pro";
            secondBeneficiary.MobileDeviceSerialNumber = AlphanumericGenerator.RandomString(8);
            secondBeneficiary.MobileDeviceManufactoringYear = new DateTime(2018, 9, 11);
            secondBeneficiary.MobileDeviceInvoiceValue = 4499.99;
            contract.MobileDevices.Add(secondBeneficiary);

            MobileDeviceDomain thirdBeneficiary = new MobileDeviceDomain();
            thirdBeneficiary.MobileDeviceType = MobileDeviceType.Laptop;
            thirdBeneficiary.MobileDeviceBrand = "Asus";
            thirdBeneficiary.MobileDeviceModel = "Pro SX";
            thirdBeneficiary.MobileDeviceSerialNumber = AlphanumericGenerator.RandomString(8);
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
            Assert.IsNotNull(putApiResponse);
            Assert.IsInstanceOf<ContractViewModel>(putApiResponse);
            Assert.AreEqual(putApiResponse.ExpiryDate, getApiResponse.ExpiryDate);
            Assert.AreEqual(putApiResponse.IsActive, getApiResponse.IsActive);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{getApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }
        #endregion

        #region Delete
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfVehicles_ThenICanDeleteAContractObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.VehicleInsurance;
            contract.Category = ContractCategory.Diamond;
            contract.ExpiryDate = new DateTime(2021, 2, 1);
            contract.Vehicles = new List<VehicleDomain>();
            contract.Realties = new List<RealtyViewModel>();
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Felipe Araujo Souza";
            contractHolder.individualEmail = "FelipeAraujo@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "463205522";
            contractHolder.individualBirthdate = new DateTime(1992, 11, 28);

            VehicleDomain firstBeneficiary = new VehicleDomain();
            firstBeneficiary.VehicleBrand = "Daewoo";
            firstBeneficiary.VehicleModel = "Leganza";
            firstBeneficiary.VehicleChassisNumber = AlphanumericGenerator.RandomString(7);
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
            secondBeneficiary.VehicleChassisNumber = AlphanumericGenerator.RandomString(7);
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
            Assert.IsNotNull(deleteApiResponse);
            Assert.IsInstanceOf<ContractViewModel>(deleteApiResponse);
            Assert.AreEqual(contract.Category,  deleteApiResponse.Category);
            Assert.AreEqual(contract.ExpiryDate, deleteApiResponse.ExpiryDate);
            Assert.AreEqual(contract.ContractHolderId, deleteApiResponse.ContractHolderId);
            Assert.AreEqual(contract.Type, deleteApiResponse.Type);

            //cleaning
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }
        #endregion
    }
}