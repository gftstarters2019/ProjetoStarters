using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class SignedContractRepository : IRepository<SignedContract>
    {
        private readonly ConfigurationContext _db;

        public SignedContractRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public SignedContract Add(SignedContract signedContract)
        {
            var signedContractContractHolder = _db
                                               .Individuals
                                               .Where(ind => ind.BeneficiaryId == signedContract.IndividualId)
                                               .FirstOrDefault();
            if (signedContractContractHolder == null)
                return null;

            signedContract.SignedContractId = Guid.NewGuid();

            return _db.SignedContracts.Add(signedContract).Entity;
        }

        public SignedContract Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SignedContract> Get() => _db
            .SignedContracts
            .Where(sc => sc.ContractIndividualIsActive)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public SignedContract Update(Guid id, SignedContract t)
        {
            throw new NotImplementedException();
        }
    }
}
