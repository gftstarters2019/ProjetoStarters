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
                //var telephones = ViewModelCreator.Generate(vm.IndividualTelephones);
                //if (telephones == null)
                //    return false;
                //if (telephones.Count > 0)
                //{
                //    foreach (Telephone in telephones)
                //    {
                //        var db_tel = _db.Add(telephone);
                //        if (db_tel == null) return false;

                //        var db_bentel = _db.Add(new BeneficiaryTelephone
                //        {
                //            BeneficiaryId = individual.id,
                //            TelephoneId = telephone.telephoneId
                //        });
                //        if (db_bentel == null) return false;
                //    }
                //}

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
