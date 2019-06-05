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
            List<ContractHolderViewModel> ContractHolders = new List<ContractHolderViewModel>();

            var individuals = _db.Individuals.ToList();
            var telephones = _db.Telephones.ToList();
            var addresses = _db.Addresses.ToList();
            var beneficiary_addresses = _db.Beneficiary_Address.ToList();
            var individual_telephones = _db.Individual_Telephone.ToList();

            foreach (var individual in individuals)
            {
                ContractHolderViewModel vm = new ContractHolderViewModel();
                vm.IndividualId = individual.BeneficiaryId;
                vm.IndividualName = individual.IndividualName;
                vm.IndividualCPF = individual.IndividualCPF;
                vm.IndividualBirthdate = individual.IndividualBirthdate;
                vm.IndividualEmail = individual.IndividualEmail;
                vm.IndividualRG = individual.IndividualRG;

                foreach (var beneficiary_address in beneficiary_addresses)
                {
                    if(beneficiary_address.BeneficiaryId == individual.BeneficiaryId)
                    {
                        foreach (var address in addresses)
                        {
                            if(address.AddressId == beneficiary_address.AddressId)
                            {
                                Address ad = new Address();

                                ad.AddressId = address.AddressId;
                                ad.AddressCity = address.AddressCity;
                                ad.AddressComplement = address.AddressComplement;
                                ad.AddressCountry = address.AddressCountry;
                                ad.AddressNeighborhood = address.AddressNeighborhood;
                                ad.AddressNumber = address.AddressNumber;
                                ad.AddressState = address.AddressState;
                                ad.AddressStreet = address.AddressStreet;
                                ad.AddressType = address.AddressType;
                                ad.AddressZipCode = address.AddressZipCode;

                                vm.IndividualAddresses.Add(ad);
                            }
                        }
                    }
                }

                foreach (var individual_telephone in individual_telephones)
                {
                    if(individual_telephone.BeneficiaryId == individual.BeneficiaryId)
                    {
                        foreach (var telephone in telephones)
                        {
                            if(telephone.TelephoneId == individual_telephone.TelephoneId)
                            {
                                Telephone tel = new Telephone();

                                tel.TelephoneId = telephone.TelephoneId;
                                tel.TelephoneNumber = telephone.TelephoneNumber;
                                tel.TelephoneType = telephone.TelephoneType;

                                vm.IndividualTelephones.Add(tel);
                            }
                        }
                    }
                }

                ContractHolders.Add(vm);
            }

            return ContractHolders;

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
