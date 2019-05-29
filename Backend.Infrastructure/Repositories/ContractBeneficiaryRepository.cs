using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractBeneficiaryRepository : IReadOnlyRepository<ContractBeneficiary>, IWriteRepository<ContractBeneficiary>
    {
        private readonly ConfigurationContext _db;

        public ContractBeneficiaryRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public void Add(ContractBeneficiary t)
        {
            throw new NotImplementedException();
        }

        public ContractBeneficiary Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractBeneficiary> Get() => _db
            .Contract_Beneficiary
            .Where(cb => cb.SignedContract.ContractIndividualIsActive)
            .ToList();

        public ContractBeneficiary Remove(ContractBeneficiary t)
        {
            throw new NotImplementedException();
        }

        public ContractBeneficiary Update(ContractBeneficiary t)
        {
            throw new NotImplementedException();
        }
    }
}
