using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractBeneficiaryRepository : IRepository<ContractBeneficiary>
    {
        private readonly ConfigurationContext _db;

        public ContractBeneficiaryRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public ContractBeneficiary Add(ContractBeneficiary contractBeneficiary)
        {
            if(contractBeneficiary != null)
            {
                // Gets all Active SignedContracts that the Beneficiary is in
                var signedContracts = _db
                                         .SignedContracts
                                         .Where(sc => _db
                                                         .Contract_Beneficiary
                                                         .Where(cb => cb.BeneficiaryId == contractBeneficiary.BeneficiaryId)
                                                         .Select(cb => cb.SignedContractId)
                                                         .Contains(sc.SignedContractId)
                                                && sc.ContractIndividualIsActive)
                                         .ToList();

                // Verifies if any of the active SignedContracts is the same type as the one being added
                foreach (var beneficiarySignedContract in signedContracts)
                {
                    if (_db.Contracts.Where(con => con.ContractId == beneficiarySignedContract.ContractId
                                                   && con.ContractType == contractBeneficiary.SignedContract.SignedContractContract.ContractType)
                                     .Any())
                        return null;
                }

                contractBeneficiary.ContractBeneficiaryId = Guid.NewGuid();
                return _db.Contract_Beneficiary.Add(contractBeneficiary).Entity;
            }
            return null;
        }

        public ContractBeneficiary Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public ContractBeneficiary FindCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractBeneficiary> Get() => _db
            .Contract_Beneficiary
            .Where(cb => cb.SignedContract.ContractIndividualIsActive)
            .ToList();

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public ContractBeneficiary Update(Guid id, ContractBeneficiary t)
        {
            throw new NotImplementedException();
        }
    }
}
