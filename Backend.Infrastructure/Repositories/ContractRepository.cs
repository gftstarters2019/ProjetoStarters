using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class ContractRepository : IRepository<ContractEntity>
    {
        private readonly ConfigurationContext _db;

        public ContractRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public ContractEntity Find(Guid id) => _db
            .Contracts
            .FirstOrDefault(con => con.ContractId == id);

        public IEnumerable<ContractEntity> Get() => _db
            .Contracts
            .Where(con => !con.ContractDeleted)
            .ToList();

        public ContractEntity Add(ContractEntity contract)
        {
            if (contract == null)
                return null;

            contract.ContractId = Guid.NewGuid();

            return _db.Contracts.Add(contract).Entity;
        }

        public bool Remove(Guid id)
        {
            var contract = Find(id);
            if (contract != null)
            {
                _db.Remove(contract);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public ContractEntity Update(Guid id, ContractEntity contract)
        {
            if (contract != null)
            {
                var contractToUpdate = Find(id);
                if (contractToUpdate != null)
                {
                    contractToUpdate.ContractCategory = contract.ContractCategory;
                    contractToUpdate.ContractDeleted = contract.ContractDeleted;
                    contractToUpdate.ContractExpiryDate = contract.ContractExpiryDate;
                    contractToUpdate.ContractType = contract.ContractType;

                    return _db.Contracts.Update(contractToUpdate).Entity;
                }
            }
            return null;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
