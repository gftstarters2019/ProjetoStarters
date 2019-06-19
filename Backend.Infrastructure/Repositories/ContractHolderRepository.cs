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
    public class ContractHolderRepository : IRepository<Individual>
    {
        private readonly ConfigurationContext _db;

        public ContractHolderRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public Individual Find(Guid id) => _db
            .Individuals
            .FirstOrDefault(ind => ind.BeneficiaryId == id);

        public IEnumerable<Individual> Get() => _db
              .Individuals
              .ToList();

        public bool Add(Individual individual)
        {
            if(individual != null)
            {
                _db.Add(individual);
                if (_db.SaveChanges() == 1)
                    return true;

                return false;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var individual = Find(id);
            if(individual != null)
            {
                _db.Remove(individual);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public Individual Update(Guid id, Individual individual)
        {
            if(individual != null)
            {
                _db.Update(individual);
                _db.SaveChanges();
            }

            return individual;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Individual FindCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
