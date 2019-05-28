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
            individual.BeneficiaryId = new Guid("b83b58f8-d488-4181-86ab-1c065ef107ef");
            individual.IndividualId = new Guid("3fa31ff9-a641-4831-9625-839009d558b4");
            individual.IndividualBirthdate = new DateTime(2019, 05, 28, 7, 0, 0);
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";

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
