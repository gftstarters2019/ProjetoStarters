using Backend.Application.Factories.Interfaces;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories
{
    public class PetFactory : IFactoryList<Pet>
    {
        private List<Address> addresses = null;

        public List<Pet> CreateList(List<Pet> pets)
        {
            throw new NotImplementedException();
        }

        public Pet Create(ContractViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
