using Backend.Core.Models;
using Backend.IntegrationTests;
using Backend.IntegrationTests.ControllerTest;
using ContractHolder.WebAPI;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
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
    }
}