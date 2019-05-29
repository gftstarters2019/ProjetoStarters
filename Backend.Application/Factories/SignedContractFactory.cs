using Backend.Application.Interfaces;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class SignedContractFactory : IFactory<SignedContract>
    {
        public SignedContract Create(Guid id, Guid individualId, Guid contractId, bool contractIsActive)
        {
            var signedcontract = new SignedContract();
            signedcontract.ContractSignedId = Guid.NewGuid();
            signedcontract.IndividualId = individualId;
            signedcontract.ContractId = contractId;
            signedcontract.ContractIndividualIsActive = contractIsActive;

            return signedcontract;


        }
        public SignedContract Create()
        {
            return new SignedContract();
        }
    }
}