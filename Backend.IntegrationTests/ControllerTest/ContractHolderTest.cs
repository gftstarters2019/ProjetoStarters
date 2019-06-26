using Backend.Core.Domains;
using Backend.Core.Enums;
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

        #region Get All
        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContractHolders()
        {
            // act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<List<ContractHolderViewModel>>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.IsNotNull(apiResponse);
        }
        #endregion

        #region Get By Id
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
            Assert.IsNotNull(getApiResponse);
            Assert.IsInstanceOf<ContractHolderViewModel>(getApiResponse);
            Assert.AreEqual(postApiResponse.individualId, getApiResponse.individualId);
            Assert.AreEqual(contractHolder.individualName, getApiResponse.individualName);
            Assert.AreEqual(contractHolder.individualEmail, getApiResponse.individualEmail);
            Assert.AreEqual(contractHolder.individualCPF, getApiResponse.individualCPF);
            Assert.AreEqual(contractHolder.individualRG, getApiResponse.individualRG);
            Assert.AreEqual(contractHolder.individualBirthdate, getApiResponse.individualBirthdate);

            //cleaning
            var delete = await client.DeleteAsync($"{url}/{getApiResponse.individualId}");
        }
        #endregion

        #region Post
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
            address.AddressStreet = "Avenida V-008";
            address.AddressNumber = "2000";
            address.AddressNeighborhood = "Mansões Paraíso";
            address.AddressComplement = "A";
            address.AddressZipCode = "74952560";
            address.AddressCity = "Aparecida de Goiânia";
            address.AddressState = "GO";
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
            Assert.AreEqual(contractHolder.individualAddresses, getApiResponse.individualAddresses);
            Assert.AreEqual(contractHolder.individualTelephones, getApiResponse.individualTelephones);

            //cleaning
            var delete = await client.DeleteAsync($"{url}/{getApiResponse.individualId}");
        }
        #endregion

        #region Update
        [Test]
        public async Task WhenRequestContractHolderControllerUsingPost_ThenICanUpdateAContractHolderRegistryAndReceiveThatObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Samuel Cavalcanti Martins";
            contractHolder.individualEmail = "SamuelCavalcanti@rhyta.com";
            contractHolder.individualCPF = CpfGenerator.GenerateCpf();
            contractHolder.individualRG = "310291136";
            contractHolder.individualBirthdate = new DateTime(1993, 2, 20);

            //act
            var jsonContent = JsonConvert.SerializeObject(contractHolder);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var postResponse = await client.PostAsync($"{url}", contentString);
            var postApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await postResponse.Content.ReadAsStringAsync());

            postApiResponse.individualName = "Samuel Cavalcant Martin";
            postApiResponse.individualEmail = "SamuelCavalcant@rhyta.com";
            postApiResponse.individualRG = "310291137";
            postApiResponse.individualBirthdate = new DateTime(1993, 2, 21);

            jsonContent = JsonConvert.SerializeObject(postApiResponse);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var putResponse = await client.PutAsync($"{url}/{postApiResponse.individualId}", contentString);
            var putApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await putResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync($"{url}/{postApiResponse.individualId}");
            var getApiResponse = JsonConvert.DeserializeObject<ContractHolderViewModel>(await getResponse.Content.ReadAsStringAsync());

            //assert
            Assert.IsNotNull(putApiResponse);
            Assert.IsInstanceOf<ContractHolderViewModel>(putApiResponse);
            Assert.AreEqual(putApiResponse.individualName, getApiResponse.individualName);
            Assert.AreEqual(putApiResponse.individualEmail, getApiResponse.individualEmail);
            Assert.AreEqual(contractHolder.individualCPF, getApiResponse.individualCPF);
            Assert.AreEqual(putApiResponse.individualRG, getApiResponse.individualRG);
            Assert.AreEqual(putApiResponse.individualBirthdate, getApiResponse.individualBirthdate);

            //cleaning
            var delete = await client.DeleteAsync($"{url}/{getApiResponse.individualId}");
        }
        #endregion

        #region Delete
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
            Assert.IsNotNull(deleteApiResponse);
            Assert.IsInstanceOf<ContractHolderViewModel>(deleteApiResponse);
            Assert.AreEqual(deleteApiResponse.isDeleted, true);
        }
        #endregion
    }
}