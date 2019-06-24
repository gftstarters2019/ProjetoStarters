using Backend.Core.Models;
using Backend.IntegrationTests.ControllerTest;
using Backend.IntegrationTests.Helper;
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
            var apiResponse = JsonConvert.DeserializeObject<List<IndividualEntity>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanGetContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            IndividualEntity individual = new IndividualEntity();
            individual.IndividualName = "Emilly Rodrigues Cardoso";
            individual.IndividualEmail = "EmillyRodrigues@rhyta.com";
            individual.IndividualCPF = CpfGenerator.GenerateCpf();
            individual.IndividualRG = "405589219";
            individual.IndividualBirthdate = new DateTime(1948, 10, 6);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(postApiResponse);
            Assert.IsInstanceOf<IndividualEntity>(postApiResponse);
            Assert.AreEqual(postApiResponse.BeneficiaryId, getApiResponse.BeneficiaryId);
            Assert.AreEqual(individual.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, getApiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, getApiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanRequestAContractHolderObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            IndividualEntity individual = new IndividualEntity();
            individual.IndividualName = "Otávio Ribeiro Lima";
            individual.IndividualEmail = "OtavioRibeiroLima@rhyta.com";
            individual.IndividualCPF = CpfGenerator.GenerateCpf();
            individual.IndividualRG = "244025769";
            individual.IndividualBirthdate = new DateTime(1990, 10, 12);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await postResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var apiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<IndividualEntity>(apiResponse);
            Assert.AreEqual(individual.IndividualName, apiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, apiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, apiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, apiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, apiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanUpdateAContractHolderRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            IndividualEntity individual = new IndividualEntity();
            individual.IndividualName = "Samuel Cavalcanti Martins";
            individual.IndividualEmail = "SamuelCavalcantiMartins@rhyta.com";
            individual.IndividualCPF = CpfGenerator.GenerateCpf();
            individual.IndividualRG = "310291136";
            individual.IndividualBirthdate = new DateTime(1993, 2, 20);


            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await postResponse.Content.ReadAsStringAsync());

            postApiResponse.IndividualName = "Samuel Cavalcanti Martin";
            postApiResponse.IndividualEmail = "SamuelCavalcantiMartin@rhyta.com";
            postApiResponse.IndividualRG = "310291137";
            postApiResponse.IndividualBirthdate = new DateTime(1993, 2, 21);

            jsonContent = JsonConvert.SerializeObject(postApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.BeneficiaryId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<IndividualEntity>(getApiResponse);
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
            IndividualEntity individual = new IndividualEntity();
            individual.IndividualName = "Vinicius Araujo";
            individual.IndividualEmail = "ViniciusAraujo@dayrep.com"; 
            individual.IndividualCPF = CpfGenerator.GenerateCpf();
            individual.IndividualRG = "485936781";
            individual.IndividualBirthdate = new DateTime(1980, 6, 1);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{getApiResponse.BeneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<IndividualEntity>(await deleteResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<IndividualEntity>(deleteApiResponse);
            Assert.AreEqual(individual.IndividualName,  deleteApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, deleteApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, deleteApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, deleteApiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, deleteApiResponse.IndividualBirthdate);
        }
    }
}