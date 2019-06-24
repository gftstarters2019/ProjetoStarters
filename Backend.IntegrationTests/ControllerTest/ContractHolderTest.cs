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
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Emilly Rodrigues Cardoso";
            individual.individualEmail = "EmillyRodrigues@rhyta.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "405589219";
            individual.individualBirthdate = new DateTime(1948, 10, 6);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
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
            Assert.AreEqual(individual.individualName, getApiResponse.individualName);
            Assert.AreEqual(individual.individualEmail, getApiResponse.individualEmail);
            Assert.AreEqual(individual.individualCPF, getApiResponse.individualCPF);
            Assert.AreEqual(individual.individualRG, getApiResponse.individualRG);
            Assert.AreEqual(individual.individualBirthdate, getApiResponse.individualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanRequestAContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Otávio Ribeiro Lima";
            individual.individualEmail = "OtavioRibeiroLima@rhyta.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "244025769";
            individual.individualBirthdate = new DateTime(1990, 10, 12);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var apiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractHolderViewModel>(apiResponse);
            Assert.AreEqual(individual.individualName, apiResponse.individualName);
            Assert.AreEqual(individual.individualEmail, apiResponse.individualEmail);
            Assert.AreEqual(individual.individualCPF, apiResponse.individualCPF);
            Assert.AreEqual(individual.individualRG, apiResponse.individualRG);
            Assert.AreEqual(individual.individualBirthdate, apiResponse.individualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanUpdateAContractHolderRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Samuel Cavalcanti Martins";
            individual.individualEmail = "SamuelCavalcantiMartins@rhyta.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "310291136";
            individual.individualBirthdate = new DateTime(1993, 2, 20);


            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
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
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Vinicius Araujo";
            individual.individualEmail = "ViniciusAraujo@dayrep.com"; 
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "485936781";
            individual.individualBirthdate = new DateTime(1980, 6, 1);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
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
            Assert.AreEqual(individual.individualName,  deleteApiResponse.individualName);
            Assert.AreEqual(individual.individualEmail, deleteApiResponse.individualEmail);
            Assert.AreEqual(individual.individualCPF, deleteApiResponse.individualCPF);
            Assert.AreEqual(individual.individualRG, deleteApiResponse.individualRG);
            Assert.AreEqual(individual.individualBirthdate, deleteApiResponse.individualBirthdate);
        }
    }
}