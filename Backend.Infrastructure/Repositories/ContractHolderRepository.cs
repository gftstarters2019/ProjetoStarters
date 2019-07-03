using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Converters;
using Backend.Infrastructure.Repositories.Interfaces;

namespace Backend.Infrastructure.Repositories
{
    public class ContractHolderRepository : IRepository<ContractHolderDomain>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<IndividualEntity> _individualsRepository;
        private readonly IRepository<TelephoneEntity> _telephonesRepository;
        private readonly IRepository<IndividualTelephone> _individualTelephonesRepository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<BeneficiaryAddress> _beneficiaryAddressRepository;
        private bool disposed = false;

        public ContractHolderRepository(ConfigurationContext db,
                                        IRepository<IndividualEntity> individualsRepository,
                                        IRepository<TelephoneEntity> telephonesRepository,
                                        IRepository<IndividualTelephone> individualTelephonesRepository,
                                        IRepository<AddressEntity> addressRepository,
                                        IRepository<BeneficiaryAddress> beneficiaryAddressRepository)
        {
            _db = db;

            _individualsRepository = individualsRepository;
            _telephonesRepository = telephonesRepository;
            _individualTelephonesRepository = individualTelephonesRepository;
            _addressRepository = addressRepository;
            _beneficiaryAddressRepository = beneficiaryAddressRepository;
        }

