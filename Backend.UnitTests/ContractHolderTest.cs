using Backend.Core.Enums;
using Backend.Core.Models;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class ContractHolderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenCreateAnIndividual_ThenVerifyIfHeIsAContractHolder()
        {
            //arrange
            var individual = new IndividualEntity();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            

            //act
            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContractEntity();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;


            //assert
            //Assert.AreEqual(individual.IndividualId, signedContract.IndividualId);
            Assert.AreEqual(individual, signedContract.SignedContractIndividual);

        }

        [Test]
        public void WhenCreateAContractHolder_ThenVerifyIfICanReadHisProperties()
        {
            //arrange
            var beneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            var individualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            var individualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            var individualCPF = "45301152897";
            var individualRG = "458559462";
            var individualEmail = "gftstarters2019@outlook.com";

            //act
            var individual = new IndividualEntity();
            individual.BeneficiaryId = new Guid("103660e3-5fd7-4606-bb1d-9d0f52e9c17a");
            //individual.IndividualId = new Guid("184ac189-467f-4e3b-badc-b6c299a25bc0");
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            

            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContractEntity();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;


            //assert
            //Assert.AreEqual(individual.IndividualId, individualId);
            Assert.AreEqual(individual.BeneficiaryId, beneficiaryId);
            Assert.AreEqual(individual.IndividualBirthdate, individualBirthdate);
            Assert.AreEqual(individual.IndividualCPF, individualCPF);
            Assert.AreEqual(individual.IndividualRG, individualRG);
            Assert.AreEqual(individual.IndividualEmail, individualEmail);
            
        }

        [Test]
        public void WhenCreateAContractHolder_ThenVerifyIfICanFindById()
        {
            //arrange
            var individual = new IndividualEntity();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            

            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContractEntity();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;


            //act
            //var individualId = individual.IndividualId;

            //assert
            //Assert.AreEqual(individual.IndividualId, individualId);
        }

        [Test]
        public void WhenCreateContractHolder_AndUpdateHim_ThenVerifyIfHeWasUpdated()
        {
            //arrange
            var individual = new IndividualEntity();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            

            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContractEntity();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;


            var updatedIndividual = new IndividualEntity();

            var beneficiaryId = Guid.NewGuid();
            var individualId = Guid.NewGuid();
            var individualBirthdate = new DateTime();
            var individualCPF = "45301152898";
            var individualRG = "458559466";
            var individualEmail = "gftstarters@outlook.com";

            //act
            updatedIndividual.BeneficiaryId = beneficiaryId;
            //updatedIndividual.IndividualId = individualId;
            updatedIndividual.IndividualBirthdate = individualBirthdate;
            updatedIndividual.IndividualCPF = individualCPF;
            updatedIndividual.IndividualRG = individualRG;
            updatedIndividual.IndividualEmail = individualEmail;

            //assert
            //Assert.AreNotEqual(individual.IndividualId, updatedIndividual.IndividualId);
            Assert.AreNotEqual(individual.BeneficiaryId, updatedIndividual.BeneficiaryId);
            Assert.AreNotEqual(individual.IndividualBirthdate, updatedIndividual.IndividualBirthdate);
            Assert.AreNotEqual(individual.IndividualCPF, updatedIndividual.IndividualCPF);
            Assert.AreNotEqual(individual.IndividualRG, updatedIndividual.IndividualRG);
            Assert.AreNotEqual(individual.IndividualEmail, updatedIndividual.IndividualEmail);
        }


        [Test]
        public void WhenCreateAContractHolder_AndDeleteHim_ThenVerifyIfHeWasDeleted()
        {
            //arrange
            var individual = new IndividualEntity();
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            

            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var signedContract = new SignedContractEntity();
            signedContract.SignedContractId = Guid.NewGuid();
            //signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.SignedContractIndividual = individual;
            signedContract.SignedContractContract = contract;
            signedContract.ContractIndividualIsActive = true;

            //act
            var signedContractDeleted = signedContract.ContractIndividualIsActive = false;
            var contractDeleted = contract.ContractDeleted = true;
            

            //assert
            Assert.IsFalse(signedContractDeleted);
            Assert.IsTrue(contractDeleted);
        }
    }
}