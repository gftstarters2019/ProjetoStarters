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

        public ContractHolderDomain Add(ContractHolderDomain contractHolder)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if(contractHolder != null)
                {
                    // Add Individual
                    contractHolder.Individual = ConvertersManager.IndividualConverter.Convert(
                        _individualsRepository.Add(ConvertersManager.IndividualConverter.Convert(
                            contractHolder.Individual)));

                    if (contractHolder.Individual == null)
                        return null;
                    _individualsRepository.Save();

                    // Add Telephones and Individual Telephones
                    if (contractHolder.IndividualTelephones.Count > 5)
                        return null;

                    var addedTelephones = new List<TelephoneDomain>();
                    foreach(var telephone in contractHolder.IndividualTelephones)
                    {
                        var addedTelephone = ConvertersManager.TelephoneConverter.Convert(
                            _telephonesRepository.Add(ConvertersManager.TelephoneConverter.Convert(
                                telephone)));

                        var addedIndividualTelephone = _individualTelephonesRepository.Add(new IndividualTelephone()
                        {
                            BeneficiaryId = contractHolder.Individual.BeneficiaryId,
                            TelephoneId = addedTelephone.TelephoneId
                        });

                        if (addedTelephone == null || addedIndividualTelephone == null)
                            return null;

                        addedTelephones.Add(addedTelephone);
                    }
                    if (addedTelephones.Count != contractHolder.IndividualTelephones.Count)
                        return null;
                    _telephonesRepository.Save();
                    _individualTelephonesRepository.Save();


                    contractHolder.IndividualTelephones = addedTelephones;

                    // Add Addresses
                    if (contractHolder.IndividualAddresses.Count > 3)
                        return null;

                    var addedAddresses = new List<AddressDomain>();
                    foreach (var address in contractHolder.IndividualAddresses)
                    {
                        var addedAddress = ConvertersManager.AddressConverter.Convert(
                            _addressRepository.Add(ConvertersManager.AddressConverter.Convert(
                                address)));

                        var addedBeneficiaryAddress = _beneficiaryAddressRepository.Add(new BeneficiaryAddress()
                        {
                            AddressId = addedAddress.AddressId,
                            BeneficiaryId = contractHolder.Individual.BeneficiaryId
                        });

                        if (addedAddress == null || addedBeneficiaryAddress == null)
                            return null;

                        addedAddresses.Add(addedAddress);
                    }
                    if (addedAddresses.Count != contractHolder.IndividualAddresses.Count)
                        return null;
                    _addressRepository.Save();
                    _beneficiaryAddressRepository.Save();

                    contractHolder.IndividualAddresses = addedAddresses;

                    scope.Complete();
                    return contractHolder;
                }
                return null;
            }
        }

        public ContractHolderDomain Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractHolderDomain> Get()
        {
            var contractHolders = new List<ContractHolderDomain>();

            foreach(var individual in _db.Individuals)
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

        public ContractHolderDomain Update(Guid id, ContractHolderDomain t)
        {
            throw new NotImplementedException();
        }
    }
}
