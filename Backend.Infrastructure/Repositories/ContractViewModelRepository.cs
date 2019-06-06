﻿using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class ContractViewModelRepository : IReadOnlyRepository<ContractViewModel>, IWriteRepository<ContractViewModel>
    {
        private readonly ConfigurationContext _db;

        public ContractViewModelRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(ContractViewModel viewModel)
        {
            if(viewModel != null)
            {
                // Contract
                var contract = AddContract(viewModel);
                if (contract == null)
                    return false;

                // Signed Contract
                var signedContract = AddSignedContract(viewModel, contract);
                if (signedContract == null)
                    return false;
                
                // Contract Beneficiaries
                if (AddContractBeneficiaries(viewModel, signedContract))
                {
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private bool AddContractBeneficiaries(ContractViewModel viewModel, SignedContract signedContract)
        {
            var beneficiaries = new List<Guid>();
            switch (viewModel.Type)
            {
                case Core.Enums.ContractType.DentalPlan:
                case Core.Enums.ContractType.HealthPlan:
                case Core.Enums.ContractType.LifeInsurance:
                    beneficiaries = _db.Individuals
                        .Where(ind => viewModel.Beneficiaries.Contains(ind.BeneficiaryId))
                        .Select(ind => ind.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.AnimalHealthPlan:
                    beneficiaries = _db.Pets
                        .Where(pet => viewModel.Beneficiaries.Contains(pet.BeneficiaryId))
                        .Select(pet => pet.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.MobileDeviceInsurance:
                    beneficiaries = _db.MobileDevices
                        .Where(mob => viewModel.Beneficiaries.Contains(mob.BeneficiaryId))
                        .Select(mob => mob.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.RealStateInsurance:
                    beneficiaries = _db.Reaties
                        .Where(rea => viewModel.Beneficiaries.Contains(rea.BeneficiaryId))
                        .Select(rea => rea.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.VehicleInsurance:
                    beneficiaries = _db.Vehicles
                        .Where(vec => viewModel.Beneficiaries.Contains(vec.BeneficiaryId))
                        .Select(vec => vec.BeneficiaryId)
                        .ToList();
                    break;
                default:
                    return false;
            }
            if (beneficiaries.Count == 0 || beneficiaries.Count != viewModel.Beneficiaries.Count)
                return false;

            foreach (var ben in beneficiaries)
            {
                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.ContractSignedId,
                    ContractBeneficiaryId = Guid.NewGuid()
                };
                _db.Contract_Beneficiary.Add(contract_beneficiary);
            }
            return true;
        }

        private SignedContract AddSignedContract(ContractViewModel viewModel, Contract contract)
        {
            var contractHolder = _db
                .Individuals
                .Where(ind => ind.BeneficiaryId == viewModel.ContractHolderId)
                .FirstOrDefault();
            if (contractHolder == null)
                return null;

            var signedContract = new SignedContract()
            {
                ContractId = contract.ContractId,
                ContractSignedId = Guid.NewGuid(),
                IndividualId = contractHolder.BeneficiaryId,
                ContractIndividualIsActive = viewModel.IsActive
            };
            return _db.SignedContracts.Add(signedContract).Entity;
        }

        private Contract AddContract(ContractViewModel contractViewModel)
        {
            var contract = ViewModelCreator.ContractFactory.Create(contractViewModel);
            if(contract != null)
                return _db.Contracts.Add(contract).Entity;
            return null;
        }

        public ContractViewModel Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractViewModel> Get()
        {
            List<ContractViewModel> viewModelToReturn = new List<ContractViewModel>();

            var contracts = _db.Contracts.Where(con => !con.ContractDeleted).ToList();
            foreach (var contract in contracts)
            {
                var signedContracts = _db
                    .SignedContracts
                    .Where(sc => sc.ContractId == contract.ContractId)
                    .ToList();

                foreach (var signedContract in signedContracts)
                {
                    var beneficiaries = _db
                        .Contract_Beneficiary
                        .Where(cb => cb.SignedContractId == signedContract.ContractSignedId)
                        .Select(cb => cb.BeneficiaryId)
                        .ToList();
                    var viewModelToAdd = new ContractViewModel()
                    {
                        Category = contract.ContractCategory,
                        ExpiryDate = contract.ContractExpiryDate,
                        IsActive = signedContract.ContractIndividualIsActive,
                        Type = contract.ContractType,
                        ContractHolderId = signedContract.IndividualId,
                        Beneficiaries = beneficiaries
                    };
                    viewModelToReturn.Add(viewModelToAdd);
                }
            }
            return viewModelToReturn;
        }

        public ContractViewModel Remove(ContractViewModel t)
        {
            throw new NotImplementedException();
        }

        public ContractViewModel Update(ContractViewModel t)
        {
            throw new NotImplementedException();
        }
    }
}
