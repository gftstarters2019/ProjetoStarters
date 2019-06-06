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
            if(contract != null)
            {
                _db.Add(contract);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
        }

        public Contract Remove(Contract contract)
        {
            if(contract != null)
            {
                _db.Remove(contract);
                _db.SaveChanges();
            }

            return contract;
        }

        public Contract Update(Contract contract)
        {
            if(contract != null)
            {
                _db.Update(contract);
                _db.SaveChanges();
            }

            return contract;
        }
    }
}
