using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Converters;
using Backend.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Backend.Infrastructure.Repositories
{
    public class CompleteContractRepository : IRepository<CompleteContractDomain>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<ContractEntity> _contractRepository;
        private readonly IRepository<SignedContractEntity> _signedContractRepository;
        private readonly IRepository<IndividualEntity> _individualsRepository;
        private readonly IRepository<PetEntity> _petsRepository;
        private readonly IRepository<MobileDeviceEntity> _mobileDevicesRepository;
        private readonly IRepository<RealtyEntity> _realtiesRepository;
        private readonly IRepository<VehicleEntity> _vehiclesRepository;
        private readonly IRepository<ContractBeneficiary> _contractBeneficiaryRepository;

        public CompleteContractRepository(ConfigurationContext db,
                                          IRepository<ContractEntity> contractRepository,
                                          IRepository<SignedContractEntity> signedContractRepository,
                                          IRepository<IndividualEntity> individualsRepository,
                                          IRepository<PetEntity> petsRepository,
                                          IRepository<MobileDeviceEntity> mobileDevicesRepository,
                                          IRepository<RealtyEntity> realtiesRepository,
                                          IRepository<VehicleEntity> vehiclesRepository,
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
        public CompleteContractDomain Add(CompleteContractDomain completeContract)
        {
            if (completeContract == null)
                return null;

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                // Add Contract to DB
                completeContract.Contract = ConvertersManager.ContractConverter.Convert(
                    _contractRepository.Add(ConvertersManager.ContractConverter.Convert(
                        completeContract.Contract)));

                if (completeContract.Contract == null)
                    return null;
                _contractRepository.Save();

                // Add Signed Contract to DB
                completeContract.SignedContract.SignedContractContract = completeContract.Contract;
                completeContract.SignedContract = ConvertersManager.SignedContractConverter.Convert(
                    _signedContractRepository.Add(ConvertersManager.SignedContractConverter.Convert(
                        completeContract.SignedContract)));

                if (completeContract.SignedContract == null)
                    return null;
                _signedContractRepository.Save();

                // Add Beneficiaries to DB and associate to Contract
                var addedBeneficiaries = AddBeneficiaries(ref completeContract);
                if (addedBeneficiaries == null)
                    return null;
                if (AddContractBeneficiaries(addedBeneficiaries,
                                             completeContract.SignedContract))
                {
                    _db.SaveChanges();
                    scope.Complete();
                    return completeContract;
                }
                return null;
            }
        }
        
        public List<Guid> AddBeneficiaries(ref CompleteContractDomain completeContract)
        {
            switch (completeContract.Contract.ContractType)
            {
                case ContractType.DentalPlan:
                case ContractType.HealthPlan:
                case ContractType.LifeInsurance:
                    if (completeContract.Individuals.Count == 0)
                        return null;

                    completeContract.Individuals = AddIndividuals(completeContract.Individuals);

                    return completeContract.Individuals.Select(ind => ind.BeneficiaryId).ToList();

                case ContractType.AnimalHealthPlan:
                    if (completeContract.Pets.Count == 0)
                        return null;

                    completeContract.Pets = AddPets(completeContract.Pets);

                    return completeContract.Pets.Select(pet => pet.BeneficiaryId).ToList();

                case ContractType.MobileDeviceInsurance:
                    if (completeContract.MobileDevices.Count == 0)
                        return null;

                    completeContract.MobileDevices = AddMobileDevices(completeContract.MobileDevices);

                    return completeContract.MobileDevices.Select(mob => mob.BeneficiaryId).ToList();

                case ContractType.RealStateInsurance:
                    if (completeContract.Realties.Count == 0)
                        return null;

                    completeContract.Realties = AddRealties(completeContract.Realties);

                    return completeContract.Realties.Select(real => real.BeneficiaryId).ToList();

                case ContractType.VehicleInsurance:
                    if (completeContract.Vehicles.Count == 0)
                        return null;

                    completeContract.Vehicles = AddVehicles(completeContract.Vehicles);

                    return completeContract.Vehicles.Select(veh => veh.BeneficiaryId).ToList();

                default:
                    return null;
            }
        }
        private IndividualDomain AddIndividual(IndividualDomain individual)
        {
            return ConvertersManager.IndividualConverter.Convert(
                _individualsRepository.Add(ConvertersManager.IndividualConverter.Convert(
                    individual)));
        }

        private List<IndividualDomain> AddIndividuals(List<IndividualDomain> individuals)
        {
            var insertedIndividuals = new List<IndividualDomain>();
            
            foreach (var ind in individuals)
            {
                if (ind.BeneficiaryId != Guid.Empty)
                    insertedIndividuals.Add(ind);
                else
                {
                    var addedIndividual = AddIndividual(ind);

                    if (addedIndividual != null)
                        insertedIndividuals.Add(addedIndividual);
                }
            }
            _individualsRepository.Save();
            if (insertedIndividuals.Count == individuals.Count)
                return insertedIndividuals;

            return null;
        }

        private PetDomain AddPet(PetDomain pet)
        {
            return ConvertersManager.PetConverter.Convert(
                _petsRepository.Add(ConvertersManager.PetConverter.Convert(pet)));
        }

        private List<PetDomain> AddPets(List<PetDomain> pets)
        {
            var insertedPets = new List<PetDomain>();
            
            foreach (var pet in pets)
            {
                if (pet.BeneficiaryId != Guid.Empty)
                    insertedPets.Add(pet);
                else
                {
                    var addedPet = AddPet(pet);

                    if (addedPet != null)
                        insertedPets.Add(addedPet);
                }
            }
            _petsRepository.Save();
            if (insertedPets.Count == pets.Count)
                return insertedPets;

            return null;
        }

        private MobileDeviceDomain AddMobileDevice(MobileDeviceDomain mobileDevice)
        {
            return ConvertersManager.MobileDeviceConverter.Convert(
                _mobileDevicesRepository.Add(ConvertersManager.MobileDeviceConverter.Convert(mobileDevice)));
        }

        private List<MobileDeviceDomain> AddMobileDevices(List<MobileDeviceDomain> mobileDevices)
        {
            var insertedMobileDevices = new List<MobileDeviceDomain>();
            
            foreach (var mobile in mobileDevices)
            {
                if (mobile.BeneficiaryId != Guid.Empty)
                    insertedMobileDevices.Add(mobile);
                else
                {
                    var addedMobile = AddMobileDevice(mobile);

                    if (addedMobile != null)
                        insertedMobileDevices.Add(addedMobile);
                }
            }
            _mobileDevicesRepository.Save();
            if (insertedMobileDevices.Count == mobileDevices.Count)
                return insertedMobileDevices;

            return null;
        }

        private RealtyDomain AddRealty(RealtyDomain realty)
        {
            return ConvertersManager.RealtyConverter.Convert(
                _realtiesRepository.Add(ConvertersManager.RealtyConverter.Convert(realty)));
        }

        private List<RealtyDomain> AddRealties(List<RealtyDomain> realties)
        {
            var insertedRealties = new List<RealtyDomain>();
            
            foreach (var realty in realties)
            {
                if (realty.BeneficiaryId != Guid.Empty)
                    insertedRealties.Add(realty);
                else
                {
                    var addedRealty = AddRealty(realty);

                    if (addedRealty != null)
                        insertedRealties.Add(addedRealty);
                }
            }
            _realtiesRepository.Save();
            if (insertedRealties.Count == realties.Count)
                return insertedRealties;

            return null;
        }

        private VehicleDomain AddVehicle(VehicleDomain vehicle)
        {
            return ConvertersManager.VehicleConverter.Convert(
                _vehiclesRepository.Add(ConvertersManager.VehicleConverter.Convert(vehicle)));
        }

        private List<VehicleDomain> AddVehicles(List<VehicleDomain> vehicles)
        {
            var insertedVehicles = new List<VehicleDomain>();
            
            foreach (var vehicle in vehicles)
            {
                if (vehicle.BeneficiaryId != Guid.Empty)
                    insertedVehicles.Add(vehicle);
                else
                {
                    var addedVehicle = AddVehicle(vehicle);

                    if (addedVehicle != null)
                        insertedVehicles.Add(addedVehicle);
                }
            }
            _vehiclesRepository.Save();
            if (insertedVehicles.Count == vehicles.Count)
                return insertedVehicles;

            return null;
        }

        private bool AddContractBeneficiaries(List<Guid> beneficiariesGuidList, SignedContractDomain signedContract)
        {
            if (beneficiariesGuidList == null)
                return false;

            foreach (var ben in beneficiariesGuidList)
            {
                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract)
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
        public CompleteContractDomain Update(Guid id, CompleteContractDomain completeContractToUpdate)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var updatedCompleteContract = new CompleteContractDomain();
                updatedCompleteContract.SignedContract = ConvertersManager.SignedContractConverter.Convert(
                    _signedContractRepository.Update(id, ConvertersManager.SignedContractConverter.Convert(
                        completeContractToUpdate.SignedContract)));
                if (updatedCompleteContract.SignedContract == null)
                    return null;
                _signedContractRepository.Save();

                updatedCompleteContract.Contract = ConvertersManager.ContractConverter.Convert(
                    _contractRepository.Update(updatedCompleteContract.SignedContract.ContractId, ConvertersManager.ContractConverter.Convert(
                        completeContractToUpdate.Contract)));
                if (updatedCompleteContract.Contract == null)
                    return null;
                _contractRepository.Save();

                var updatedBeneficiaries = UpdateBeneficiaries(id, completeContractToUpdate);
                if (updatedBeneficiaries == null)
                    return null;

                if(UpdateContractBeneficiaries(updatedBeneficiaries,
                                             updatedCompleteContract.SignedContract))
                {
                    _db.SaveChanges();
                    scope.Complete();
                    return updatedCompleteContract;
                }
                return null;
            }
        }

        private bool UpdateContractBeneficiaries(List<Guid> updatedBeneficiaries, SignedContractDomain signedContract)
        {
            if (updatedBeneficiaries == null)
                return false;

            _contractBeneficiaryRepository.Get().Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => _contractBeneficiaryRepository.Remove(cb.ContractBeneficiaryId));

            foreach (var ben in updatedBeneficiaries)
            {
                _contractBeneficiaryRepository.Add(new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract)
                });
            }
            _contractBeneficiaryRepository.Save();

            if (updatedBeneficiaries.Count == _db.Contract_Beneficiary
                                                 .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
                                                 .Count())
                return true;
            else
                return false;
            
        }

        private List<Guid> UpdateBeneficiaries(Guid id, CompleteContractDomain completeContract)
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

        private List<Guid> UpdateIndividuals(List<IndividualDomain> individuals)
        {
            List<Guid> updatedIndividuals = new List<Guid>();

            foreach (var ind in individuals)
            {
                // If Individual doesn't have an ID, add it do the DB
                if (ind.BeneficiaryId == Guid.Empty)
                {
                    var addedIndividual = AddIndividual(ind);

                    if (addedIndividual != null)
                        updatedIndividuals.Add(addedIndividual.BeneficiaryId);
                }
                // If Individual already has an ID, update it
                else
                {
                    var updatedIndividual = _individualsRepository.Update(ind.BeneficiaryId, ConvertersManager.IndividualConverter.Convert(
                        ind));

                    if(updatedIndividual != null)
                        updatedIndividuals.Add(updatedIndividual.BeneficiaryId);
                }
            }
            _individualsRepository.Save();
            if (updatedIndividuals.Count == individuals.Count)
                return updatedIndividuals;

            return null;
        }

        private List<Guid> UpdatePets(List<PetDomain> pets)
        {
            List<Guid> updatedPets = new List<Guid>();

            foreach (var pet in pets)
            {
                // If Pet doesn't have an ID, add it do the DB
                if (pet.BeneficiaryId == Guid.Empty)
                {
                    var addedPet = AddPet(pet);

                    if (addedPet != null)
                        updatedPets.Add(addedPet.BeneficiaryId);
                }
                // If Pet already has an ID, update it
                else
                {
                    var updatedPet = _petsRepository.Update(pet.BeneficiaryId, ConvertersManager.PetConverter.Convert(
                        pet));

                    if (updatedPet != null)
                        updatedPets.Add(updatedPet.BeneficiaryId);
                }
            }
            _petsRepository.Save();
            if (updatedPets.Count == pets.Count)
                return updatedPets;

            return null;
        }

        private List<Guid> UpdateMobileDevices(List<MobileDeviceDomain> mobileDevices)
        {
            List<Guid> updatedMobileDevices = new List<Guid>();

            foreach (var mobileDevice in mobileDevices)
            {
                // If MobileDevice doesn't have an ID, add it do the DB
                if (mobileDevice.BeneficiaryId == Guid.Empty)
                {
                    var addedMobileDevice = AddMobileDevice(mobileDevice);

                    if (addedMobileDevice != null)
                        updatedMobileDevices.Add(addedMobileDevice.BeneficiaryId);
                }
                // If MobileDevice already has an ID, update it
                else
                {
                    var updatedMobileDevice = _mobileDevicesRepository.Update(mobileDevice.BeneficiaryId, ConvertersManager.MobileDeviceConverter.Convert(
                        mobileDevice));

                    if (updatedMobileDevice != null)
                        updatedMobileDevices.Add(updatedMobileDevice.BeneficiaryId);
                }
            }
            _mobileDevicesRepository.Save();
            if (updatedMobileDevices.Count == mobileDevices.Count)
                return updatedMobileDevices;

            return null;
        }

        private List<Guid> UpdateRealties(List<RealtyDomain> realties)
        {
            List<Guid> updatedRealties = new List<Guid>();

            foreach (var realty in realties)
            {
                // If Realty doesn't have an ID, add it do the DB
                if (realty.BeneficiaryId == Guid.Empty)
                {
                    var addedRealty = AddRealty(realty);

                    if (addedRealty != null)
                        updatedRealties.Add(addedRealty.BeneficiaryId);
                }
                // If Realty already has an ID, update it
                else
                {
                    var updatedRealty = _realtiesRepository.Update(realty.BeneficiaryId, ConvertersManager.RealtyConverter.Convert(
                        realty));

                    if (updatedRealty != null)
                        updatedRealties.Add(updatedRealty.BeneficiaryId);
                }
            }
            _realtiesRepository.Save();
            if (updatedRealties.Count == realties.Count)
                return updatedRealties;

            return null;
        }

        private List<Guid> UpdateVehicles(List<VehicleDomain> vehicles)
        {
            List<Guid> updatedVehicles = new List<Guid>();

            foreach (var vehicle in vehicles)
            {
                // If Vehicle doesn't have an ID, add it do the DB
                if (vehicle.BeneficiaryId == Guid.Empty)
                {
                    var addedVehicle = AddVehicle(vehicle);

                    if (addedVehicle != null)
                        updatedVehicles.Add(addedVehicle.BeneficiaryId);
                }
                // If Vehicle already has an ID, update it
                else
                {
                    var updatedVehicle = _vehiclesRepository.Update(vehicle.BeneficiaryId, ConvertersManager.VehicleConverter.Convert(
                        vehicle));

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

        public CompleteContractDomain Find(Guid id)
        {
            var signedContract = _db.SignedContracts.Where(sc => sc.SignedContractId == id).FirstOrDefault();
            if (signedContract == null)
                return null;

            var completeContractToReturn = new CompleteContractDomain();
            completeContractToReturn.Contract = ConvertersManager.ContractConverter.Convert(
                _db.Contracts.Where(con => con.ContractId == signedContract.ContractId).FirstOrDefault());

            signedContract.SignedContractIndividual = _individualsRepository.Find(signedContract.BeneficiaryId);
            completeContractToReturn.SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract);


            switch (completeContractToReturn.Contract.ContractType)
            {
                case ContractType.DentalPlan:
                case ContractType.HealthPlan:
                case ContractType.LifeInsurance:
                    completeContractToReturn.Individuals = _individualsRepository.Get().Where(ind => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ind.BeneficiaryId))
                        .Select(ind => ConvertersManager.IndividualConverter.Convert(ind)).ToList();
                    break;
                case ContractType.AnimalHealthPlan:
                    completeContractToReturn.Pets = _petsRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.PetConverter.Convert(ben)).ToList();
                    break;
                case ContractType.MobileDeviceInsurance:
                    completeContractToReturn.MobileDevices = _mobileDevicesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.MobileDeviceConverter.Convert(ben)).ToList();
                    break;
                case ContractType.RealStateInsurance:
                    completeContractToReturn.Realties = _realtiesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                       .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.RealtyConverter.Convert(ben)).ToList();
                    break;
                case ContractType.VehicleInsurance:
                    completeContractToReturn.Vehicles = _vehiclesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.VehicleConverter.Convert(ben)).ToList();
                    break;
                default:
                    return null;
            }
            return completeContractToReturn;
        }

        public IEnumerable<CompleteContractDomain> Get()
        {
            List<CompleteContractDomain> completeContracts = new List<CompleteContractDomain>();

            var contracts = _contractRepository.Get();

            foreach (var contract in contracts)
            {
                var signedContracts = _signedContractRepository.Get().Where(sc => sc.ContractId == contract.ContractId).ToList();

                foreach (var signedContract in signedContracts)
                {
                    var completeContractToAdd = new CompleteContractDomain();
                    completeContractToAdd.Contract = ConvertersManager.ContractConverter.Convert(contract);
                    signedContract.SignedContractIndividual = _individualsRepository.Find(signedContract.BeneficiaryId);
                    completeContractToAdd.SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract);
                    

                    switch (contract.ContractType)
                    {
                        case ContractType.DentalPlan:
                        case ContractType.HealthPlan:
                        case ContractType.LifeInsurance:
                            completeContractToAdd.Individuals = _individualsRepository.Get().Where(ind => _contractBeneficiaryRepository.Get()
                                .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ind.BeneficiaryId))
                                .Select(ind => ConvertersManager.IndividualConverter.Convert(ind)).ToList();
                            break;
                        case ContractType.AnimalHealthPlan:
                            completeContractToAdd.Pets = _petsRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                                .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                                .Select(ben => ConvertersManager.PetConverter.Convert(ben)).ToList();
                            break;
                        case ContractType.MobileDeviceInsurance:
                            completeContractToAdd.MobileDevices = _mobileDevicesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                                .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                                .Select(ben => ConvertersManager.MobileDeviceConverter.Convert(ben)).ToList();
                            break;
                        case ContractType.RealStateInsurance:
                            completeContractToAdd.Realties  = _realtiesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                                .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                                .Select(ben => ConvertersManager.RealtyConverter.Convert(ben)).ToList();
                            break;
                        case ContractType.VehicleInsurance:
                            completeContractToAdd.Vehicles = _vehiclesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                                .Where(cb => cb.SignedContractId == signedContract.SignedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                                .Select(ben => ConvertersManager.VehicleConverter.Convert(ben)).ToList();
                            break;
                        default:
                            continue;
                    }
                    completeContracts.Add(completeContractToAdd);
                }
            }

            return completeContracts;
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

            return true;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
