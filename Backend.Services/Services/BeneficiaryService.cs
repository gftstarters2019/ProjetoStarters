using Backend.Core.Domains;
using Backend.Services.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Backend.Services.Services
{
    public class BeneficiaryService : IService<BeneficiaryDomain>
    {
        public BeneficiaryDomain Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public BeneficiaryDomain Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<BeneficiaryDomain> GetAll()
        {
            throw new NotImplementedException();
        }

        public BeneficiaryDomain Save(BeneficiaryDomain modelToAddToDB)
        {
            throw new NotImplementedException();
        }

        public BeneficiaryDomain Update(Guid id, BeneficiaryDomain modelToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
