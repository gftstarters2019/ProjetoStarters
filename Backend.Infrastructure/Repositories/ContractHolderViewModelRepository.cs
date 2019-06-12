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

        /// <summary>
        /// Add de Contract Holder
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool Add(ContractHolderViewModel vm)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (vm != null)
                {
                    // Individual
                    var individual = ViewModelCreator.IndividualFactory.Create(vm);

                    if (individual == null)
                        return false;
                    else if (_db.Individuals.Where(ind => ind.IndividualCPF == individual.IndividualCPF).Any())
                        return false;

                    _db.Add(individual);

                    // Telephone
                    if (vm.individualTelephones.Count() != 0)
                    {
                        var telephones = ViewModelCreator.TelephoneFactory.CreateList(vm.individualTelephones);
                        if (telephones.Count != vm.individualTelephones.Count)
                            return false;

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
                    if (vm.individualAddresses.Count() != 0)
                    {
                        var addresses = ViewModelCreator.AddressFactory.CreateList(vm.individualAddresses);
                        if (addresses.Count != vm.individualAddresses.Count)
                            return false;

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
                    scope.Complete();
                    return true;

                }
                scope.Complete();
                return false;
            }
        }

        /// <summary>
        /// Find de Contract Holder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContractHolderViewModel Find(Guid id)
        {
            Individual individual = _db.Individuals.Where(ind => (!ind.IsDeleted) && (ind.BeneficiaryId == id)).FirstOrDefault();

            if (individual == null)
                return null;

            var beneficiary_addresses = _db.Beneficiary_Address.Where(benAd => (individual != null) && (benAd.BeneficiaryId == individual.BeneficiaryId)).ToList();
            var individual_telephones = _db.Individual_Telephone.Where(indTel => indTel.BeneficiaryId == individual.BeneficiaryId).ToList();

            ContractHolderViewModel ContractHolderViewModel = new ContractHolderViewModel();

            //Individual
            ContractHolderViewModel.individualId = individual.BeneficiaryId;
            ContractHolderViewModel.individualName = individual.IndividualName;
            ContractHolderViewModel.individualCPF = individual.IndividualCPF;
            ContractHolderViewModel.individualBirthdate = individual.IndividualBirthdate;
            ContractHolderViewModel.individualEmail = individual.IndividualEmail;
            ContractHolderViewModel.individualRG = individual.IndividualRG;

            //Addresses
            foreach (var beneficiary_address in beneficiary_addresses)
            {
                var addresses = _db.Addresses.Where(ad => ad.AddressId == beneficiary_address.AddressId).ToList();

                foreach (var address in addresses)
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

                    ContractHolderViewModel.individualAddresses.Add(ad);                    
                }            
            }

            //Telephones
            foreach (var individual_telephone in individual_telephones)
            {
                var telephones = _db.Telephones.Where(tel => tel.TelephoneId == individual_telephone.TelephoneId).ToList();

                foreach (var telephone in telephones)
                {
                    Telephone tel = new Telephone();

                    tel.TelephoneId = telephone.TelephoneId;
                    tel.TelephoneNumber = telephone.TelephoneNumber;
                    tel.TelephoneType = telephone.TelephoneType;

                    ContractHolderViewModel.individualTelephones.Add(tel);
                }               
            }
            return ContractHolderViewModel;
        }

        /// <summary>
        /// Get de Contract Holder
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContractHolderViewModel> Get()
        {
            List<ContractHolderViewModel> ContractHolders = new List<ContractHolderViewModel>();

            var individuals = _db.Individuals.Where(ind => !ind.IsDeleted).ToList();

            foreach (var individual in individuals)
            {
                //Individuals
                ContractHolderViewModel vm = new ContractHolderViewModel();
                vm.individualId = individual.BeneficiaryId;
                vm.individualName = individual.IndividualName;
                vm.individualCPF = individual.IndividualCPF;
                vm.individualBirthdate = individual.IndividualBirthdate;
                vm.individualEmail = individual.IndividualEmail;
                vm.individualRG = individual.IndividualRG;

                //Addresses
                var beneficiary_addresses = _db.Beneficiary_Address.Where(benAdr => benAdr.BeneficiaryId == individual.BeneficiaryId).ToList();

                foreach (var beneficiary_address in beneficiary_addresses)
                {
                    var addresses = _db.Addresses.Where(ad => ad.AddressId == beneficiary_address.AddressId).ToList();

                    foreach (var address in addresses)
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

                        vm.individualAddresses.Add(ad);
                        
                    }                  
                }

                //Telephones
                var individual_telephones = _db.Individual_Telephone.Where(indTel => indTel.BeneficiaryId == individual.BeneficiaryId).ToList();

                foreach (var individual_telephone in individual_telephones)
                {
                    var telephones = _db.Telephones.Where(tel => tel.TelephoneId == individual_telephone.TelephoneId).ToList();

                    foreach (var telephone in telephones)
                    {

                        Telephone tel = new Telephone();

                        tel.TelephoneId = telephone.TelephoneId;
                        tel.TelephoneNumber = telephone.TelephoneNumber;
                        tel.TelephoneType = telephone.TelephoneType;

                        vm.individualTelephones.Add(tel);
                        
                    }

                }

                ContractHolders.Add(vm);
            }

            return ContractHolders;
        }

        /// <summary>
        /// Update/Delete de Contract Holder
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ContractHolderViewModel Update(Guid id, ContractHolderViewModel vm)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var individual = _db.Individuals.Where(ind => ind.BeneficiaryId == id).FirstOrDefault();

                if (individual == null)
                    return null;

                var beneficary_addresses = _db.Beneficiary_Address.Where(benAd => benAd.BeneficiaryId == individual.BeneficiaryId).ToList();
                var individual_telephones = _db.Individual_Telephone.Where(indTel => indTel.BeneficiaryId == individual.BeneficiaryId).ToList();

                //Soft Delete
                if (vm.isDeleted)
                {
                    if (_db.SignedContracts.Where(sigCon => (sigCon.ContractIndividualIsActive == true) && (sigCon.IndividualId == id)).Any())
                        return null;

                    individual.IsDeleted = vm.isDeleted;
                    _db.Update(individual);
                }

                //Update
                else
                {
                    if (ViewModelCreator.IndividualFactory.Create(vm) == null ||
                        ViewModelCreator.AddressFactory.CreateList(vm.individualAddresses).Count() != vm.individualAddresses.Count() ||
                        ViewModelCreator.TelephoneFactory.CreateList(vm.individualTelephones).Count() != vm.individualTelephones.Count())
                        return null;

                    else if (individual.IndividualCPF != vm.individualCPF)
                        return null;

                    individual.IndividualBirthdate = vm.individualBirthdate;
                    individual.IndividualCPF = vm.individualCPF;
                    individual.IndividualEmail = vm.individualEmail;
                    individual.IndividualName = vm.individualName;
                    individual.IndividualRG = vm.individualRG;

                    _db.Update(individual);

                    if (beneficary_addresses.Count() > 0)
                        _db.RemoveRange(beneficary_addresses);
                    if (individual_telephones.Count() > 0)
                        _db.RemoveRange(individual_telephones);

                    _db.AddRange(vm.individualAddresses);
                    _db.AddRange(vm.individualTelephones);

                    foreach (var benAdd in vm.individualAddresses)
                    {
                        _db.Add(new BeneficiaryAddress
                        {
                            BeneficiaryAddressId = Guid.NewGuid(),
                            BeneficiaryId = individual.BeneficiaryId,
                            AddressId = benAdd.AddressId
                        });
                    }

                    foreach (var indTel in vm.individualTelephones)
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
