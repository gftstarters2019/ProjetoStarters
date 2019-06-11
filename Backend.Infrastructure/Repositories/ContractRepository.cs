using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractRepository : IReadOnlyRepository<Contract>, IWriteRepository<Contract>
    {
        private readonly ConfigurationContext _db;

        public ContractRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Contract Find(Guid id) => _db
            .Contracts
            .FirstOrDefault(con => con.ContractId == id);

        public IEnumerable<Contract> Get() => _db
            .Contracts
            .ToList();

        public bool Add(Contract contract)
        {
            if (contract != null)
            {
                _db.Add(contract);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
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

        public Contract Update(Guid id, Contract contract)
        {
            if (contract != null)
            {
                var contractToUpdate = contract;
                if (contract.ContractId != id)
                {
                    contractToUpdate = Find(id);
                    contractToUpdate.ContractCategory = contract.ContractCategory;
                    contractToUpdate.ContractDeleted = contract.ContractDeleted;
                    contractToUpdate.ContractExpiryDate = contract.ContractExpiryDate;
                    contractToUpdate.ContractType = contract.ContractType;
                }
                _db.Update(contractToUpdate);
                _db.SaveChanges();
            }

            return contract;
        }
    }
}
