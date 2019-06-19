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
        public CompleteContract Update(Guid id, CompleteContract t)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                scope.Complete();
                throw new NotImplementedException();
            }
        }
        #endregion Update

        public CompleteContract Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompleteContract> Get()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
