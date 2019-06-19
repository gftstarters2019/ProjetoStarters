using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Backend.Infrastructure.Repositories
{
    public class CompleteContractRepository : IRepository<CompleteContract>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<Contract> _contractRepository;
        private readonly IRepository<SignedContract> _signedContractRepository;
        private readonly IRepository<Individual> _individualsRepository;
        private readonly IRepository<Pet> _petsRepository;
        private readonly IRepository<MobileDevice> _mobileDevicesRepository;
        private readonly IRepository<Realty> _realtiesRepository;
        private readonly IRepository<Vehicle> _vehiclesRepository;
        private readonly IRepository<ContractBeneficiary> _contractBeneficiaryRepository;

        public CompleteContractRepository(ConfigurationContext db,
                                          IRepository<Contract> contractRepository,
                                          IRepository<SignedContract> signedContractRepository,
                                          IRepository<Individual> individualsRepository,
                                          IRepository<Pet> petsRepository,
                                          IRepository<MobileDevice> mobileDevicesRepository,
                                          IRepository<Realty> realtiesRepository,
                                          IRepository<Vehicle> vehiclesRepository,
                                          IRepository<ContractBeneficiary> contractBeneficiaryRepository)
        {
            _db = db;
            _contractRepository = contractRepository;
            _signedContractRepository = signedContractRepository;
            _individualsRepository = individualsRepository;
            _petsRepository = petsRepository;
            _mobileDevicesRepository = mobileDevicesRepository;
            _realtiesRepository = realtiesRepository;
            _vehiclesRepository = vehiclesRepository;
            _contractBeneficiaryRepository = contractBeneficiaryRepository;
        }

        #region Add
        public CompleteContract Add(CompleteContract completeContract)
        {
            if (completeContract == null)
                return null;

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                // Add Contract to DB
                completeContract.Contract = _contractRepository.Add(completeContract.Contract);
                if (completeContract.Contract == null || !_contractRepository.Save())
                    return null;

                // Add Signed Contract to DB
                completeContract.SignedContract.SignedContractContract = completeContract.Contract;
                completeContract.SignedContract = _signedContractRepository.Add(completeContract.SignedContract);
                if (completeContract.SignedContract == null || !_signedContractRepository.Save())
                    return null;

                // Add Beneficiaries to DB and associate to Contract
                var addedBeneficiaries = AddBeneficiaries(completeContract);
                if (addedBeneficiaries == null)
                    return null;
                if (AddContractBeneficiaries(addedBeneficiaries,
                                             completeContract.SignedContract,
                                             completeContract.Contract.ContractType))
                {
                    _db.SaveChanges();
                    scope.Complete();
                    return completeContract;
                }
                return null;
            }
        }
        
        public List<Guid> AddBeneficiaries(CompleteContract completeContract)
        {
            switch (completeContract.Contract.ContractType)
            {
                case ContractType.DentalPlan:
                case ContractType.HealthPlan:
                case ContractType.LifeInsurance:
                    if (completeContract.Individuals.Count == 0)
                        return null;

                    var addedIndividuals = AddIndividuals(completeContract.Individuals);

                    return addedIndividuals;

                case ContractType.AnimalHealthPlan:
                    if (completeContract.Pets.Count == 0)
                        return null;

                    var addedPets = AddPets(completeContract.Pets);
                    
                    return addedPets;

                case ContractType.MobileDeviceInsurance:
                    if (completeContract.MobileDevices.Count == 0)
                        return null;

                    var addedMobileDevices = AddMobileDevices(completeContract.MobileDevices);

                    return addedMobileDevices;

                case ContractType.RealStateInsurance:
                    if (completeContract.Realties.Count == 0)
                        return null;

                    var addedRealties = AddRealties(completeContract.Realties);

                    return addedRealties;

                case ContractType.VehicleInsurance:
                    if (completeContract.Vehicles.Count == 0)
                        return null;

                    var addedVehicles = AddVehicles(completeContract.Vehicles);

                    return addedVehicles;

                default:
                    return null;
            }
        }

        private List<Guid> AddIndividuals(List<Individual> individuals)
        {
            List<Guid> insertedIndividuals = new List<Guid>();
            
            // Verifies if Individuals already have IDs, and if they do put them in the inserted list
            insertedIndividuals.AddRange(individuals.Where(ben => ben.BeneficiaryId != Guid.Empty).Select(ben => ben.BeneficiaryId));
            
            // Removes Individuals that already have IDs
            individuals.RemoveAll(ben => ben.BeneficiaryId != Guid.Empty);
            
            foreach (var ind in individuals)
            {
                var addedIndividual = _individualsRepository.Add(ind);

                if(addedIndividual != null)
                    insertedIndividuals.Add(addedIndividual.BeneficiaryId);
            }
            _individualsRepository.Save();
            if (insertedIndividuals.Count == individuals.Count)
                return insertedIndividuals;

            return null;
        }

        private List<Guid> AddPets(List<Pet> pets)
        {
            List<Guid> insertedPets = new List<Guid>();

            // Verifies if Pets already have IDs, and if they do put them in the inserted list
            insertedPets.AddRange(pets.Where(ben => ben.BeneficiaryId != Guid.Empty).Select(ben => ben.BeneficiaryId));
            
            // Removes Pets that already have IDs
            pets.RemoveAll(ben => ben.BeneficiaryId != Guid.Empty);

            foreach (var pet in pets)
            {
                var addedPet = _petsRepository.Add(pet);
                if(addedPet != null)
                    insertedPets.Add(pet.BeneficiaryId);
            }
            _petsRepository.Save();
            if (insertedPets.Count == pets.Count)
                return insertedPets;

            return null;
        }

        private List<Guid> AddMobileDevices(List<MobileDevice> mobileDevices)
        {
            List<Guid> insertedMobileDevices = new List<Guid>();

            // Verifies if MobileDevices already have IDs, and if they do put them in the inserted list
            insertedMobileDevices.AddRange(mobileDevices.Where(ben => ben.BeneficiaryId != Guid.Empty).Select(ben => ben.BeneficiaryId));

            // Removes MobileDevices that already have IDs
            mobileDevices.RemoveAll(ben => ben.BeneficiaryId != Guid.Empty);
            
            foreach (var mobile in mobileDevices)
            {
                var addedMobile = _mobileDevicesRepository.Add(mobile);
                if(addedMobile != null)
                    insertedMobileDevices.Add(addedMobile.BeneficiaryId);
            }
            _mobileDevicesRepository.Save();
            if (insertedMobileDevices.Count == mobileDevices.Count)
                return insertedMobileDevices;

            return null;
        }

        private List<Guid> AddRealties(List<Realty> realties)
        {
            List<Guid> insertedRealties = new List<Guid>();

            // Verifies if Realties already have IDs, and if they do put them in the inserted list
            insertedRealties.AddRange(realties.Where(ben => ben.BeneficiaryId != Guid.Empty).Select(ben => ben.BeneficiaryId));

            // Removes Realties that already have IDs
            realties.RemoveAll(ben => ben.BeneficiaryId != Guid.Empty);
            
            foreach (var realty in realties)
            {
                var addedRealty = _realtiesRepository.Add(realty);
                if (addedRealty != null)
                    insertedRealties.Add(addedRealty.BeneficiaryId);
            }
            _realtiesRepository.Save();
            if (insertedRealties.Count == realties.Count)
                return insertedRealties;

            return null;
        }

        private List<Guid> AddVehicles(List<Vehicle> vehicles)
        {
            List<Guid> insertedVehicles = new List<Guid>();

            // Verifies if Vehicles already have IDs, and if they do put them in the inserted list
            insertedVehicles.AddRange(vehicles.Where(ben => ben.BeneficiaryId != Guid.Empty).Select(ben => ben.BeneficiaryId));

            // Removes Vehicles that already have IDs
            vehicles.RemoveAll(ben => ben.BeneficiaryId != Guid.Empty);
            
            foreach (var vehicle in vehicles)
            {
                var addedVehicle = _vehiclesRepository.Add(vehicle);
                if (addedVehicle != null)
                    insertedVehicles.Add(addedVehicle.BeneficiaryId);
            }
            _vehiclesRepository.Save();
            if (insertedVehicles.Count == vehicles.Count)
                return insertedVehicles;

            return null;
        }

        private bool AddContractBeneficiaries(List<Guid> beneficiariesGuidList, SignedContract signedContract, ContractType contractType)
        {
            if (beneficiariesGuidList == null)
                return false;

            foreach (var ben in beneficiariesGuidList)
            {
                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    SignedContract = signedContract
                };
                _contractBeneficiaryRepository.Add(contract_beneficiary);
            }
            _contractBeneficiaryRepository.Save();

            if(beneficiariesGuidList.Count == _db.Contract_Beneficiary
                                                 .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
                                                 .Count())
                return true;
            else
                return false;
        }
        #endregion Add

        #region Update
        public CompleteContract Update(Guid id, CompleteContract completeContract)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var updatedSignedContract = _signedContractRepository.Update(id, completeContract.SignedContract);
                if (!_signedContractRepository.Save())
                    return null;

                var updatedContract = _contractRepository.Update(updatedSignedContract.ContractId, completeContract.Contract);
                if (!_contractRepository.Save())
                    return null;

                var updatedBeneficiaries = UpdatedBeneficiaries(id, completeContract);
                if (updatedBeneficiaries == null)
                    return null;
                //
                scope.Complete();
                throw new NotImplementedException();
            }
        }

        private List<Guid> UpdatedBeneficiaries(Guid id, CompleteContract completeContract)
        {
            switch (completeContract.Contract.ContractType)
            {
                case ContractType.DentalPlan:
                case ContractType.HealthPlan:
                case ContractType.LifeInsurance:
                    if (completeContract.Individuals.Count == 0)
                        return null;

                    var updatedIndividuals = UpdateIndividuals(completeContract.Individuals);

                    return updatedIndividuals;

                case ContractType.AnimalHealthPlan:
                    if (completeContract.Pets.Count == 0)
                        return null;

                    var updatedPets = UpdatePets(completeContract.Pets);

                    return updatedPets;

                case ContractType.MobileDeviceInsurance:
                    if (completeContract.MobileDevices.Count == 0)
                        return null;

                    var updatedMobileDevices = UpdateMobileDevices(completeContract.MobileDevices);

                    return updatedMobileDevices;

                case ContractType.RealStateInsurance:
                    if (completeContract.Realties.Count == 0)
                        return null;

                    var updatedRealties = UpdateRealties(completeContract.Realties);

                    return updatedRealties;

                case ContractType.VehicleInsurance:
                    if (completeContract.Vehicles.Count == 0)
                        return null;

                    var updatedVehicles = UpdateVehicles(completeContract.Vehicles);

                    return updatedVehicles;

                default:
                    return null;
            }
        }

        private List<Guid> UpdateIndividuals(List<Individual> individuals)
        {
            List<Guid> updatedIndividuals = new List<Guid>();

            foreach (var ind in individuals)
            {
                // If Individual doesn't have an ID, add it do the DB
                if (ind.BeneficiaryId == Guid.Empty)
                {
                    var addedIndividual = _individualsRepository.Add(ind);

                    if (addedIndividual != null)
                        updatedIndividuals.Add(addedIndividual.BeneficiaryId);
                }
                // If Individual already has an ID, update it
                else
                {
                    var updatedIndividual = _individualsRepository.Update(ind.BeneficiaryId, ind);
                    if(updatedIndividual != null)
                        updatedIndividuals.Add(updatedIndividual.BeneficiaryId);
                }
            }
            _individualsRepository.Save();
            if (updatedIndividuals.Count == individuals.Count)
                return updatedIndividuals;

            return null;
        }

        private List<Guid> UpdatePets(List<Pet> pets)
        {
            List<Guid> updatedPets = new List<Guid>();

            foreach (var pet in pets)
            {
                // If Pet doesn't have an ID, add it do the DB
                if (pet.BeneficiaryId == Guid.Empty)
                {
                    var addedPet = _petsRepository.Add(pet);

                    if (addedPet != null)
                        updatedPets.Add(addedPet.BeneficiaryId);
                }
                // If Pet already has an ID, update it
                else
                {
                    var updatedPet = _petsRepository.Update(pet.BeneficiaryId, pet);
                    if (updatedPet != null)
                        updatedPets.Add(updatedPet.BeneficiaryId);
                }
            }
            _petsRepository.Save();
            if (updatedPets.Count == pets.Count)
                return updatedPets;

            return null;
        }

        private List<Guid> UpdateMobileDevices(List<MobileDevice> mobileDevices)
        {
            List<Guid> updatedMobileDevices = new List<Guid>();

            foreach (var mobileDevice in mobileDevices)
            {
                // If MobileDevice doesn't have an ID, add it do the DB
                if (mobileDevice.BeneficiaryId == Guid.Empty)
                {
                    var addedMobileDevice = _mobileDevicesRepository.Add(mobileDevice);

                    if (addedMobileDevice != null)
                        updatedMobileDevices.Add(addedMobileDevice.BeneficiaryId);
                }
                // If MobileDevice already has an ID, update it
                else
                {
                    var updatedMobileDevice = _mobileDevicesRepository.Update(mobileDevice.BeneficiaryId, mobileDevice);
                    if (updatedMobileDevice != null)
                        updatedMobileDevices.Add(updatedMobileDevice.BeneficiaryId);
                }
            }
            _mobileDevicesRepository.Save();
            if (updatedMobileDevices.Count == mobileDevices.Count)
                return updatedMobileDevices;

            return null;
        }

        private List<Guid> UpdateRealties(List<Realty> realties)
        {
            List<Guid> updatedRealties = new List<Guid>();

            foreach (var realty in realties)
            {
                // If Realty doesn't have an ID, add it do the DB
                if (realty.BeneficiaryId == Guid.Empty)
                {
                    var addedRealty = _realtiesRepository.Add(realty);

                    if (addedRealty != null)
                        updatedRealties.Add(addedRealty.BeneficiaryId);
                }
                // If Realty already has an ID, update it
                else
                {
                    var updatedRealty = _realtiesRepository.Update(realty.BeneficiaryId, realty);
                    if (updatedRealty != null)
                        updatedRealties.Add(updatedRealty.BeneficiaryId);
                }
            }
            _realtiesRepository.Save();
            if (updatedRealties.Count == realties.Count)
                return updatedRealties;

            return null;
        }

        private List<Guid> UpdateVehicles(List<Vehicle> vehicles)
        {
            List<Guid> updatedVehicles = new List<Guid>();

            foreach (var vehicle in vehicles)
            {
                // If Vehicle doesn't have an ID, add it do the DB
                if (vehicle.BeneficiaryId == Guid.Empty)
                {
                    var addedVehicle = _vehiclesRepository.Add(vehicle);

                    if (addedVehicle != null)
                        updatedVehicles.Add(addedVehicle.BeneficiaryId);
                }
                // If Vehicle already has an ID, update it
                else
                {
                    var updatedVehicle = _vehiclesRepository.Update(vehicle.BeneficiaryId, vehicle);
                    if (updatedVehicle != null)
                        updatedVehicles.Add(updatedVehicle.BeneficiaryId);
                }
            }
            _vehiclesRepository.Save();
            if (updatedVehicles.Count == vehicles.Count)
                return updatedVehicles;

            return null;
        }
        #endregion Update

        public CompleteContract Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompleteContract> Get()
        {
            List<CompleteContract> completeContracts = new List<CompleteContract>();

            var contracts = _contractRepository.Get();

            //
            //List<ContractViewModel> viewModelToReturn = new List<ContractViewModel>();

            //var contracts = _db.Contracts
            //    .Where(con => !con.ContractDeleted)
            //    .ToList();
            //foreach (var contract in contracts)
            //{
            //    var signedContracts = _db
            //        .SignedContracts
            //        .Where(sc => sc.ContractId == contract.ContractId)
            //        .ToList();

            //    foreach (var signedContract in signedContracts)
            //    {
            //        var beneficiaries = _db
            //            .Contract_Beneficiary
            //            .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
            //            .Select(cb => cb.BeneficiaryId)
            //            .ToList();

            //        //
            //        //ContractViewModel viewModelToAdd;
            //        switch (contract.ContractType)
            //        {
            //            case Core.Enums.ContractType.DentalPlan:
            //            case Core.Enums.ContractType.HealthPlan:
            //            case Core.Enums.ContractType.LifeInsurance:
            //                var viewModelIndividualToAdd = new ContractViewModel()
            //                {
            //                    Category = contract.ContractCategory,
            //                    ExpiryDate = contract.ContractExpiryDate,
            //                    IsActive = signedContract.ContractIndividualIsActive,
            //                    Type = contract.ContractType,
            //                    SignedContractId = signedContract.SignedContractId,
            //                    ContractHolderId = signedContract.IndividualId,
            //                    ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
            //                    Individuals = _db.Individuals.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
            //                };
            //                viewModelToReturn.Add(viewModelIndividualToAdd);
            //                break;

            //            case Core.Enums.ContractType.AnimalHealthPlan:
            //                var viewModelPetToAdd = new ContractViewModel()
            //                {
            //                    Category = contract.ContractCategory,
            //                    ExpiryDate = contract.ContractExpiryDate,
            //                    IsActive = signedContract.ContractIndividualIsActive,
            //                    Type = contract.ContractType,
            //                    SignedContractId = signedContract.SignedContractId,
            //                    ContractHolderId = signedContract.IndividualId,
            //                    ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
            //                    Pets = _db.Pets.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
            //                };
            //                viewModelToReturn.Add(viewModelPetToAdd);
            //                break;

            //            case Core.Enums.ContractType.MobileDeviceInsurance:
            //                var viewModelMobileDeviceToAdd = new ContractViewModel()
            //                {
            //                    Category = contract.ContractCategory,
            //                    ExpiryDate = contract.ContractExpiryDate,
            //                    IsActive = signedContract.ContractIndividualIsActive,
            //                    Type = contract.ContractType,
            //                    SignedContractId = signedContract.SignedContractId,
            //                    ContractHolderId = signedContract.IndividualId,
            //                    ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
            //                    MobileDevices = _db.MobileDevices.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
            //                };
            //                viewModelToReturn.Add(viewModelMobileDeviceToAdd);
            //                break;

            //            case Core.Enums.ContractType.RealStateInsurance:
            //                var contractRealties = _db.Realties.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList();
            //                List<RealtyViewModel> realtiesToReturn = new List<RealtyViewModel>();
            //                foreach (var real in contractRealties)
            //                {
            //                    realtiesToReturn.Add(new RealtyViewModel()
            //                    {
            //                        Id = real.BeneficiaryId,
            //                        ConstructionDate = real.RealtyConstructionDate,
            //                        MarketValue = real.RealtyMarketValue,
            //                        MunicipalRegistration = real.RealtyMunicipalRegistration,
            //                        SaleValue = real.RealtySaleValue,
            //                        Address = _db
            //                        .Addresses
            //                        .Where(a => a.AddressId == _db.Beneficiary_Address
            //                                                    .Where(ba => ba.BeneficiaryId == real.BeneficiaryId)
            //                                                    .Select(ba => ba.AddressId)
            //                                                    .FirstOrDefault())
            //                        .FirstOrDefault()
            //                    });
            //                }
            //                var viewModelRealtyToAdd = new ContractViewModel()
            //                {
            //                    Category = contract.ContractCategory,
            //                    ExpiryDate = contract.ContractExpiryDate,
            //                    IsActive = signedContract.ContractIndividualIsActive,
            //                    Type = contract.ContractType,
            //                    SignedContractId = signedContract.SignedContractId,
            //                    ContractHolderId = signedContract.IndividualId,
            //                    ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
            //                    Realties = realtiesToReturn
            //                };
            //                viewModelToReturn.Add(viewModelRealtyToAdd);
            //                break;

            //            case Core.Enums.ContractType.VehicleInsurance:
            //                var viewModelVehicleToAdd = new ContractViewModel()
            //                {
            //                    Category = contract.ContractCategory,
            //                    ExpiryDate = contract.ContractExpiryDate,
            //                    IsActive = signedContract.ContractIndividualIsActive,
            //                    Type = contract.ContractType,
            //                    SignedContractId = signedContract.SignedContractId,
            //                    ContractHolderId = signedContract.IndividualId,
            //                    ContractHolder = _db.Individuals.Where(ind => ind.BeneficiaryId == signedContract.IndividualId).FirstOrDefault(),
            //                    Vehicles = _db.Vehicles.Where(ind => beneficiaries.Contains(ind.BeneficiaryId)).ToList()
            //                };
            //                viewModelToReturn.Add(viewModelVehicleToAdd);
            //                break;

            //            default:
            //                break;
            //        }
            //    }
            //}
            //return viewModelToReturn;
            //
            throw new NotImplementedException();
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

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
