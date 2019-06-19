﻿using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractRepository : IRepository<Contract>
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

        public Contract Add(Contract contract)
        {
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

        public Contract Update(Guid id, Contract contract)
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
