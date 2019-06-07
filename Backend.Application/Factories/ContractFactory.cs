using Backend.Application.Factories.Interfaces;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories
{
    public class ContractFactory : IFactory<Contract, ContractViewModel>
    {

        public Contract Create(ContractViewModel contractViewModel)
        {
            Contract contract = new Contract()
            {
                ContractCategory = contractViewModel.Category,
                ContractDeleted = false,
                ContractExpiryDate = contractViewModel.ExpiryDate,
                ContractId = Guid.NewGuid(),
                ContractType = contractViewModel.Type
            };
            return contract;
        }
    }
}
