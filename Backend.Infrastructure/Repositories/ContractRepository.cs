using Backend.Core;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractRepository : IReadOnlyRepository<APITeste>, IWriteRepository<APITeste>
    {
        public ContractRepository()
        {

        }

        public APITeste Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APITeste> Get()
        {
            throw new NotImplementedException();
        }

        public void Add(APITeste t)
        {
            
        }

        public APITeste Remove(APITeste t)
        {
            throw new NotImplementedException();
        }

        public APITeste Update(APITeste t)
        {
            throw new NotImplementedException();
        }
    }
}
