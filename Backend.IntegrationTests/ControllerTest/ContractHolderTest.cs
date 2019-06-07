using Backend.Core.Models;
using Backend.IntegrationTests;
using Backend.IntegrationTests.ControllerTest;
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
            var apiResponse = JsonConvert.DeserializeObject<List<Individual>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanRequestOwnerObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Individual individual = new Individual();
            individual.IndividualName = "Elwing";
            individual.IndividualEmail = "elwing@email.com";
            individual.IndividualCPF = "36449769025";
            individual.IndividualRG = "405589219";
            individual.IndividualBirthdate = new DateTime(2017, 1, 18);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(postApiResponse);
            Assert.IsInstanceOf<Individual>(postApiResponse);
            //Assert.AreEqual(postApiResponse.BeneficiaryId, getApiResponse.BeneficiaryId);
            Assert.AreEqual(individual.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, getApiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, getApiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanGetAnOwnerObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Individual individual = new Individual();
            individual.IndividualName = "Feanor";
            individual.IndividualEmail = "feanor@email.com";
            individual.IndividualCPF = "58302207098";
            individual.IndividualRG = "244025769";
            individual.IndividualBirthdate = new DateTime(2017, 1, 18);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var apiResponse = JsonConvert.DeserializeObject<Individual>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(apiResponse);
            
            Assert.AreEqual(individual.IndividualName, apiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, apiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, apiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, apiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, apiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanUpdateAnOwnerObjectAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Individual individual = new Individual();
            individual.IndividualName = "Earendil";
            individual.IndividualEmail = "earendil@email.com";
            individual.IndividualCPF = "35895879039";
            individual.IndividualRG = "310291136";
            individual.IndividualBirthdate = new DateTime(2017, 1, 18);


            //act

            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            postApiResponse.IndividualName = "Thorondor";
            postApiResponse.IndividualEmail = "thorondor@gmail.com";
            individual.IndividualCPF = "29823232067";
            individual.IndividualRG = "288715524";

            jsonContent = JsonConvert.SerializeObject(postApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.BeneficiaryId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<Individual>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(getApiResponse);
            Assert.AreEqual(putApiResponse.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(putApiResponse.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(putApiResponse.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(putApiResponse.IndividualRG, getApiResponse.IndividualRG);
            Assert.AreEqual(putApiResponse.IndividualBirthdate, getApiResponse.IndividualBirthdate);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPostAndGet_ThenICanDeleteAnOwnerObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Individual individual = new Individual();
            individual.IndividualName = "Manwe";
            individual.IndividualEmail = "manwe@email.com";
            individual.IndividualCPF = "83094604064";
            individual.IndividualRG = "485936781";
            individual.IndividualBirthdate = new DateTime(2017, 1, 18);

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.BeneficiaryId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{getApiResponse.BeneficiaryId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<Individual>(await deleteResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(deleteApiResponse);
            Assert.AreEqual(individual.IndividualName,  deleteApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, deleteApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, deleteApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, deleteApiResponse.IndividualRG);
            Assert.AreEqual(individual.IndividualBirthdate, deleteApiResponse.IndividualBirthdate);
        }
    }
}