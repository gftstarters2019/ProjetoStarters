using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Backend.Infrastructure.Repositories
{
    public class ContractHolderViewModelRepository : IReadOnlyRepository<ContractHolderViewModel>, IWriteRepository<ContractHolderViewModel>
    {
        private readonly ConfigurationContext _db;

        public ContractHolderViewModelRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(ContractHolderViewModel vm)
        {
            if (vm != null)
            {
                // Individual
                var individual = ViewModelCreator.IndividualFactory.Create(vm);

                if (individual == null)
                    return false;

                _db.Add(individual);

                // Telephone
                var telephones = ViewModelCreator.TelephoneFactory.CreateList(vm.IndividualTelephones);
                if (telephones.Count != vm.IndividualTelephones.Count || !telephones.Any())
                    return false;
                if (telephones.Count > 0)
                {
                    foreach (var telephone in telephones)
                    {
                        _db.Add(telephone);

                        _db.Add(new BeneficiaryTelephone
                        {
                            BeneficiaryTelephoneId = Guid.NewGuid(),
                            BeneficiaryId = individual.BeneficiaryId,
                            TelephoneId = telephone.TelephoneId
                        });

                    }
                }

                // Address
                var addresses = ViewModelCreator.AddressFactory.CreateList(vm.IndividualAddresses);
                if (addresses.Count != vm.IndividualAddresses.Count || !addresses.Any())
                    return false;
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        _db.Add(address);

                        _db.Add(new BeneficiaryAddress
                        {
                            BeneficiaryAddressId = Guid.NewGuid(),
                            BeneficiaryId = individual.BeneficiaryId,
                            AddressId = address.AddressId
                        });

                    }
                }

                _db.SaveChanges();
                return true;

            }
            return false;
        }

        public ContractHolderViewModel Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractHolderViewModel> Get()
        {
            throw new NotImplementedException();
        }

        public ContractHolderViewModel Remove(ContractHolderViewModel t)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                //
                throw new NotImplementedException();
                _db.SaveChanges();

                scope.Complete();
            }
        }

        public ContractHolderViewModel Update(ContractHolderViewModel t)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                throw new NotImplementedException();
                _db.SaveChanges();

                scope.Complete();
            }
        }
    }
}
