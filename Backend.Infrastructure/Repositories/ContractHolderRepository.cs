using System;
using System.Collections.Generic;
using System.Transactions;
using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Converters;
using Backend.Infrastructure.Repositories.Contracts;

namespace Backend.Infrastructure.Repositories
{
    public class ContractHolderRepository : IRepository<ContractHolderDomain>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<IndividualEntity> _individualsRepository;
        private readonly IRepository<TelephoneEntity> _telephonesRepository;
        private readonly IRepository<BeneficiaryTelephone> _beneficiaryTelephonesRepository;

        public ContractHolderRepository(ConfigurationContext db,
                                        IRepository<IndividualEntity> individualsRepository,
                                        IRepository<TelephoneEntity> telephonesRepository,
                                        IRepository<BeneficiaryTelephone> beneficiaryTelephonesRepository)
        {
            _db = db;

            _individualsRepository = individualsRepository;
            _telephonesRepository = telephonesRepository;
            _beneficiaryTelephonesRepository = beneficiaryTelephonesRepository;
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

                    if (contractHolder.Individual == null || !_individualsRepository.Save())
                        return null;

                    // Add Telephones and Individual Telephones
                    var addedTelephones = new List<TelephoneDomain>();
                    foreach(var telephone in contractHolder.IndividualTelephones)
                    {
                        var addedTelephone = ConvertersManager.TelephoneConverter.Convert(
                            _telephonesRepository.Add(ConvertersManager.TelephoneConverter.Convert(
                                telephone)));

                        var addedIndividualTelephone = _beneficiaryTelephonesRepository.Add(new BeneficiaryTelephone()
                        {
                            BeneficiaryId = contractHolder.Individual.BeneficiaryId,
                            TelephoneId = telephone.TelephoneId
                        });

                        if (addedTelephone == null || addedIndividualTelephone == null)
                            return null;

                        addedTelephones.Add(addedTelephone);
                    }
                    if (addedTelephones.Count != contractHolder.IndividualTelephones.Count
                        || _telephonesRepository.Save()
                        || _beneficiaryTelephonesRepository.Save())
                        return null;

                    contractHolder.IndividualTelephones = addedTelephones;
                    
                    // Add Addresses
                    //TODO
                    // Add Beneficiary Addresses
                }
                scope.Complete();
            }
                throw new NotImplementedException();
        }

        public ContractHolderDomain Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractHolderDomain> Get()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public ContractHolderDomain Update(Guid id, ContractHolderDomain t)
        {
            throw new NotImplementedException();
        }
    }
}
