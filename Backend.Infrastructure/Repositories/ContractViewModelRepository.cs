using Backend.Application.ModelValidations;
using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;

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
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (viewModel != null)
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
                        scope.Complete();
                        return true;
                    }
                }
                scope.Complete();
                return false;
            }
        }

        private bool AddContractBeneficiaries(ContractViewModel viewModel, SignedContract signedContract)
        {
            var beneficiaries = AddBeneficiaries(viewModel);

            if (beneficiaries == null)
                return false;

            foreach (var ben in beneficiaries)
            {
                var signedContracts = _db
                                      .SignedContracts
                                      .Where(sc => sc.SignedContractId == _db
                                                                          .Contract_Beneficiary
                                                                          .Where(cb => cb.BeneficiaryId == ben)
                                                                          .Select(cb => cb.SignedContractId).FirstOrDefault()
                     && sc.ContractIndividualIsActive)
                     .ToList();

                foreach (var beneficiarySignedContract in signedContracts)
                {
                    if (_db.Contracts.Where(con => con.ContractId == beneficiarySignedContract.ContractId && con.ContractType == viewModel.Type).Any())
                        return false;
                }

                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    ContractBeneficiaryId = Guid.NewGuid()
                };
                _db.Contract_Beneficiary.Add(contract_beneficiary);
            }
            return true;
        }

        private List<Guid> AddBeneficiaries(ContractViewModel viewModel)
        {
            switch (viewModel.Type)
            {
                case Core.Enums.ContractType.DentalPlan:
                case Core.Enums.ContractType.HealthPlan:
                case Core.Enums.ContractType.LifeInsurance:
                    if (viewModel.Individuals.Count == 0)
                        return null;
                    return AddIndividuals(viewModel.Individuals);

                case Core.Enums.ContractType.AnimalHealthPlan:
                    if (viewModel.Pets.Count == 0)
                        return null;
                    return AddPets(viewModel.Pets);

                case Core.Enums.ContractType.MobileDeviceInsurance:
                    if (viewModel.MobileDevices.Count == 0)
                        return null;
                    return AddMobileDevices(viewModel.MobileDevices);

                case Core.Enums.ContractType.RealStateInsurance:
                    if (viewModel.Realties.Count == 0)
                        return null;
                    return AddRealties(viewModel.Realties);

                case Core.Enums.ContractType.VehicleInsurance:
                    if (viewModel.Vehicles.Count == 0)
                        return null;
                    return AddVehicles(viewModel.Vehicles);

                default:
                    return null;
            }
        }

        #region Add Beneficiaries To DB
        private List<Guid> AddVehicles(List<Vehicle> vehicles)
        {
            // Verifies if Chassis Number is already in DB
            if (_db.Vehicles
                    .Select(veh => veh.VehicleChassisNumber)
                    .Where(cha => vehicles.Select(veh => veh.VehicleChassisNumber).Contains(cha))
                    .ToList().Count > 0)
                return null;

            List<Guid> insertedVehicles = new List<Guid>();

            foreach (var vehicle in vehicles)
            {
                vehicle.IsDeleted = false;
                if (VehicleValidations.VehicleIsValid(vehicle))
                    insertedVehicles.Add(_db.Vehicles.Add(vehicle).Entity.BeneficiaryId);
            }
            if (insertedVehicles.Count == vehicles.Count)
                return insertedVehicles;
            return null;
        }

        private List<Guid> AddRealties(List<RealtyViewModel> realties)
        {
            // Verifies if Municipal Registration is already in DB
            if (_db.Realties
                    .Select(real => real.RealtyMunicipalRegistration)
                    .Where(reg => realties.Select(real => real.MunicipalRegistration).Contains(reg))
                    .ToList().Count > 0)
                return null;

            List<Guid> insertedRealties = new List<Guid>();

            foreach (var realty in realties)
            {
                realty.IsDeleted = false;
                Address realtyAddress = new Address()
                {
                    AddressCity = realty.AddressCity,
                    AddressComplement = realty.AddressComplement,
                    AddressCountry = realty.AddressCountry,
                    AddressNeighborhood = realty.AddressNeighborhood,
                    AddressNumber = realty.AddressNumber,
                    AddressState = realty.AddressState,
                    AddressStreet = realty.AddressStreet,
                    AddressType = realty.AddressType,
                    AddressZipCode = realty.AddressZipCode
                };
                //if (AddressValidations.AddressIsValid(realtyAddress))
                realtyAddress = _db.Addresses.Add(realtyAddress).Entity;
                //else return null;

                Realty realtyToAdd = new Realty()
                {
                    IsDeleted = realty.IsDeleted,
                    RealtyConstructionDate = realty.ConstructionDate,
                    RealtyMarketValue = realty.MarketValue,
                    RealtyMunicipalRegistration = realty.MunicipalRegistration,
                    RealtySaleValue = realty.SaleValue
                };

                if (RealtyValidations.RealtyIsValid(realtyToAdd))
                {
                    realtyToAdd = _db.Realties.Add(realtyToAdd).Entity;
                    insertedRealties.Add(realtyToAdd.BeneficiaryId);
                }
                else return null;

                _db.Beneficiary_Address.Add(new BeneficiaryAddress()
                {
                    AddressId = realtyAddress.AddressId,
                    BeneficiaryId = realtyToAdd.BeneficiaryId,
                    BeneficiaryAddressId = Guid.NewGuid()
                });
            }
            if (insertedRealties.Count == realties.Count)
                return insertedRealties;
            return null;
        }

        private List<Guid> AddMobileDevices(List<MobileDevice> mobileDevices)
        {
            // Verifies if Serial Number is already in DB
            if (_db.MobileDevices
                    .Select(mob => mob.MobileDeviceSerialNumber)
                    .Where(serial => mobileDevices.Select(mob => mob.MobileDeviceSerialNumber).Contains(serial))
                    .ToList().Count > 0)
                return null;

            List<Guid> insertedMobileDevices = new List<Guid>();

            foreach (var mobile in mobileDevices)
            {
                mobile.IsDeleted = false;
                if (MobileDeviceValidations.MobileDeviceIsValid(mobile))
                    insertedMobileDevices.Add(_db.MobileDevices.Add(mobile).Entity.BeneficiaryId);
            }
            if (insertedMobileDevices.Count == mobileDevices.Count)
                return insertedMobileDevices;
            return null;
        }

        private List<Guid> AddPets(List<Pet> pets)
        {
            List<Guid> insertedPets = new List<Guid>();

            foreach (var pet in pets)
            {
                pet.IsDeleted = false;
                if (PetValidations.PetIsValid(pet))
                    insertedPets.Add(_db.Pets.Add(pet).Entity.BeneficiaryId);
            }
            if (insertedPets.Count == pets.Count)
                return insertedPets;
            return null;
        }

        private List<Guid> AddIndividuals(List<Individual> individuals)
        {
            // Verifies if CPF is already in DB
            if (_db.Individuals
                    .Select(ind => ind.IndividualCPF)
                    .Where(cpf => individuals.Select(ind => ind.IndividualCPF).Contains(cpf))
                    .ToList().Count > 0)
                return null;

            List<Guid> insertedIndividuals = new List<Guid>();

            foreach (var ind in individuals)
            {
                ind.IsDeleted = false;
                if (IndividualValidations.IndividualIsValid(ind))
                    insertedIndividuals.Add(_db.Individuals.Add(ind).Entity.BeneficiaryId);
            }
            if (insertedIndividuals.Count == individuals.Count)
                return insertedIndividuals;
            return null;
        }
        #endregion Add Beneficiaries To DB

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
                SignedContractId = Guid.NewGuid(),
                IndividualId = contractHolder.BeneficiaryId,
                ContractIndividualIsActive = viewModel.IsActive
            };
            return _db.SignedContracts.Add(signedContract).Entity;
        }

        private Contract AddContract(ContractViewModel contractViewModel)
        {
            var contract = Factories.ContractFactory.Create(contractViewModel);
            if (contract != null)
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

            var contracts = _db.Contracts
                .Where(con => !con.ContractDeleted)
                .ToList();
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
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
                        .Select(cb => cb.BeneficiaryId)
                        .ToList();

                    //
                    //ContractViewModel viewModelToAdd;
                    switch (contract.ContractType)
                    {
                        case Core.Enums.ContractType.DentalPlan:
                        case Core.Enums.ContractType.HealthPlan:
                        case Core.Enums.ContractType.LifeInsurance:
                            var viewModelIndividualToAdd = new ContractViewModel()
                            {
                                Category = contract.ContractCategory,
                                ExpiryDate = contract.ContractExpiryDate,
                                IsActive = signedContract.ContractIndividualIsActive,
                                Type = contract.ContractType,
                                SignedContractId = signedContract.SignedContractId,
                                ContractHolderId = signedContract.IndividualId,
                                ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
                                Individuals = _db.Individuals.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
                            };
                            viewModelToReturn.Add(viewModelIndividualToAdd);
                            break;

                        case Core.Enums.ContractType.AnimalHealthPlan:
                            var viewModelPetToAdd = new ContractViewModel()
                            {
                                Category = contract.ContractCategory,
                                ExpiryDate = contract.ContractExpiryDate,
                                IsActive = signedContract.ContractIndividualIsActive,
                                Type = contract.ContractType,
                                SignedContractId = signedContract.SignedContractId,
                                ContractHolderId = signedContract.IndividualId,
                                ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
                                Pets = _db.Pets.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
                            };
                            viewModelToReturn.Add(viewModelPetToAdd);
                            break;

                        case Core.Enums.ContractType.MobileDeviceInsurance:
                            var viewModelMobileDeviceToAdd = new ContractViewModel()
                            {
                                Category = contract.ContractCategory,
                                ExpiryDate = contract.ContractExpiryDate,
                                IsActive = signedContract.ContractIndividualIsActive,
                                Type = contract.ContractType,
                                SignedContractId = signedContract.SignedContractId,
                                ContractHolderId = signedContract.IndividualId,
                                ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
                                MobileDevices = _db.MobileDevices.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
                            };
                            viewModelToReturn.Add(viewModelMobileDeviceToAdd);
                            break;

                        case Core.Enums.ContractType.RealStateInsurance:
                            var contractRealties = _db.Realties.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList();
                            List<RealtyViewModel> realtiesToReturn = new List<RealtyViewModel>();
                            foreach (var real in contractRealties)
                            {
                                realtiesToReturn.Add(new RealtyViewModel()
                                {
                                    Id = real.BeneficiaryId,
                                    ConstructionDate = real.RealtyConstructionDate,
                                    MarketValue = real.RealtyMarketValue,
                                    MunicipalRegistration = real.RealtyMunicipalRegistration,
                                    SaleValue = real.RealtySaleValue,
                                    Address = _db
                                    .Addresses
                                    .Where(a => a.AddressId == _db.Beneficiary_Address
                                                                .Where(ba => ba.BeneficiaryId == real.BeneficiaryId)
                                                                .Select(ba => ba.AddressId)
                                                                .FirstOrDefault())
                                    .FirstOrDefault()
                                });
                            }
                            var viewModelRealtyToAdd = new ContractViewModel()
                            {
                                Category = contract.ContractCategory,
                                ExpiryDate = contract.ContractExpiryDate,
                                IsActive = signedContract.ContractIndividualIsActive,
                                Type = contract.ContractType,
                                SignedContractId = signedContract.SignedContractId,
                                ContractHolderId = signedContract.IndividualId,
                                ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
                                Realties = realtiesToReturn
                            };
                            viewModelToReturn.Add(viewModelRealtyToAdd);
                            break;

                        case Core.Enums.ContractType.VehicleInsurance:
                            var viewModelVehicleToAdd = new ContractViewModel()
                            {
                                Category = contract.ContractCategory,
                                ExpiryDate = contract.ContractExpiryDate,
                                IsActive = signedContract.ContractIndividualIsActive,
                                Type = contract.ContractType,
                                SignedContractId = signedContract.SignedContractId,
                                ContractHolderId = signedContract.IndividualId,
                                ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
                                Vehicles = _db.Vehicles.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
                            };
                            viewModelToReturn.Add(viewModelVehicleToAdd);
                            break;

                        default:
                            break;
                    }
                }
            }
            return viewModelToReturn;
        }

        public bool Remove(Guid id)
        {
            var contractToDelete = _db
                .SignedContracts
                .Where(sc => sc.SignedContractId == id)
                .FirstOrDefault()
                .ContractId;

            if (_db.SignedContracts
                .Where(sc => sc.ContractId == contractToDelete && sc.ContractIndividualIsActive)
                .Count() > 0)
                return false;

            _db.Contracts.Remove(_db.Contracts.Where(c => c.ContractId == contractToDelete).FirstOrDefault());
            _db.SaveChanges();

            return true;
        }

        public ContractViewModel Update(Guid id, ContractViewModel contractViewModel)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var contractId = _db.SignedContracts.Where(sc => sc.SignedContractId == id).Select(sc => sc.ContractId).FirstOrDefault();

                if (contractViewModel != null)
                {
                    // Contract
                    var contractToUpdate = _db.Contracts.Where(con => con.ContractId == contractId).FirstOrDefault();
                    var updatedContract = Factories.ContractFactory.Create(contractViewModel);
                    contractToUpdate.ContractCategory = updatedContract.ContractCategory;
                    contractToUpdate.ContractExpiryDate = updatedContract.ContractExpiryDate;
                    contractToUpdate.ContractType = updatedContract.ContractType;
                    _db.Update(contractToUpdate);

                    // Signed Contract
                    var signedContractToUpdate = _db.SignedContracts.Where(sc => sc.SignedContractId == id).FirstOrDefault();
                    signedContractToUpdate.ContractIndividualIsActive = contractViewModel.IsActive;
                    
                    if (_db.Individuals.Select(ind => ind.BeneficiaryId).Contains(contractViewModel.ContractHolderId))
                        signedContractToUpdate.IndividualId = contractViewModel.ContractHolderId;
                    else
                    {
                        scope.Complete();
                        return null;
                    }

                    // Contract Beneficiaries
                    _db.Contract_Beneficiary.RemoveRange(
                        _db.Contract_Beneficiary.Where(cb => cb.SignedContractId == id));
                    
                    if(AddContractBeneficiaries(contractViewModel, signedContractToUpdate))
                    {
                        scope.Complete();
                        _db.SaveChanges();
                        return contractViewModel;
                    }
                    
                }
                
                scope.Complete();
                return null;
            }
        }

        private ContractViewModel UpdateContract(Guid id, ContractViewModel contractViewModel)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var contractToUpdate = _db.SignedContracts.Where(sc => sc.SignedContractId == id).FirstOrDefault();

                contractToUpdate.ContractIndividualIsActive = contractViewModel.IsActive;

                var beneficiariesToDelete = _db
                    .Contract_Beneficiary
                    .Where(cb => cb.SignedContractId == contractToUpdate.SignedContractId)
                    .ToList();

                _db.RemoveRange(beneficiariesToDelete);

                if (UpdateBeneficiaries(contractViewModel, contractToUpdate.SignedContractId))
                {
                    _db.SaveChanges();
                    return contractViewModel;
                }

                scope.Complete();
                return null;
            }
        }

        private bool UpdateBeneficiaries(ContractViewModel viewModel, Guid signedContractId)
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
                    beneficiaries = _db.Realties
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
                var signedContracts = _db
                      .SignedContracts
                      .Where(sc => sc.SignedContractId == _db
                                                          .Contract_Beneficiary
                                                          .Where(cb => cb.BeneficiaryId == ben)
                                                          .Select(cb => cb.SignedContractId).FirstOrDefault()
                    && sc.ContractIndividualIsActive)
                    .ToList();

                foreach (var beneficiarySignedContract in signedContracts)
                {
                    if (_db.Contracts.Where(con => con.ContractId == beneficiarySignedContract.ContractId && con.ContractType == viewModel.Type).Any())
                        return false;
                }

                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContractId,
                    ContractBeneficiaryId = Guid.NewGuid()
                };
                _db.Contract_Beneficiary.Add(contract_beneficiary);
            }
            return true;
        }
    }
}
