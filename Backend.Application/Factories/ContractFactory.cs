using Backend.Application.Interfaces;
using Backend.Core.Enums;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class ContractFactory : IFactory<Contract>
    {
        
        public Contract Create(Guid id, ContractType contractType, ContractCategory contractCategory, DateTime contractepiryDate, bool contractDeleted)
        {
           var contract = new Contract();
            contract.ContractId = Guid.NewGuid();
            contract.ContractType = contractType;
            contract.ContractCategory = contractCategory;
            contract.ContractExpiryDate = contractepiryDate;
            contract.ContractDeleted = contractDeleted;



            return contract;
        }
        

        public Contract Create()
        {
            return new Contract();
        }
    }
}