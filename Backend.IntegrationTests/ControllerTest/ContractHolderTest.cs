using Backend.Core.Models;
using Backend.IntegrationTests;
using Backend.IntegrationTests.ControllerTest;
using ContractHolder.WebAPI;
using Newtonsoft.Json;
using NUnit.Framework;
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
            individual.IndividualCPF = "123456789";
            individual.IndividualRG = "123456789";

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.IndividualId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(postApiResponse);
            Assert.IsInstanceOf<Individual>(postApiResponse);
            Assert.AreEqual(postApiResponse.IndividualId, getApiResponse.IndividualId);
            Assert.AreEqual(individual.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, getApiResponse.IndividualRG);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPost_ThenICanGetAnOwnerObjectById()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Individual individual = new Individual();
            individual.IndividualName = "Feanor";
            individual.IndividualEmail = "feanor@email.com";
            individual.IndividualCPF = "9876543210";
            individual.IndividualRG = "9876543210";

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync($"{url}/{postApiResponse.IndividualId}");
            var apiResponse = JsonConvert.DeserializeObject<Individual>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(apiResponse);
            
            Assert.AreEqual(individual.IndividualName, apiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, apiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, apiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, apiResponse.IndividualRG);
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

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.IndividualId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<Individual>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.IndividualId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(getApiResponse);
            Assert.AreEqual(putApiResponse.IndividualName, getApiResponse.IndividualName);
            Assert.AreEqual(putApiResponse.IndividualEmail, getApiResponse.IndividualEmail);
            Assert.AreEqual(putApiResponse.IndividualCPF, getApiResponse.IndividualCPF);
            Assert.AreEqual(putApiResponse.IndividualRG, getApiResponse.IndividualRG);
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

            //act
            var jsonContent = JsonConvert.SerializeObject(individual);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<Individual>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.IndividualId}");
            var getApiResponse = JsonConvert.DeserializeObject<Individual>(await getResponse.Content.ReadAsStringAsync());

            var deleteResponse = await client.DeleteAsync($"{url}/{getApiResponse.IndividualId}");
            var deleteApiResponse = JsonConvert.DeserializeObject<Individual>(await deleteResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsInstanceOf<Individual>(deleteApiResponse);
            Assert.AreEqual(individual.IndividualName,  deleteApiResponse.IndividualName);
            Assert.AreEqual(individual.IndividualEmail, deleteApiResponse.IndividualEmail);
            Assert.AreEqual(individual.IndividualCPF, deleteApiResponse.IndividualCPF);
            Assert.AreEqual(individual.IndividualRG, deleteApiResponse.IndividualRG);
        }
    }
}