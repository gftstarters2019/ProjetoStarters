using Backend.Core.Models;
using Backend.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Services
{
    public class ContractHolderService : IService<Individual>
    {
        public Individual Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Individual Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Individual> GetAll()
        {
            throw new NotImplementedException();
        }

        public Individual Save(Individual modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public Individual Update(Guid id, Individual modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
