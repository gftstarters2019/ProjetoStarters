using Backend.Core.Models;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenCreateAnIndividual_ThenVerifyIfHeIsAContractHolder()
        {
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime();
            individual.IndividualCPF = "45301152897";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualId = Guid.NewGuid();

        }
    }
}