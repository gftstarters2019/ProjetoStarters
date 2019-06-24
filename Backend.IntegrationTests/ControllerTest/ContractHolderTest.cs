using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.IntegrationTests.ControllerTest;
using Backend.IntegrationTests.Helper;
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
    public class ContractHolderTest : BaseIntegrationTest
    {
        private const string url = "/api/ContractHolder";

        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContractHolders()
        {

            // act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<List<ContractHolderViewModel>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanGetContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Emilly Rodrigues Cardoso";
            contractHolder.individualEmail = "EmillyRodrigues@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "405589219";
            contractHolder.individualBirthdate = new DateTime(1948, 10, 6);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(postApiResponse);
            Assert.IsInstanceOf<ContractHolderViewModel>(postApiResponse);
            Assert.AreEqual(postApiResponse.individualId, getApiResponse.individualId);
            Assert.AreEqual(contractHolder.individualName, getApiResponse.individualName);
            Assert.AreEqual(contractHolder.individualEmail, getApiResponse.individualEmail);
            Assert.AreEqual(contractHolder.individualCPF, getApiResponse.individualCPF);
            Assert.AreEqual(contractHolder.individualRG, getApiResponse.individualRG);
            Assert.AreEqual(contractHolder.individualBirthdate, getApiResponse.individualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanRequestAContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Otávio Ribeiro Lima";
            contractHolder.individualEmail = "OtavioRibeiroLima@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "244025769";
            contractHolder.individualBirthdate = new DateTime(1990, 10, 12);
            contractHolder.individualAddresses = new List<AddressDomain>();
            contractHolder.individualTelephones = new List<TelephoneDomain>();

            AddressDomain address = new AddressDomain();
            address.AddressStreet = "Rua Gonçalves";
            address.AddressNumber = "542";
            address.AddressNeighborhood = "Jardim Pereira";
            address.AddressComplement = "A";
            address.AddressZipCode = "18067098";
            address.AddressCity = "Sorocaba";
            address.AddressState = "SP";
            address.AddressCountry = "Brasil";
            address.AddressType = AddressType.Home;
            contractHolder.individualAddresses.Add(address);

            TelephoneDomain telephone = new TelephoneDomain();
            telephone.TelephoneNumber = "1534158971";
            telephone.TelephoneType = TelephoneType.Home;
            contractHolder.individualTelephones.Add(telephone);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var apiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractHolderViewModel>(apiResponse);
            Assert.AreEqual(contractHolder.individualName, apiResponse.individualName);
            Assert.AreEqual(contractHolder.individualEmail, apiResponse.individualEmail);
            Assert.AreEqual(contractHolder.individualCPF, apiResponse.individualCPF);
            Assert.AreEqual(contractHolder.individualRG, apiResponse.individualRG);
            Assert.AreEqual(contractHolder.individualBirthdate, apiResponse.individualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanUpdateAContractHolderRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Samuel Cavalcanti Martins";
            contractHolder.individualEmail = "SamuelCavalcantiMartins@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "310291136";
            contractHolder.individualBirthdate = new DateTime(1993, 2, 20);


            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            postApiResponse.individualName = "Samuel Cavalcanti Martin";
            postApiResponse.individualEmail = "SamuelCavalcantiMartin@rhyta.com";
            postApiResponse.individualRG = "310291137";
            postApiResponse.individualBirthdate = new DateTime(1993, 2, 21);

            jsonContent = JsonConvert.SerializeObject(postApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.individualId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractHolderViewModel>(getApiResponse);
            Assert.AreEqual(putApiResponse.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(putApiResponse.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(putApiResponse.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(putApiResponse.IndividualRG, getApiResponse.IndividualRG);
            Assert.AreEqual(putApiResponse.IndividualBirthdate, getApiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPostAndGet_ThenICanDeleteAContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Vinicius Araujo";
            contractHolder.individualEmail = "ViniciusAraujo@dayrep.com"; 
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "485936781";
            contractHolder.individualBirthdate = new DateTime(1980, 6, 1);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{getApiResponse.individualId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await deleteResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractHolderViewModel>(deleteApiResponse);
            Assert.AreEqual(contractHolder.individualName,  deleteApiResponse.individualName);
            Assert.AreEqual(contractHolder.individualEmail, deleteApiResponse.individualEmail);
            Assert.AreEqual(contractHolder.individualCPF, deleteApiResponse.individualCPF);
            Assert.AreEqual(contractHolder.individualRG, deleteApiResponse.individualRG);
            Assert.AreEqual(contractHolder.individualBirthdate, deleteApiResponse.individualBirthdate);
        }
    }
}