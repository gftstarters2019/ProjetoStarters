using Backend.Application.Interfaces;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class ContractBeneficiaryFactory : IFactory<ContractBeneficiary>
    {
        public ContractBeneficiary Create(Guid contractBeneficiaryId, Guid signedContractId, Guid beneficiaryId)
        {
            var contractBeneficiary = new ContractBeneficiary();
            contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
            contractBeneficiary.SignedContractId = signedContractId;
            contractBeneficiary.BeneficiaryId = beneficiaryId;

            return contractBeneficiary;

        }
        public ContractBeneficiary Create()
        {
            return new ContractBeneficiary();
        }
    }
}