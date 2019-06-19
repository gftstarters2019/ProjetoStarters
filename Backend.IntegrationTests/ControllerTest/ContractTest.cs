using Backend.Application.ViewModels;
using Backend.Core.Enums;
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
    public class ContractTest : BaseIntegrationTest
    {
        private const string url = "/api/Contract";
        private const string urlContractHolder = "/api/ContractHolder";

        [Test]
        public async Task WhenRequestGetUsingController_ThenIShouldReceiveContracts()
        {

            // Act
            var response = await client.GetAsync($"{url}");
            var apiResponse = JsonConvert.DeserializeObject<IEnumerable<ContractViewModel>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.IsNotNull(apiResponse);
        }

        [Test]
        public async Task WhenRequestOwnerControllerUsingPostSendingAListOfIndividuals_ThenICanRequestContractObject()
        {
            //arrange
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ContractViewModel contract = new ContractViewModel();
            contract.Type = ContractType.DentalPlan;
            contract.Category = ContractCategory.Silver;
            contract.ExpiryDate = new DateTime(2020, 10, 5);
            contract.IsActive = true;

            ContractHolderViewModel contractHolder = new ContractHolderViewModel();
            contractHolder.individualName = "Caio Silva Ferreira";
            contractHolder.individualEmail = "CaioSilvaFerreira@rhyta.com";
            contractHolder.individualCPF = "58302207098";
            contractHolder.individualRG = "335373793";
            contractHolder.individualBirthdate = new DateTime(1966, 12, 28);

            Individual firstBeneficiary = new Individual();
            firstBeneficiary.IndividualName = "Bianca Correia Fernandes";
            firstBeneficiary.IndividualEmail = "BiancaCorreiaFernandes@teleworm.com";
            firstBeneficiary.IndividualCPF = "58302207098";
            firstBeneficiary.IndividualRG = "335838108";
            firstBeneficiary.IndividualBirthdate = new DateTime(1978, 6, 19);
            contract.Individuals.Add(firstBeneficiary);

            Individual secondBeneficiary = new Individual();
            secondBeneficiary.IndividualName = "Cauã Costa Lima";
            secondBeneficiary.IndividualEmail = "CauaCostaLima@jourrapide.com";
            secondBeneficiary.IndividualCPF = "58302207098";
            secondBeneficiary.IndividualRG = "109014509";
            secondBeneficiary.IndividualBirthdate = new DateTime(1993, 4, 21);
            contract.Individuals.Add(secondBeneficiary);

            Individual thirdBeneficiary = new Individual();
            thirdBeneficiary.IndividualName = "Livia Pereira Costa";
            thirdBeneficiary.IndividualEmail = "LiviaPereiraCosta@dayrep.com";
            thirdBeneficiary.IndividualCPF = "58302207098";
            thirdBeneficiary.IndividualRG = "102653604";
            thirdBeneficiary.IndividualBirthdate = new DateTime(1957, 7, 24);
            contract.Individuals.Add(thirdBeneficiary);

            Individual fourthBeneficiary = new Individual();
            fourthBeneficiary.IndividualName = "Beatrice Fernandes Barbosa";
            fourthBeneficiary.IndividualEmail = "BeatriceFernandesBarbosa@teleworm.us";
            fourthBeneficiary.IndividualCPF = "58302207098";
            fourthBeneficiary.IndividualRG = "484661632";
            fourthBeneficiary.IndividualBirthdate = new DateTime(1955, 9, 3);
            contract.Individuals.Add(fourthBeneficiary);

            Individual fifthBeneficiary = new Individual();
            fifthBeneficiary.IndividualName = "Júlio Souza Barros";
            fifthBeneficiary.IndividualEmail = "JulioSouzaBarros@rhyta.com";
            fifthBeneficiary.IndividualCPF = "58302207098";
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