using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.UnitTests;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class Tests
    {
        public ObjectGenerationHelper ObjectGenerator { get; set; }
        [SetUp]
        public void Setup()
        {
            ObjectGenerator = new ObjectGenerationHelper();
        }

        [Test]
        public void WhenCreateAnIndividual_ThenVerifyIfHeIsAContractHolder()
        {
            //arrange
            var individual = ObjectGenerator.GenerateIndividual();

            //act
            var contract = ObjectGenerator.GenerateContract(ContractType.LifeInsurance, ContractCategory.Gold);
            var signedContract = ObjectGenerator.GenerateSignedContract(individual, contract);

            //assert
            Assert.AreEqual(individual.IndividualId, signedContract.IndividualId);

        }

        [Test]
        public void WhenCreateAContractHolder_ThenVerifyIfICanReadHisProperties()
        {
            //arrange
            var beneficiaryId = new Guid("b83b58f8-d488-4181-86ab-1c065ef107ef");
            var individualId = new Guid("3fa31ff9-a641-4831-9625-839009d558b4");
            var individualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            var individualCPF = "45301152897";
            var individualRG = "458559462";
            var individualEmail = "gftstarters2019@outlook.com";
            //act
            var individual = ObjectGenerator.GenerateIndividual();
            var contract = ObjectGenerator.GenerateContract(ContractType.LifeInsurance, ContractCategory.Gold);
            var signedContract = ObjectGenerator.GenerateSignedContract(individual, contract);

            //assert
            Assert.AreEqual(individual.IndividualId, individualId);
            Assert.AreEqual(individual.BeneficiaryId, beneficiaryId);
            Assert.AreEqual(individual.IndividualBirthdate, individualBirthdate);
            Assert.AreEqual(individual.IndividualCPF, individualCPF);
            Assert.AreEqual(individual.IndividualRG, individualRG);
            Assert.AreEqual(individual.IndividualEmail, individualEmail);
        }

       
    }
}