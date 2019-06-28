using Backend.Core.Enums;
using Backend.Core.Models;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class ContractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenCreateAContract_ThenVerifyIfICanReadItsProperties()
        {
            //arrange
            var contractId = new Guid("8b5e7842-0087-4ea9-9903-f127043ede90");
            var contractType = ContractType.HealthPlan;
            var contractCategory = ContractCategory.Diamond;
            var contractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            var contractDeleted = false;

            //act
            var contract = new ContractEntity();
            contract.ContractId = new Guid("8b5e7842-0087-4ea9-9903-f127043ede90");
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            //assert
            Assert.AreEqual(contract.ContractId, contractId);
            Assert.AreEqual(contract.ContractType, contractType);
            Assert.AreEqual(contract.ContractCategory, contractCategory);
            Assert.AreEqual(contract.ContractExpiryDate, contractExpiryDate);
            Assert.AreEqual(contract.ContractDeleted, contractDeleted);
        }

        [Test]
        public void WhenCreateAContract_ThenVerifyIfICanFindById()
        {
            //arrange
            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            //act
            var contractId = contract.ContractId;

            //assert
            Assert.AreEqual(contract.ContractId, contractId);
        }

        [Test]
        public void WhenCreateContract_AndUpdateIt_ThenVerifyIfItWasUpdated()
        {
            //arrange
            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            var updatedContract = new ContractEntity();

            var contractId = Guid.NewGuid();
            var contractType = ContractType.LifeInsurance;
            var contractCategory = ContractCategory.Gold;
            var contractExpiryDate = new DateTime();
            var contractDeleted = false;

            //act
            updatedContract.ContractId = contractId;
            updatedContract.ContractType = contractType;
            updatedContract.ContractCategory = contractCategory;
            updatedContract.ContractExpiryDate = contractExpiryDate;
            updatedContract.ContractDeleted = contractDeleted;

            //assert
            Assert.AreNotEqual(contract.ContractId, updatedContract.ContractId);
            Assert.AreNotEqual(contract.ContractType, updatedContract.ContractType);
            Assert.AreNotEqual(contract.ContractCategory, updatedContract.ContractCategory);
            Assert.AreNotEqual(contract.ContractExpiryDate, updatedContract.ContractExpiryDate);
        }


        [Test]
        public void WhenCreateAContractHolder_AndDeleteHim_ThenVerifyIfHeWasDeleted()
        {
            //arrange
            var contract = new ContractEntity();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = ContractType.HealthPlan;
            contract.ContractCategory = ContractCategory.Diamond;
            contract.ContractExpiryDate = new DateTime(2019, 05, 28, 7, 0, 0);
            contract.ContractDeleted = false;

            //act
            contract.ContractDeleted = true;

            //assert
            Assert.IsTrue(contract.ContractDeleted);
        }
    }
}