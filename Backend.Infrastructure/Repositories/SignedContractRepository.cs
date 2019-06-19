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
        private readonly IRepository<Contract> _contractRepository;

        public SignedContractRepository(ConfigurationContext db,
                                        IRepository<Contract> contractRepository)
        {
            _db = db;
            _contractRepository = contractRepository;
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
            var signedContract = _db.SignedContracts.FirstOrDefault(sc => sc.ContractId == id);
            signedContract.SignedContractContract = _contractRepository.Find(signedContract.ContractId);
            return signedContract;
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
            return _db.SaveChanges() > 0;
        }

        public SignedContract Update(Guid id, SignedContract signedContract)
        {
            if (signedContract != null)
            {
                var signedContractToUpdate = Find(id);
                if (signedContractToUpdate != null)
                {
                    signedContractToUpdate.ContractIndividualIsActive = signedContract.ContractIndividualIsActive;
                    signedContractToUpdate.IndividualId = signedContract.IndividualId;
                    return _db.SignedContracts.Update(signedContractToUpdate).Entity;
                }
            }
            return null;
        }
    }
}
