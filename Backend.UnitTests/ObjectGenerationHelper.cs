using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.UnitTests
{
    public class ObjectGenerationHelper
    {
        public void GenerateContractHolder()
        {
            var individual = new Individual();
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualBirthdate = new DateTime();
            individual.IndividualCPF = "45301152897";
            individual.IndividualRG = "458559462";
            individual.IndividualEmail = "gftstarters2019@outlook.com";
            individual.IndividualId = Guid.NewGuid();

            var contract = new Contract();


            var signedContract = new SignedContract();
            signedContract.ContractId = Guid.NewGuid();
            signedContract.ContractIndividualIsActive = true;
            signedContract.ContractSignedContract
        }
    }
}