        #region Add
        public ContractHolderDomain Add(ContractHolderDomain contractHolder)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if(contractHolder != null)
                {
                    contractHolder.Individual = AddIndividual(contractHolder.Individual);
                    
                    contractHolder.IndividualTelephones = AddTelephones(contractHolder.IndividualTelephones, contractHolder.Individual.BeneficiaryId);

                    contractHolder.IndividualAddresses = AddAddresses(contractHolder.IndividualAddresses, contractHolder.Individual.BeneficiaryId);

                    scope.Complete();
                    return contractHolder;
                }
                return null;
            }
        }

        private IndividualDomain AddIndividual(IndividualDomain individualToAdd)
        {
            var addedContractHolder = ConvertersManager.IndividualConverter.Convert(
                        _individualsRepository.Add(ConvertersManager.IndividualConverter.Convert(
                            individualToAdd)));

            if (addedContractHolder == null)
                throw new Exception("CPF já cadastrado! ");

            _individualsRepository.Save();

            return addedContractHolder;
        }

        private List<TelephoneDomain> AddTelephones(List<TelephoneDomain> telephonesToAdd, Guid beneficiaryId)
        {
            if (telephonesToAdd.Count > 5)
                throw new Exception("Quantidade máxima de telefones excedida! ");

            var addedTelephones = new List<TelephoneDomain>();
            foreach (var telephone in telephonesToAdd)
            {
                var addedTelephone = ConvertersManager.TelephoneConverter.Convert(
                    _telephonesRepository.Add(ConvertersManager.TelephoneConverter.Convert(
                        telephone)));

                var addedIndividualTelephone = _individualTelephonesRepository.Add(new IndividualTelephone()
                {
                    BeneficiaryId = beneficiaryId,
                    TelephoneId = addedTelephone.TelephoneId
                });

                if (addedTelephone == null || addedIndividualTelephone == null)
                    return null;

                addedTelephones.Add(addedTelephone);
            }
            if (addedTelephones.Count != telephonesToAdd.Count)
                return null;

            _telephonesRepository.Save();
            _individualTelephonesRepository.Save();

            return addedTelephones;
        }

        private List<AddressDomain> AddAddresses(List<AddressDomain> telephonesToAdd, Guid beneficiaryId)
        {
            if (telephonesToAdd.Count > 3)
                throw new Exception("Quantidade máxima de endereços excedida! ");

            var addedAddresses = new List<AddressDomain>();
            foreach (var address in telephonesToAdd)
            {
                var addedAddress = ConvertersManager.AddressConverter.Convert(
                    _addressRepository.Add(ConvertersManager.AddressConverter.Convert(
                        address)));

                var addedBeneficiaryAddress = _beneficiaryAddressRepository.Add(new BeneficiaryAddress()
                {
                    AddressId = addedAddress.AddressId,
                    BeneficiaryId = beneficiaryId
                });

                if (addedAddress == null || addedBeneficiaryAddress == null)
                    return null;

                addedAddresses.Add(addedAddress);
            }
            if (addedAddresses.Count != telephonesToAdd.Count)
                return null;

            _addressRepository.Save();
            _beneficiaryAddressRepository.Save();

            return addedAddresses;
        }
        #endregion Add

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ContractHolderDomain Find(Guid id)
        {
            var individual = _individualsRepository.Find(id);
            if (individual == null)
                return null;

            var contractHolder = new ContractHolderDomain();
            contractHolder.Individual = ConvertersManager.IndividualConverter.Convert(individual);

            contractHolder.IndividualAddresses = _addressRepository.Get().Where(add => _beneficiaryAddressRepository.Get()
                                                        .Where(ba => ba.BeneficiaryId == contractHolder.Individual.BeneficiaryId).Select(ba => ba.AddressId).Contains(add.AddressId))
                                                        .Select(add => ConvertersManager.AddressConverter.Convert(add)).ToList();

            contractHolder.IndividualTelephones = _telephonesRepository.Get().Where(tel => _individualTelephonesRepository.Get()
                                                        .Where(it => it.BeneficiaryId == contractHolder.Individual.BeneficiaryId).Select(it => it.TelephoneId).Contains(tel.TelephoneId))
                                                        .Select(tel => ConvertersManager.TelephoneConverter.Convert(tel)).ToList();
            return contractHolder;
        }

        public IEnumerable<ContractHolderDomain> Get()
        {
            var contractHolders = new List<ContractHolderDomain>();

            foreach(var individual in _individualsRepository.Get())
            {
                var contractHolderToAdd = new ContractHolderDomain();
                contractHolderToAdd.Individual = ConvertersManager.IndividualConverter.Convert(individual);

                contractHolderToAdd.IndividualAddresses = _addressRepository.Get().Where(add => _beneficiaryAddressRepository.Get()
                                                            .Where(ba => ba.BeneficiaryId == contractHolderToAdd.Individual.BeneficiaryId).Select(ba => ba.AddressId).Contains(add.AddressId))
                                                            .Select(add => ConvertersManager.AddressConverter.Convert(add)).ToList();

                contractHolderToAdd.IndividualTelephones = _telephonesRepository.Get().Where(tel => _individualTelephonesRepository.Get()
                                                            .Where(it => it.BeneficiaryId == contractHolderToAdd.Individual.BeneficiaryId).Select(it => it.TelephoneId).Contains(tel.TelephoneId))
                                                            .Select(tel => ConvertersManager.TelephoneConverter.Convert(tel)).ToList();
                contractHolders.Add(contractHolderToAdd);
            }

            return contractHolders;
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public ContractHolderDomain Update(Guid id, ContractHolderDomain updatedContractHolder)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var contractHolderToUpdate = Find(id);
                if (contractHolderToUpdate == null)
                    return null;

                // Individual
                contractHolderToUpdate.Individual = ConvertersManager.IndividualConverter.Convert(
                    _individualsRepository.Update(contractHolderToUpdate.Individual.BeneficiaryId, ConvertersManager.IndividualConverter.Convert(updatedContractHolder.Individual)));
                if (contractHolderToUpdate.Individual == null)
                    return null;
                _individualsRepository.Save();

                // Addresses
                contractHolderToUpdate.IndividualAddresses.RemoveAll(ad => updatedContractHolder.IndividualAddresses.Select(add => add.AddressId).Contains(ad.AddressId));
                updatedContractHolder.IndividualAddresses.RemoveAll(ad => ad.AddressId != Guid.Empty);
                foreach(var address in contractHolderToUpdate.IndividualAddresses)
                {
                    _beneficiaryAddressRepository.Remove(_beneficiaryAddressRepository.Get().FirstOrDefault(ba => ba.BeneficiaryId == id && ba.AddressId == address.AddressId).BeneficiaryId);
                    _beneficiaryAddressRepository.Save();
                }
                
                foreach (var address in updatedContractHolder.IndividualAddresses)
                {
                    var addedAddress = ConvertersManager.AddressConverter.Convert(
                        _addressRepository.Add(ConvertersManager.AddressConverter.Convert(address)));
                    if (_beneficiaryAddressRepository.Add(new BeneficiaryAddress()
                    {
                        AddressId = addedAddress.AddressId,
                        BeneficiaryId = id
                    }) == null)
                        return null;
                    _beneficiaryAddressRepository.Save();
                    _addressRepository.Save();
                    contractHolderToUpdate.IndividualAddresses.Add(addedAddress);
                }

                _beneficiaryAddressRepository.Save();
                _addressRepository.Save();
                
                // Telephones
                contractHolderToUpdate.IndividualTelephones.RemoveAll(tel => updatedContractHolder.IndividualTelephones.Select(tele => tele.TelephoneId).Contains(tel.TelephoneId));
                updatedContractHolder.IndividualTelephones.RemoveAll(ad => ad.TelephoneId != Guid.Empty);
                foreach (var telephone in contractHolderToUpdate.IndividualTelephones)
                {
                    _individualTelephonesRepository.Remove(_individualTelephonesRepository.Get().FirstOrDefault(it => it.BeneficiaryId == id && it.TelephoneId == telephone.TelephoneId).BeneficiaryId);
                    _individualTelephonesRepository.Save();
                }
                foreach (var telephone in updatedContractHolder.IndividualTelephones)
                {
                    var addedTelephone = ConvertersManager.TelephoneConverter.Convert(
                        _telephonesRepository.Add(ConvertersManager.TelephoneConverter.Convert(telephone)));
                    if (_individualTelephonesRepository.Add(new IndividualTelephone()
                    {
                        TelephoneId = addedTelephone.TelephoneId,
                        BeneficiaryId = id
                    }) == null)
                        return null;
                    _individualTelephonesRepository.Save();
                    _telephonesRepository.Save();
                    contractHolderToUpdate.IndividualTelephones.Add(addedTelephone);
                }

                _telephonesRepository.Save();
                _individualTelephonesRepository.Save();

                scope.Complete();
                return contractHolderToUpdate;
            }
        }
    }
}
