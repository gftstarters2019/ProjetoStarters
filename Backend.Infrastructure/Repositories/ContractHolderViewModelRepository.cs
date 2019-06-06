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
                if (vm.IndividualTelephones.Count() != 0)
                {
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
                }

                // Address
                var addresses = ViewModelCreator.AddressFactory.CreateList(vm.IndividualAddresses);

                if (vm.IndividualAddresses.Count() != 0)
                {
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
                }

                _db.SaveChanges();
                return true;

            }
            return false;
        }

        public ContractHolderViewModel Find(Guid id)
        {
            Individual individual = _db.Individuals.Where(ind => (!ind.IsDeleted) && (ind.BeneficiaryId == id)).First();
            var telephones = _db.Telephones.ToList();
            var addresses = _db.Addresses.ToList();
            var beneficiary_addresses = _db.Beneficiary_Address.ToList();
            var individual_telephones = _db.Individual_Telephone.ToList();

            ContractHolderViewModel ContractHolderViewModel = new ContractHolderViewModel();

            ContractHolderViewModel.IndividualId = individual.BeneficiaryId;
            ContractHolderViewModel.IndividualName = individual.IndividualName;
            ContractHolderViewModel.IndividualCPF = individual.IndividualCPF;
            ContractHolderViewModel.IndividualBirthdate = individual.IndividualBirthdate;
            ContractHolderViewModel.IndividualEmail = individual.IndividualEmail;
            ContractHolderViewModel.IndividualRG = individual.IndividualRG;

            foreach (var beneficiary_address in beneficiary_addresses)
            {
                if (beneficiary_address.BeneficiaryId == individual.BeneficiaryId)
                {
                    foreach (var address in addresses)
                    {
                        if (address.AddressId == beneficiary_address.AddressId)
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

                            ContractHolderViewModel.IndividualAddresses.Add(ad);
                        }
                    }
                }
            }

            foreach (var individual_telephone in individual_telephones)
            {
                if (individual_telephone.BeneficiaryId == individual.BeneficiaryId)
                {
                    foreach (var telephone in telephones)
                    {
                        if (telephone.TelephoneId == individual_telephone.TelephoneId)
                        {
                            Telephone tel = new Telephone();

                            tel.TelephoneId = telephone.TelephoneId;
                            tel.TelephoneNumber = telephone.TelephoneNumber;
                            tel.TelephoneType = telephone.TelephoneType;

                            ContractHolderViewModel.IndividualTelephones.Add(tel);
                        }
                    }
                }
            }
            return ContractHolderViewModel;
        }

        public IEnumerable<ContractHolderViewModel> Get()
        {
            List<ContractHolderViewModel> ContractHolders = new List<ContractHolderViewModel>();

            var individuals = _db.Individuals.Where(ind => !ind.IsDeleted).ToList();
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

        public ContractHolderViewModel Update(Guid id, ContractHolderViewModel vm)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var individual = _db.Individuals.Where(ind => ind.BeneficiaryId == id).First();
                var beneficary_addresses = _db.Beneficiary_Address.Where(benAd => benAd.BeneficiaryId == individual.BeneficiaryId).ToList();
                var individual_telephones = _db.Individual_Telephone.Where(indTel => indTel.BeneficiaryId == individual.BeneficiaryId).ToList();

                if (vm.IsDeleted)
                {
                    individual.IsDeleted = vm.IsDeleted;
                    _db.Update(individual);
                }

                else
                {
                    if (ViewModelCreator.IndividualFactory.Create(vm) == null ||
                        ViewModelCreator.AddressFactory.CreateList(vm.IndividualAddresses).Count() != vm.IndividualAddresses.Count() ||
                        ViewModelCreator.TelephoneFactory.CreateList(vm.IndividualTelephones).Count() != vm.IndividualTelephones.Count())
                        return null;

                    individual.IndividualBirthdate = vm.IndividualBirthdate;
                    individual.IndividualCPF = vm.IndividualCPF;
                    individual.IndividualEmail = vm.IndividualEmail;
                    individual.IndividualName = vm.IndividualName;
                    individual.IndividualRG = vm.IndividualRG;

                    _db.Update(individual);

                    if (beneficary_addresses.Count() > 0)
                        _db.RemoveRange(beneficary_addresses);
                    if (individual_telephones.Count() > 0)
                        _db.RemoveRange(individual_telephones);

                    _db.AddRange(vm.IndividualAddresses);
                    _db.AddRange(vm.IndividualTelephones);

                    foreach (var benAdd in vm.IndividualAddresses)
                    {
                        _db.Add(new BeneficiaryAddress
                        {
                            BeneficiaryAddressId = Guid.NewGuid(),
                            BeneficiaryId = individual.BeneficiaryId,
                            AddressId = benAdd.AddressId
                        });
                    }

                    foreach (var indTel in vm.IndividualTelephones)
                    {
                        _db.Add(new BeneficiaryTelephone
                        {
                            BeneficiaryTelephoneId = Guid.NewGuid(),
                            BeneficiaryId = individual.BeneficiaryId,
                            TelephoneId = indTel.TelephoneId
                        });
                    }

                }

                _db.SaveChanges();

                scope.Complete();

                return vm;
            }
        }

        public bool Remove(Guid id)
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
