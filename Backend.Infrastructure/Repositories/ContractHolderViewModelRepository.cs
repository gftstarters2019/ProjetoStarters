using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;

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
            if(vm != null)
            {
                var viewModelCreator = new ViewModelCreator(vm);

                // Individual
                var individual = viewModelCreator.Individual;

                if (individual == null)
                    return false;

                _db.Add(individual);

                // Telephone
                var telephones = viewModelCreator.Telephone;
                if (telephones == null)
                    return false;
                if (telephones.Count > 0)
                {
                    foreach (var telephone in telephones)
                    {
                        _db.Add(telephone);

                        _db.Add(new BeneficiaryTelephone
                        {
                            BeneficiaryTelephoneId = Guid.NewGuid(),
                            BeneficiaryId = individual.IndividualId,
                            TelephoneId = telephone.TelephoneId
                        });

                    }
                }

                // Address
                var addresses = viewModelCreator.Address;
                if (addresses == null)
                    return false;
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        _db.Add(address);

                        _db.Add(new BeneficiaryAddress
                        {
                            BeneficiaryAddressId = Guid.NewGuid(),
                            BeneficiaryId = individual.IndividualId,
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
            throw new NotImplementedException();
        }

        public ContractHolderViewModel Update(ContractHolderViewModel t)
        {
            throw new NotImplementedException();
        }
    }
}
