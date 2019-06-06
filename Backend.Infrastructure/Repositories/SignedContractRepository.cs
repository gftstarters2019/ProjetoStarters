using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class SignedContractRepository : IReadOnlyRepository<SignedContract>, IWriteRepository<SignedContract>
    {
        private readonly ConfigurationContext _db;

        public SignedContractRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(SignedContract t)
        {
            throw new NotImplementedException();
        }

        public SignedContract Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SignedContract> Get() => _db
            .SignedContracts
            .Where(sc => sc.ContractIndividualIsActive)
            .ToList();

        public SignedContract Remove(SignedContract t)
        {
            throw new NotImplementedException();
        }

        public SignedContract Update(Guid id, SignedContract t)
        {
            throw new NotImplementedException();
        }
    }
}
