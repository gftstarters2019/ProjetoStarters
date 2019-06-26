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
    public class BeneficiaryTest : BaseIntegrationTest
    {
        private const string urlContract = "/api/Contract";
        private const string urlContractHolder = "/api/ContractHolder";
        private const string url = "/api/Beneficiary";

        #region Get All

        [Test]
        public async Task WhenRequestListOfIndividuals_ThenIShouldReceiveIndividuals()
        {
            // act
            var response = await client.GetAsync($"{url}/Individuals");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<IndividualDomain>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestListOfPets_ThenIShouldReceiveIndividuals()
        {
            // act
            var response = await client.GetAsync($"{url}/Pets");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<PetDomain>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestListOfRealties_ThenIShouldReceiveIndividuals()
        {
            // act
            var response = await client.GetAsync($"{url}/Realties");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<RealtyDomain>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestListOfVehicles_ThenIShouldReceiveIndividuals()
        {
            // act
            var response = await client.GetAsync($"{url}/Vehicles");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<VehicleDomain>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestListOfMobileDevices_ThenIShouldReceiveIndividuals()
        {
            // act
            var response = await client.GetAsync($"{url}/MobileDevices");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<MobileDeviceDomain>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }
        #endregion

        #region Get By Id
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfIndividuals_ThenICanGetIndividualObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.DentalPlan;
            contract.Category = ContractCategory.Silver;
            contract.Individuals = new List<IndividualDomain>();
            contract.ExpiryDate = new DateTime(2023, 1, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Lucas Souza Barbosa";
            contractHolder.individualEmail = "LucasSouza@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "220479239";
            contractHolder.individualBirthdate = new DateTime(1973, 5, 14);

            IndividualDomain firstBeneficiary = new IndividualDomain();
            firstBeneficiary.IndividualName = "Marisa Cunha Barros";
            firstBeneficiary.IndividualEmail = "MarisaCunha@jourrapide.com";
            firstBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            firstBeneficiary.IndividualRG = "296305881";
            firstBeneficiary.IndividualBirthdate = new DateTime(1962, 2, 28);
            contract.Individuals.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<IndividualDomain>(getApiResponse);
            Assert.AreEqual(firstBeneficiary.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(firstBeneficiary.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(firstBeneficiary.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(firstBeneficiary.IndividualRG, getApiResponse.IndividualRG);
            Assert.AreEqual(firstBeneficiary.IndividualBirthdate, getApiResponse.IndividualBirthdate);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
            var deleteBeneficiary = await client.DeleteAsync($"{url}/{beneficiaryId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfPets_ThenICanGetPetObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.AnimalHealthPlan;
            contract.Category = ContractCategory.Iron;
            contract.Pets = new List<PetDomain>();
            contract.ExpiryDate = new DateTime(2019, 1, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Vitor Barros Gomes";
            contractHolder.individualEmail = "VitorBarros@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "243708014";
            contractHolder.individualBirthdate = new DateTime(1972, 1, 18);

            PetDomain firstBeneficiary = new PetDomain();
            firstBeneficiary.PetName = "Astro";
            firstBeneficiary.PetBreed = "Calopsita";
            firstBeneficiary.PetSpecies = PetSpecies.Nymphicus_hollandicus;
            firstBeneficiary.PetBirthdate = new DateTime(2010, 9, 10);
            contract.Pets.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<PetDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<PetDomain>(getApiResponse);
            Assert.AreEqual(firstBeneficiary.PetName, getApiResponse.PetName);
            Assert.AreEqual(firstBeneficiary.PetBreed, getApiResponse.PetBreed);
            Assert.AreEqual(firstBeneficiary.PetSpecies, getApiResponse.PetSpecies);
            Assert.AreEqual(firstBeneficiary.PetBirthdate, getApiResponse.PetBirthdate);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
            var deleteBeneficiary = await client.DeleteAsync($"{url}/{beneficiaryId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfMobileDevices_ThenICanGetMobileDeviceObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.MobileDeviceInsurance;
            contract.Category = ContractCategory.Gold;
            contract.MobileDevices = new List<MobileDeviceDomain>();
            contract.ExpiryDate = new DateTime(2021, 5, 4);
            contract.IsActive = false;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Thiago Cunha Correia";
            contractHolder.individualEmail = "ThiagoCunha@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "442870954";
            contractHolder.individualBirthdate = new DateTime(1962, 2, 20);

            MobileDeviceDomain firstBeneficiary = new MobileDeviceDomain();
            firstBeneficiary.MobileDeviceType = MobileDeviceType.Smartphone;
            firstBeneficiary.MobileDeviceBrand = "Nokia";
            firstBeneficiary.MobileDeviceModel = "SZX";
            firstBeneficiary.MobileDeviceSerialNumber = AlphanumericGenerator.RandomString(8);
            firstBeneficiary.MobileDeviceManufactoringYear = new DateTime(2004, 1, 3);
            firstBeneficiary.MobileDeviceInvoiceValue = 1099.90;
            contract.MobileDevices.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<MobileDeviceDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<MobileDeviceDomain>(getApiResponse);
            Assert.AreEqual(firstBeneficiary.MobileDeviceType, getApiResponse.MobileDeviceType);
            Assert.AreEqual(firstBeneficiary.MobileDeviceBrand, getApiResponse.MobileDeviceBrand);
            Assert.AreEqual(firstBeneficiary.MobileDeviceModel, getApiResponse.MobileDeviceModel);
            Assert.AreEqual(firstBeneficiary.MobileDeviceSerialNumber, getApiResponse.MobileDeviceSerialNumber);
            Assert.AreEqual(firstBeneficiary.MobileDeviceManufactoringYear, getApiResponse.MobileDeviceManufactoringYear);
            Assert.AreEqual(firstBeneficiary.MobileDeviceInvoiceValue, getApiResponse.MobileDeviceInvoiceValue);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
            var deleteBeneficiary = await client.DeleteAsync($"{url}/{beneficiaryId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfVehicles_ThenICanGetVehicleObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.VehicleInsurance;
            contract.Category = ContractCategory.Bronze;
            contract.Vehicles = new List<VehicleDomain>();
            contract.ExpiryDate = new DateTime(2022, 5, 4);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Sophia Lima Barros";
            contractHolder.individualEmail = "SophiaLima@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "338794335";
            contractHolder.individualBirthdate = new DateTime(1996, 6, 11);

            VehicleDomain firstBeneficiary = new VehicleDomain();
            firstBeneficiary.VehicleBrand = "Ford";
            firstBeneficiary.VehicleModel = "Ka";
            firstBeneficiary.VehicleChassisNumber = AlphanumericGenerator.RandomString(7);
            firstBeneficiary.VehicleColor = Color.Red;
            firstBeneficiary.VehicleCurrentFipeValue = 15000.00;
            firstBeneficiary.VehicleCurrentMileage = 200000;
            firstBeneficiary.VehicleDoneInspection = true;
            firstBeneficiary.VehicleManufactoringYear = new DateTime(2000, 1, 1);
            firstBeneficiary.VehicleModelYear = new DateTime(2000, 1, 1);
            contract.Vehicles.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<VehicleDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<VehicleDomain>(getApiResponse);
            Assert.AreEqual(firstBeneficiary.VehicleBrand, getApiResponse.VehicleBrand);
            Assert.AreEqual(firstBeneficiary.VehicleModel, getApiResponse.VehicleModel);
            Assert.AreEqual(firstBeneficiary.VehicleChassisNumber, getApiResponse.VehicleChassisNumber);
            Assert.AreEqual(firstBeneficiary.VehicleColor, getApiResponse.VehicleColor);
            Assert.AreEqual(firstBeneficiary.VehicleCurrentFipeValue, getApiResponse.VehicleCurrentFipeValue);
            Assert.AreEqual(firstBeneficiary.VehicleCurrentMileage, getApiResponse.VehicleCurrentMileage);
            Assert.AreEqual(firstBeneficiary.VehicleDoneInspection, getApiResponse.VehicleDoneInspection);
            Assert.AreEqual(firstBeneficiary.VehicleManufactoringYear, getApiResponse.VehicleManufactoringYear);
            Assert.AreEqual(firstBeneficiary.VehicleModelYear, getApiResponse.VehicleModelYear);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
            var deleteBeneficiary = await client.DeleteAsync($"{url}/{beneficiaryId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfRealties_ThenICanGetRealtyObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.RealStateInsurance;
            contract.Category = ContractCategory.Gold;
            contract.Realties = new List<RealtyViewModel>();
            contract.ExpiryDate = new DateTime(2022, 5, 4);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Sophia Lima Barros";
            contractHolder.individualEmail = "SophiaLima@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "338794335";
            contractHolder.individualBirthdate = new DateTime(1996, 6, 11);

            RealtyViewModel firstBeneficiary = new RealtyViewModel();
            firstBeneficiary.MunicipalRegistration = AlphanumericGenerator.RandomString(10);
            firstBeneficiary.MarketValue = 200000;
            firstBeneficiary.SaleValue = 199000.54;
            firstBeneficiary.ConstructionDate = new DateTime(1983, 1, 30);
            firstBeneficiary.AddressStreet = "Rua Recife";
            firstBeneficiary.AddressNumber = "955";
            firstBeneficiary.AddressNeighborhood = "Jardim São Miguel";
            firstBeneficiary.AddressZipCode = "78110788";
            firstBeneficiary.AddressCity = "Várzea Grande";
            firstBeneficiary.AddressState = "MT";
            firstBeneficiary.AddressCountry = "Brasil";
            firstBeneficiary.AddressType = AddressType.Home;
            contract.Realties.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<RealtyViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<RealtyViewModel>(getApiResponse);
            Assert.AreEqual(firstBeneficiary.MunicipalRegistration, getApiResponse.MunicipalRegistration);
            Assert.AreEqual(firstBeneficiary.MarketValue, getApiResponse.MarketValue);
            Assert.AreEqual(firstBeneficiary.SaleValue, getApiResponse.SaleValue);
            Assert.AreEqual(firstBeneficiary.ConstructionDate, getApiResponse.ConstructionDate);
            Assert.AreEqual(firstBeneficiary.AddressStreet, getApiResponse.AddressStreet);
            Assert.AreEqual(firstBeneficiary.AddressNumber, getApiResponse.AddressNumber);
            Assert.AreEqual(firstBeneficiary.AddressNeighborhood, getApiResponse.AddressNeighborhood);
            Assert.AreEqual(firstBeneficiary.AddressZipCode, getApiResponse.AddressZipCode);
            Assert.AreEqual(firstBeneficiary.AddressCity, getApiResponse.AddressCity);
            Assert.AreEqual(firstBeneficiary.AddressState, getApiResponse.AddressState);
            Assert.AreEqual(firstBeneficiary.AddressCountry, getApiResponse.AddressCountry);
            Assert.AreEqual(firstBeneficiary.AddressType, getApiResponse.AddressType);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
            var deleteBeneficiary = await client.DeleteAsync($"{url}/{beneficiaryId}");
        }
        #endregion

        #region Delete
        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfIndividuals_ThenICanDeleteIndividualObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.LifeInsurance;
            contract.Category = ContractCategory.Silver;
            contract.Individuals = new List<IndividualDomain>();
            contract.ExpiryDate = new DateTime(2040, 1, 1);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Isabela Castro Rocha";
            contractHolder.individualEmail = "IsabelaCastro@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "475121788";
            contractHolder.individualBirthdate = new DateTime(1973, 5, 14);

            IndividualDomain firstBeneficiary = new IndividualDomain();
            firstBeneficiary.IndividualName = "Marisa Cunha Barros";
            firstBeneficiary.IndividualEmail = "MarisaCunha@jourrapide.com";
            firstBeneficiary.IndividualCPF = CpfGenerator.GenerateCpf();
            firstBeneficiary.IndividualRG = "296305881";
            firstBeneficiary.IndividualBirthdate = new DateTime(1988, 4, 11);
            contract.Individuals.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{beneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<IndividualDomain>(await getResponse.Content.ReadAsStringAsync());
            
            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<IndividualDomain>(getApiResponse);
            Assert.AreEqual(deleteApiResponse.IsDeleted, true);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfPets_ThenICanDeletePetObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.AnimalHealthPlan;
            contract.Category = ContractCategory.Silver;
            contract.Pets = new List<PetDomain>();
            contract.ExpiryDate = new DateTime(2040, 1, 1);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Estevan Melo Silva";
            contractHolder.individualEmail = "EstevanMelo@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "475630415";
            contractHolder.individualBirthdate = new DateTime(1951, 9, 9);

            PetDomain firstBeneficiary = new PetDomain();
            firstBeneficiary.PetName = "Brutus";
            firstBeneficiary.PetBreed = "Arara";
            firstBeneficiary.PetSpecies = PetSpecies.Ara_chloropterus;
            firstBeneficiary.PetBirthdate = new DateTime(1993, 1, 1);
            contract.Pets.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<PetDomain>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{beneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<PetDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<PetDomain>(getApiResponse);
            Assert.AreEqual(deleteApiResponse.IsDeleted, true);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfMobileDevices_ThenICanDeleteMobileDeviceObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.MobileDeviceInsurance;
            contract.Category = ContractCategory.Silver;
            contract.MobileDevices = new List<MobileDeviceDomain>();
            contract.ExpiryDate = new DateTime(2020, 1, 1);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Matilde Ferreira Carvalho";
            contractHolder.individualEmail = "MatildeFerreira@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "492935749";
            contractHolder.individualBirthdate = new DateTime(1960, 10, 26);

            MobileDeviceDomain firstBeneficiary = new MobileDeviceDomain();
            firstBeneficiary.MobileDeviceType = MobileDeviceType.Laptop;
            firstBeneficiary.MobileDeviceBrand = "Dell";
            firstBeneficiary.MobileDeviceModel = "X1";
            firstBeneficiary.MobileDeviceSerialNumber = AlphanumericGenerator.RandomString(8);
            firstBeneficiary.MobileDeviceManufactoringYear = new DateTime(2019, 1, 1);
            firstBeneficiary.MobileDeviceInvoiceValue = 1400.99;
            contract.MobileDevices.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<MobileDeviceDomain>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{beneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<MobileDeviceDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<MobileDeviceDomain>(getApiResponse);
            Assert.AreEqual(deleteApiResponse.IsDeleted, true);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }

        [Test]
        public async Task WhenRequestContractControllerUsingPostSendingAListOfVehicles_ThenICanDeleteVehicleObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.VehicleInsurance;
            contract.Category = ContractCategory.Diamond;
            contract.Vehicles = new List<VehicleDomain>();
            contract.ExpiryDate = new DateTime(2023, 1, 1);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Melissa Barros Rocha";
            contractHolder.individualEmail = "Melissa Barros Rocha@dayrep.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "452978415";
            contractHolder.individualBirthdate = new DateTime(1991, 6, 5);

            VehicleDomain firstBeneficiary = new VehicleDomain();
            firstBeneficiary.VehicleBrand = "Chevrolet";
            firstBeneficiary.VehicleModel = "Camaro";
            firstBeneficiary.VehicleChassisNumber = AlphanumericGenerator.RandomString(7);
            firstBeneficiary.VehicleColor = Color.Red;
            firstBeneficiary.VehicleCurrentFipeValue = 328000.00;
            firstBeneficiary.VehicleCurrentMileage = 100;
            firstBeneficiary.VehicleDoneInspection = true;
            firstBeneficiary.VehicleManufactoringYear = new DateTime(2018, 1, 1);
            firstBeneficiary.VehicleModelYear = new DateTime(2018, 1, 1);
            contract.Vehicles.Add(firstBeneficiary);

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

            var beneficiaryId = contractPostApiResponse.Individuals[0].BeneficiaryId;

            var getResponse = await client.GetAsync($"{url}/{beneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<VehicleDomain>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{beneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<VehicleDomain>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<VehicleDomain>(getApiResponse);
            Assert.AreEqual(deleteApiResponse.IsDeleted, true);

            //cleaning
            var deleteContract = await client.DeleteAsync($"{url}/{contractPostApiResponse.SignedContractId}");
            var deleteContractHolder = await client.DeleteAsync($"{urlContractHolder}/{contractHolderPostApiResponse.individualId}");
        }
        #endregion
    }
}