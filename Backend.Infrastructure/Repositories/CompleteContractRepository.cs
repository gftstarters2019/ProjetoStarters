﻿using Backend.Core.Domains;
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
        private bool disposed = false;

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
                    _contractBeneficiaryRepository.Dispose();
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
                    if (completeContract.Individuals.Count == 0 || completeContract.Individuals.Count > 5)
                        return null;

                    completeContract.Individuals = AddIndividuals(completeContract.Individuals);

                    return completeContract.Individuals.Select(ind => ind.BeneficiaryId).ToList();

                case ContractType.AnimalHealthPlan:
                    if (completeContract.Pets.Count == 0 || completeContract.Pets.Count > 5)
                        return null;

                    completeContract.Pets = AddPets(completeContract.Pets);

                    return completeContract.Pets.Select(pet => pet.BeneficiaryId).ToList();

                case ContractType.MobileDeviceInsurance:
                    if (completeContract.MobileDevices.Count == 0 || completeContract.MobileDevices.Count > 5)
                        return null;

                    completeContract.MobileDevices = AddMobileDevices(completeContract.MobileDevices);

                    return completeContract.MobileDevices.Select(mob => mob.BeneficiaryId).ToList();

                case ContractType.RealStateInsurance:
                    if (completeContract.Realties.Count == 0 || completeContract.Realties.Count > 5)
                        return null;

                    completeContract.Realties = AddRealties(completeContract.Realties);

                    return completeContract.Realties.Select(real => real.BeneficiaryId).ToList();

                case ContractType.VehicleInsurance:
                    if (completeContract.Vehicles.Count == 0 || completeContract.Vehicles.Count > 5)
                        return null;

                    completeContract.Vehicles = AddVehicles(completeContract.Vehicles);

                    return completeContract.Vehicles.Select(veh => veh.BeneficiaryId).ToList();

                default:
                    return null;
            }
        }
        private IndividualDomain AddIndividual(IndividualDomain individual) => ConvertersManager.IndividualConverter.Convert(
            _individualsRepository.Add(ConvertersManager.IndividualConverter.Convert(
                individual)));

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

        private PetDomain AddPet(PetDomain pet) => ConvertersManager.PetConverter.Convert(
                _petsRepository.Add(ConvertersManager.PetConverter.Convert(pet)));

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

        private MobileDeviceDomain AddMobileDevice(MobileDeviceDomain mobileDevice) => ConvertersManager.MobileDeviceConverter.Convert(
                _mobileDevicesRepository.Add(ConvertersManager.MobileDeviceConverter.Convert(mobileDevice)));

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

        private RealtyDomain AddRealty(RealtyDomain realty) => ConvertersManager.RealtyConverter.Convert(
                _realtiesRepository.Add(ConvertersManager.RealtyConverter.Convert(realty)));

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

        private VehicleDomain AddVehicle(VehicleDomain vehicle) => ConvertersManager.VehicleConverter.Convert(
                _vehiclesRepository.Add(ConvertersManager.VehicleConverter.Convert(vehicle)));

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
                var addedContractBeneficiary = AddContractBeneficiary(new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract)
                });
                if (addedContractBeneficiary == null)
                    return false;
            }

            if(beneficiariesGuidList.Count == _contractBeneficiaryRepository.Get()
                                                 .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
                                                 .Count())
                return true;
            else
                return false;
        }

        private ContractBeneficiary AddContractBeneficiary(ContractBeneficiary contractBeneficiaryToAdd)
        {
            var addedContractBeneficiary = _contractBeneficiaryRepository.Add(contractBeneficiaryToAdd);
            if (addedContractBeneficiary == null)
                return null;

            _contractBeneficiaryRepository.Save();
            return addedContractBeneficiary;
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
            
            if (!DeleteContractBeneficiaries(signedContract.SignedContractId))
                return false;
            
            foreach (var ben in updatedBeneficiaries)
            {
                var addedContractBeneficiary = AddContractBeneficiary(new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    SignedContract = ConvertersManager.SignedContractConverter.Convert(signedContract)
                });
                if (addedContractBeneficiary == null)
                    return false;
            }
            
            _contractBeneficiaryRepository.Save();

            if (updatedBeneficiaries.Count == _contractBeneficiaryRepository.Get()
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
                    return UpdateIndividuals(completeContract.Individuals);

                case ContractType.AnimalHealthPlan:
                    return UpdatePets(completeContract.Pets);

                case ContractType.MobileDeviceInsurance:
                    return UpdateMobileDevices(completeContract.MobileDevices);

                case ContractType.RealStateInsurance:
                    return UpdateRealties(completeContract.Realties);

                case ContractType.VehicleInsurance:
                    return UpdateVehicles(completeContract.Vehicles);

                default:
                    return null;
            }
        }

        private List<Guid> UpdateIndividuals(List<IndividualDomain> individuals)
        {
            if (individuals.Count == 0)
                return null;

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
            if (pets.Count == 0)
                return null;

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
            if (mobileDevices.Count == 0)
                return null;

            List<Guid> updatedMobileDevices = new List<Guid>();

            foreach (var mobileDevice in mobileDevices)
            {
                // If MobileDevice doesn't have an ID, add it do the DB
                if (mobileDevice.BeneficiaryId == Guid.Empty)
                {
                    var addedMobileDevice = AddMobileDevice(mobileDevice);

                    if (addedMobileDevice == null)
                        return null;
                    updatedMobileDevices.Add(addedMobileDevice.BeneficiaryId);
                }
                // If MobileDevice already has an ID, update it
                else
                {
                    var updatedMobileDevice = _mobileDevicesRepository.Update(mobileDevice.BeneficiaryId, ConvertersManager.MobileDeviceConverter.Convert(
                        mobileDevice));

                    if (updatedMobileDevice == null)
                        return null;
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
            if (realties.Count == 0)
                return null;

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
            if (vehicles.Count == 0)
                return null;

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

        #region Delete
        private bool DeleteContractBeneficiaries(Guid id)
        {
            var contractBeneficiariesToDelete = _contractBeneficiaryRepository.Get().Where(cb => cb.SignedContractId == id).ToList();
            foreach(var contractBeneficiary in contractBeneficiariesToDelete)
            {
                if(!DeleteContractBeneficiary(contractBeneficiary.ContractBeneficiaryId))
                    return false;
            }
            return true;
        }

        private bool DeleteContractBeneficiary(Guid id)
        {
            if(!_contractBeneficiaryRepository.Remove(id))
                return false;
            _contractBeneficiaryRepository.Save();
            return true;
        }
        #endregion Delete

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
                    completeContractToReturn.Individuals = IndividualsInContract(signedContract.SignedContractId);
                    break;
                case ContractType.AnimalHealthPlan:
                    completeContractToReturn.Pets = PetsInContract(signedContract.SignedContractId);
                    break;
                case ContractType.MobileDeviceInsurance:
                    completeContractToReturn.MobileDevices = MobileDevicesInContract(signedContract.SignedContractId);
                    break;
                case ContractType.RealStateInsurance:
                    completeContractToReturn.Realties = RealtiesInContract(signedContract.SignedContractId);
                    break;
                case ContractType.VehicleInsurance:
                    completeContractToReturn.Vehicles = VehiclesInContract(signedContract.SignedContractId);
                    break;
                default:
                    return null;
            }
            return completeContractToReturn;
        }

        #region In Contract
        private List<IndividualDomain> IndividualsInContract(Guid signedContractId)
        {
            return _individualsRepository.Get().Where(ind => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContractId).Select(cb => cb.BeneficiaryId).Contains(ind.BeneficiaryId))
                        .Select(ind => ConvertersManager.IndividualConverter.Convert(ind)).ToList();
        }

        private List<PetDomain> PetsInContract(Guid signedContractId)
        {
            return _petsRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.PetConverter.Convert(ben)).ToList();
        }

        private List<MobileDeviceDomain> MobileDevicesInContract(Guid signedContractId)
        {
            return _mobileDevicesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.MobileDeviceConverter.Convert(ben)).ToList();
        }

        private List<RealtyDomain> RealtiesInContract(Guid signedContractId)
        {
            return _realtiesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                       .Where(cb => cb.SignedContractId == signedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.RealtyConverter.Convert(ben)).ToList();
        }

        private List<VehicleDomain> VehiclesInContract(Guid signedContractId)
        {
            return _vehiclesRepository.Get().Where(ben => _contractBeneficiaryRepository.Get()
                        .Where(cb => cb.SignedContractId == signedContractId).Select(cb => cb.BeneficiaryId).Contains(ben.BeneficiaryId))
                        .Select(ben => ConvertersManager.VehicleConverter.Convert(ben)).ToList();
        }
        #endregion In Contract

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
                            completeContractToAdd.Individuals = IndividualsInContract(signedContract.SignedContractId);
                            break;
                        case ContractType.AnimalHealthPlan:
                            completeContractToAdd.Pets = PetsInContract(signedContract.SignedContractId);
                            break;
                        case ContractType.MobileDeviceInsurance:
                            completeContractToAdd.MobileDevices = MobileDevicesInContract(signedContract.SignedContractId);
                            break;
                        case ContractType.RealStateInsurance:
                            completeContractToAdd.Realties = RealtiesInContract(signedContract.SignedContractId);
                            break;
                        case ContractType.VehicleInsurance:
                            completeContractToAdd.Vehicles = VehiclesInContract(signedContract.SignedContractId);
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
    }
}
