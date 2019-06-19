using Backend.Application.Factories.Interfaces;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories
{
    public class PetFactory : IFactoryList<PetEntity>
    {
        private List<AddressEntity> addresses = null;

        public List<PetEntity> CreateList(List<PetEntity> pets)
        {
            throw new NotImplementedException();
        }

        public PetEntity Create(ContractViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
