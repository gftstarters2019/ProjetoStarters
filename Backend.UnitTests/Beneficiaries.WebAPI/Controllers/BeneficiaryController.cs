﻿using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Beneficiaries.WebAPI.Controllers
{
    /// <summary>
    /// Beneficiaries API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IReadOnlyRepository<Beneficiary> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<Beneficiary> _beneficiaryWriteRepository;

        /// <summary>
        /// BeneficiaryController constructor
        /// </summary>
        /// <param name="beneficiaryReadOnlyRepository"></param>
        /// <param name="beneficiaryWriteRepository"></param>
        public BeneficiaryController(IReadOnlyRepository<Beneficiary> beneficiaryReadOnlyRepository, IWriteRepository<Beneficiary> beneficiaryWriteRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
        }

        /// <summary>
        /// Gets all beneficiaries registered.
        /// </summary>
        /// <returns>Beneficiaries registered</returns>
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get().Where(b => !b.BeneficiaryDeleted));
        }

        /// <summary>
        /// Gets all deleted beneficiaries.
        /// </summary>
        /// <returns>Beneficiaries deleted</returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedBeneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get().Where(b => b.BeneficiaryDeleted));
        }

        /// <summary>
        /// Get a specific beneficiary.
        /// </summary>
        /// <param name="id">GUID value representing a BeneficiaryId</param>
        /// <returns>Chosen beneficiary</returns>
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        #region Individual
        /// <summary>
        /// Creates a new Individual in the database
        /// </summary>
        /// <param name="individual">Individual without IDs</param>
        /// <returns>Created individual, with IDs</returns>
        [HttpPost("Individual")]
        public IActionResult PostIndividual([FromBody] Individual individual)
        {
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(individual);

            return Ok(individual);
        }

        /// <summary>
        /// Updates an Individual in the database
        /// </summary>
        /// <param name="id">ID of the Individual to be updated</param>
        /// <param name="individual">New values for the individual</param>
        /// <returns>Updated Individual</returns>
        [HttpPut("Individual/{id}")]
        public IActionResult UpdateIndividual(Guid id, [FromBody] Individual individual)
        {
            var obj = (Individual) _beneficiaryReadOnlyRepository.Find(id);

            obj.BeneficiaryDeleted = individual.BeneficiaryDeleted;
            obj.IndividualBirthdate = individual.IndividualBirthdate;
            obj.IndividualCPF = individual.IndividualCPF;
            obj.IndividualEmail = individual.IndividualEmail;
            obj.IndividualName = individual.IndividualName;
            obj.IndividualRG = individual.IndividualRG;
            
            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Individual

        #region MobileDevice
        /// <summary>
        /// Creates a new Mobile Device in the database
        /// </summary>
        /// <param name="mobileDevice">Mobile Device without IDs</param>
        /// <returns>Created mobile device, with IDs</returns>
        [HttpPost("MobileDevice")]
        public IActionResult PostMobileDevice([FromBody] MobileDevice mobileDevice)
        {
            mobileDevice.BeneficiaryId = Guid.NewGuid();
            mobileDevice.MobileDeviceId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(mobileDevice);

            return Ok(mobileDevice);
        }

        /// <summary>
        /// Updates an Mobile Device in the database
        /// </summary>
        /// <param name="id">ID of the Mobile Device to be updated</param>
        /// <param name="mobileDevice">New values for the mobile device</param>
        /// <returns>Updated Mobile Device</returns>
        [HttpPut("MobileDevice/{id}")]
        public IActionResult UpdateMobileDevice(Guid id, [FromBody] MobileDevice mobileDevice)
        {
            var obj = (MobileDevice) _beneficiaryReadOnlyRepository.Find(id);

            obj.BeneficiaryDeleted = mobileDevice.BeneficiaryDeleted;
            obj.MobileDeviceBrand = mobileDevice.MobileDeviceBrand;
            obj.MobileDeviceInvoiceValue = mobileDevice.MobileDeviceInvoiceValue;
            obj.MobileDeviceManufactoringYear = mobileDevice.MobileDeviceManufactoringYear;
            obj.MobileDeviceModel = mobileDevice.MobileDeviceModel;
            obj.MobileDeviceSerialNumber = mobileDevice.MobileDeviceSerialNumber;
            obj.MobileDeviceType = mobileDevice.MobileDeviceType;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion MobileDevice

        #region Pet
        /// <summary>
        /// Creates a new Pet in the database
        /// </summary>
        /// <param name="pet">Pet without IDs</param>
        /// <returns>Created pet, with IDs</returns>
        [HttpPost("Pet")]
        public IActionResult PostPet([FromBody] Pet pet)
        {
            pet.BeneficiaryId = Guid.NewGuid();
            pet.PetId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(pet);

            return Ok(pet);
        }

        /// <summary>
        /// Updates an Pet in the database
        /// </summary>
        /// <param name="id">ID of the Pet to be updated</param>
        /// <param name="pet">New values for the pet</param>
        /// <returns>Updated Pet</returns>
        [HttpPut("Pet/{id}")]
        public IActionResult UpdatePet(Guid id, [FromBody] Pet pet)
        {
            var obj = (Pet) _beneficiaryReadOnlyRepository.Find(id);

            obj.BeneficiaryDeleted = pet.BeneficiaryDeleted;
            obj.PetBirthdate = pet.PetBirthdate;
            obj.PetBreed = pet.PetBreed;
            obj.PetName = pet.PetName;
            obj.PetSpecies = pet.PetSpecies;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Pet

        #region Realty
        /// <summary>
        /// Creates a new Realty in the database
        /// </summary>
        /// <param name="realty">Realty without IDs</param>
        /// <returns>Created realty, with IDs</returns>
        [HttpPost("Realty")]
        public IActionResult PostRealty([FromBody] Realty realty)
        {
            realty.BeneficiaryId = Guid.NewGuid();
            realty.RealtyId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(realty);

            return Ok(realty);
        }

        /// <summary>
        /// Updates an Realty in the database
        /// </summary>
        /// <param name="id">ID of the Realty to be updated</param>
        /// <param name="realty">New values for the realty</param>
        /// <returns>Updated Realty</returns>
        [HttpPut("Realty/{id}")]
        public IActionResult UpdateRealty(Guid id, [FromBody] Realty realty)
        {
            var obj = (Realty) _beneficiaryReadOnlyRepository.Find(id);

            obj.BeneficiaryDeleted = realty.BeneficiaryDeleted;
            obj.RealtyAddress = realty.RealtyAddress;
            obj.RealtyConstructionDate = realty.RealtyConstructionDate;
            obj.RealtyMarketValue = realty.RealtyMarketValue;
            obj.RealtyMunicipalRegistration = realty.RealtyMunicipalRegistration;
            obj.RealtySaleValue = realty.RealtySaleValue;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Realty

        #region Vehicle
        /// <summary>
        /// Creates a new Vehicle in the database
        /// </summary>
        /// <param name="vehicle">Vehicle without IDs</param>
        /// <returns>Created vehicle, with IDs</returns>
        [HttpPost("Vehicle")]
        public IActionResult PostVehicle([FromBody] Vehicle vehicle)
        {
            vehicle.BeneficiaryId = Guid.NewGuid();
            vehicle.VehicleId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(vehicle);

            return Ok(vehicle);
        }

        /// <summary>
        /// Updates an Vehicle in the database
        /// </summary>
        /// <param name="id">ID of the Vehicle to be updated</param>
        /// <param name="vehicle">New values for the vehicle</param>
        /// <returns>Updated Vehicle</returns>
        [HttpPut("Vehicle/{id}")]
        public IActionResult UpdateVehicle(Guid id, [FromBody] Vehicle vehicle)
        {
            var obj = (Vehicle) _beneficiaryReadOnlyRepository.Find(id);

            obj.VehicleBrand = vehicle.VehicleBrand;
            obj.VehicleChassisNumber = vehicle.VehicleChassisNumber;
            obj.VehicleColor = vehicle.VehicleColor;
            obj.VehicleCurrentFipeValue = vehicle.VehicleCurrentFipeValue;
            obj.VehicleCurrentMileage = vehicle.VehicleCurrentMileage;
            obj.VehicleDoneInspection = vehicle.VehicleDoneInspection;
            obj.VehicleManufactoringYear = vehicle.VehicleManufactoringYear;
            obj.VehicleModel = vehicle.VehicleModel;
            obj.VehicleModelYear = vehicle.VehicleModelYear;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Vehicle

        /// <summary>
        /// Deletes a beneficiary
        /// </summary>
        /// <param name="id">BeneficiaryId to be deleted</param>
        /// <returns>Deleted beneficiary</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBeneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);

            if (obj != null)
            {
                obj.BeneficiaryDeleted = !obj.BeneficiaryDeleted;
                return Ok(_beneficiaryWriteRepository.Update(obj));
            }

            return NotFound(obj);
        }
    }
}