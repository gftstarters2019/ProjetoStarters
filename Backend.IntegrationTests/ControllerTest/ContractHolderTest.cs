using Backend.Application.ViewModels;
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
    public class ContractHolderTest : BaseIntegrationTest
    {
        private const string url = "/api/ContractHolder";

        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContractHolders()
        {

            // Act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<List<ContractHolderViewModel>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanRequestOwnerObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Elwing Tolkien";
            individual.individualEmail = "elwing@email.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "405589219";
            individual.individualBirthdate = new DateTime(1980, 1, 18);

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
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanGetAnOwnerObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Feanor Elrond";
            individual.individualEmail = "feanor@email.com";
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
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanUpdateAnOwnerObjectAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Earendil Tolkien";
            individual.individualEmail = "earendil@email.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "310291136";
            individual.individualBirthdate = new DateTime(1993, 2, 20);


            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            postApiResponse.individualName = "Thorondor Tolkien";
            postApiResponse.individualEmail = "thorondor@gmail.com";
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "288715524";
            individual.individualBirthdate = new DateTime(2000, 3, 4);

            jsonContent = JsonConvert.SerializeObject(postApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.individualId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<ContractHolderViewModel>(getApiResponse);
            Assert.AreEqual(putApiResponse.individualName, getApiResponse.individualName);
            Assert.AreEqual(putApiResponse.individualEmail, getApiResponse.individualEmail);
            Assert.AreEqual(putApiResponse.individualCPF, getApiResponse.individualCPF);
            Assert.AreEqual(putApiResponse.individualRG, getApiResponse.individualRG);
            Assert.AreEqual(putApiResponse.individualBirthdate, getApiResponse.individualBirthdate);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPostAndGet_ThenICanDeleteAnOwnerObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel individual = new ContractHolderViewModel();
            individual.individualName = "Manwe Tolkien";
            individual.individualEmail = "manwe@email.com"; 
            individual.individualCPF = CpfGenerator.GenerateCpf();
            individual.individualRG = "485936781";
            individual.individualBirthdate = new DateTime(1978, 6, 1);

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