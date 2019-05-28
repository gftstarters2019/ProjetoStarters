using Backend.Core.Enums;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.UnitTests
{
    public class ObjectGenerationHelper
    {

        public Individual GenerateIndividual()
        {
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime();
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualId = Guid.NewGuid();

            return individual;
        }

        public Contract GenerateContract(ContractType contractType, ContractCategory contractCategory)
        {
            var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = contractType;
            contract.ContractCategory = contractCategory;
            contract.ContractExpiryDate = new DateTime();
            contract.ContractDeleted = false;

            return contract;
        }

        public SignedContract GenerateSignedContract(Individual individual, Contract contract)
        {
            var signedContract = new SignedContract();
            signedContract.ContractSignedId = Guid.NewGuid();
            signedContract.IndividualId = individual.IndividualId;
            signedContract.ContractId = contract.ContractId;
            signedContract.ContractSignedIndividual = individual;
            signedContract.ContractSignedContract = contract;

            return signedContract;
        }
    }
}
