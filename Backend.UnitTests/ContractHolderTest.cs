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
    }
}