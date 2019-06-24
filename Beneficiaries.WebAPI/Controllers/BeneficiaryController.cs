using Backend.Application.ModelValidations;
using Backend.Application.ViewModels;
using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mail;

namespace Beneficiaries.WebAPI.Controllers
{
    /// <summary>
    /// Beneficiaries API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IRepository<Beneficiary> _beneficiaryRepository;
        private readonly IRepository<ContractBeneficiary> _contractsRepository;

        private readonly IRepository<IndividualEntity> _individualRepository;

        private readonly IRepository<PetEntity> _petRepository;

        private readonly IRepository<MobileDeviceEntity> _mobileDeviceRepository;

        private readonly IRepository<RealtyViewModel> _realtyRepository;

        private readonly IRepository<VehicleEntity> _vehicleRepository;

        /// <summary>
        /// BeneficiaryController constructor
        /// </summary>
        public BeneficiaryController(IRepository<Beneficiary> beneficiaryRepository,
                                     IRepository<ContractBeneficiary> contractsRepository,
                                     IRepository<IndividualEntity> individualRepository,
                                     IRepository<PetEntity> petRepository,
                                     IRepository<MobileDeviceEntity> mobileDeviceRepository,
                                     IRepository<RealtyViewModel> realtyRepository,
                                     IRepository<VehicleEntity> vehicleRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _contractsRepository = contractsRepository;

            _individualRepository = individualRepository;

            _petRepository = petRepository;

            _mobileDeviceRepository = mobileDeviceRepository;

            _realtyRepository = realtyRepository;

            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Gets all beneficiaries registered.
        /// </summary>
        /// <returns>Beneficiaries registered</returns>
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok(_beneficiaryRepository.Get().Where(b => !b.IsDeleted));
        }

        /// <summary>
        /// Gets all deleted beneficiaries.
        /// </summary>
        /// <returns>Beneficiaries deleted</returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedBeneficiaries()
        {
            return Ok(_beneficiaryRepository.Get().Where(b => b.IsDeleted));
        }

        /// <summary>
        /// Get a specific beneficiary.
        /// </summary>
        /// <param name="id">GUID value representing a BeneficiaryId</param>
        /// <returns>Chosen beneficiary</returns>
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            var obj = _beneficiaryRepository.Find(id);
            return Ok(obj);
        }

        #region Individual
        /// <summary>
        /// Gets all Individuals beneficiaries.
        /// </summary>
        /// <returns>Individuals</returns>
        [HttpGet("Individuals")]
        public IActionResult GetIndividuals()
        {
            return Ok(_individualRepository.Get());
        }

        /// <summary>
        /// Creates a new Individual in the database
        /// </summary>
        /// <param name="individual">Individual without IDs</param>
        /// <returns>Created individual, with IDs</returns>
        [HttpPost("Individual")]
        public IActionResult PostIndividual([FromBody] IndividualEntity individual)
        {
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();

            if (!IndividualValidations.IndividualIsValid(individual))
                return StatusCode(403);

            _individualRepository.Add(individual);

            return Ok(individual);
        }

        /// <summary>
        /// Updates an Individual in the database
        /// </summary>
        /// <param name="id">ID of the Individual to be updated</param>
        /// <param name="individual">New values for the individual</param>
        /// <returns>Updated Individual</returns>
        [HttpPut("Individual/{id}")]
        public IActionResult UpdateIndividual(Guid id, [FromBody] IndividualEntity individual)
        {
            if (!IndividualValidations.IndividualIsValid(individual))
                return Forbid();

            var obj = (IndividualEntity)_beneficiaryRepository.Find(id);

            obj.IsDeleted = individual.IsDeleted;
            obj.IndividualBirthdate = individual.IndividualBirthdate;
            obj.IndividualCPF = individual.IndividualCPF;
            obj.IndividualEmail = individual.IndividualEmail;
            obj.IndividualName = individual.IndividualName;
            obj.IndividualRG = individual.IndividualRG;

            return Ok(_beneficiaryRepository.Update(id, obj));
        }
        #endregion Individual

        #region MobileDevice
        /// <summary>
        /// Gets all MobileDevices beneficiaries.
        /// </summary>
        /// <returns>MobileDevices</returns>
        [HttpGet("MobileDevices")]
        public IActionResult GetMobileDevices()
        {
            return Ok(_mobileDeviceRepository.Get());
        }

        /// <summary>
        /// Creates a new Mobile Device in the database
        /// </summary>
        /// <param name="mobileDevice">Mobile Device without IDs</param>
        /// <returns>Created mobile device, with IDs</returns>
        [HttpPost("MobileDevice")]
        public IActionResult PostMobileDevice([FromBody] MobileDeviceEntity mobileDevice)
        {
            mobileDevice.BeneficiaryId = Guid.NewGuid();

            if (!MobileDeviceValidations.MobileDeviceIsValid(mobileDevice))
                return Forbid();

            _mobileDeviceRepository.Add(mobileDevice);

            return Ok(mobileDevice);
        }

        /// <summary>
        /// Updates an Mobile Device in the database
        /// </summary>
        /// <param name="id">ID of the Mobile Device to be updated</param>
        /// <param name="mobileDevice">New values for the mobile device</param>
        /// <returns>Updated Mobile Device</returns>
        [HttpPut("MobileDevice/{id}")]
        public IActionResult UpdateMobileDevice(Guid id, [FromBody] MobileDeviceEntity mobileDevice)
        {
            if (!MobileDeviceValidations.MobileDeviceIsValid(mobileDevice))
                return Forbid();

            var obj = (MobileDeviceEntity)_beneficiaryRepository.Find(id);

            obj.IsDeleted = mobileDevice.IsDeleted;
            obj.MobileDeviceBrand = mobileDevice.MobileDeviceBrand;
            obj.MobileDeviceInvoiceValue = mobileDevice.MobileDeviceInvoiceValue;
            obj.MobileDeviceManufactoringYear = mobileDevice.MobileDeviceManufactoringYear;
            obj.MobileDeviceModel = mobileDevice.MobileDeviceModel;
            obj.MobileDeviceSerialNumber = mobileDevice.MobileDeviceSerialNumber;
            obj.MobileDeviceType = mobileDevice.MobileDeviceType;

            return Ok(_beneficiaryRepository.Update(id, obj));
        }
        #endregion MobileDevice

        #region Pet
        /// <summary>
        /// Gets all Pets beneficiaries.
        /// </summary>
        /// <returns>Pets</returns>
        [HttpGet("Pets")]
        public IActionResult GetPets()
        {
            return Ok(_petRepository.Get());
        }

        /// <summary>
        /// Creates a new Pet in the database
        /// </summary>
        /// <param name="pet">Pet without IDs</param>
        /// <returns>Created pet, with IDs</returns>
        [HttpPost("Pet")]
        public IActionResult PostPet([FromBody] PetEntity pet)
        {
            pet.BeneficiaryId = Guid.NewGuid();
            //pet.PetId = Guid.NewGuid();

            //if (!PetIsValid(pet))
            //    return Forbid();
            _petRepository.Add(pet);

            return Ok(pet);
        }

        /// <summary>
        /// Updates an Pet in the database
        /// </summary>
        /// <param name="id">ID of the Pet to be updated</param>
        /// <param name="pet">New values for the pet</param>
        /// <returns>Updated Pet</returns>
        [HttpPut("Pet/{id}")]
        public IActionResult UpdatePet(Guid id, [FromBody] PetEntity pet)
        {
            //if (!PetIsValid(pet))
            //    return Forbid();

            var obj = (PetEntity)_beneficiaryRepository.Find(id);

            obj.IsDeleted = pet.IsDeleted;
            obj.PetBirthdate = pet.PetBirthdate;
            obj.PetBreed = pet.PetBreed;
            obj.PetName = pet.PetName;
            obj.PetSpecies = pet.PetSpecies;

            return Ok(_beneficiaryRepository.Update(id, obj));
        }
        #endregion Pet

        #region Realty
        /// <summary>
        /// Gets all Realties beneficiaries.
        /// </summary>
        /// <returns>Realties</returns>
        [HttpGet("Realties")]
        public IActionResult GetRealties()
        {
            return Ok(_realtyRepository.Get());
        }

        /// <summary>
        /// Creates a new Realty in the database
        /// </summary>
        /// <param name="realty">Realty without IDs</param>
        /// <returns>Created realty, with IDs</returns>
        [HttpPost("Realty")]
        public IActionResult PostRealty([FromBody] RealtyEntity realty)
        {
            realty.BeneficiaryId = Guid.NewGuid();

            if (!RealtyValidations.RealtyIsValid(realty))
                return Forbid();

            //_realtyRepository.Add(realty);

            return Ok(realty);
        }

        /// <summary>
        /// Updates an Realty in the database
        /// </summary>
        /// <param name="id">ID of the Realty to be updated</param>
        /// <param name="realty">New values for the realty</param>
        /// <returns>Updated Realty</returns>
        [HttpPut("Realty/{id}")]
        public IActionResult UpdateRealty(Guid id, [FromBody] RealtyEntity realty)
        {
            if (!RealtyValidations.RealtyIsValid(realty))
                return Forbid();

            var obj = (RealtyEntity)_beneficiaryRepository.Find(id);

            obj.IsDeleted = realty.IsDeleted;
            obj.RealtyConstructionDate = realty.RealtyConstructionDate;
            obj.RealtyMarketValue = realty.RealtyMarketValue;
            obj.RealtyMunicipalRegistration = realty.RealtyMunicipalRegistration;
            obj.RealtySaleValue = realty.RealtySaleValue;

            return Ok(_beneficiaryRepository.Update(id, obj));
        }
        #endregion Realty

        #region Vehicle
        /// <summary>
        /// Gets all Vehicles beneficiaries.
        /// </summary>
        /// <returns>Vehicles</returns>
        [HttpGet("Vehicles")]
        public IActionResult GetVehicles()
        {
            return Ok(_vehicleRepository.Get());
        }

        /// <summary>
        /// Creates a new Vehicle in the database
        /// </summary>
        /// <param name="vehicle">Vehicle without IDs</param>
        /// <returns>Created vehicle, with IDs</returns>
        [HttpPost("Vehicle")]
        public IActionResult PostVehicle([FromBody] VehicleEntity vehicle)
        {
            vehicle.BeneficiaryId = Guid.NewGuid();

            if (!VehicleValidations.VehicleIsValid(vehicle))
                return Forbid();

            _vehicleRepository.Add(vehicle);

            return Ok(vehicle);
        }

        /// <summary>
        /// Updates an Vehicle in the database
        /// </summary>
        /// <param name="id">ID of the Vehicle to be updated</param>
        /// <param name="vehicle">New values for the vehicle</param>
        /// <returns>Updated Vehicle</returns>
        [HttpPut("Vehicle/{id}")]
        public IActionResult UpdateVehicle(Guid id, [FromBody] VehicleEntity vehicle)
        {
            if (!VehicleValidations.VehicleIsValid(vehicle))
                return Forbid();

            var obj = (VehicleEntity)_beneficiaryRepository.Find(id);

            obj.VehicleBrand = vehicle.VehicleBrand;
            obj.VehicleChassisNumber = vehicle.VehicleChassisNumber;
            obj.VehicleColor = vehicle.VehicleColor;
            obj.VehicleCurrentFipeValue = vehicle.VehicleCurrentFipeValue;
            obj.VehicleCurrentMileage = vehicle.VehicleCurrentMileage;
            obj.VehicleDoneInspection = vehicle.VehicleDoneInspection;
            obj.VehicleManufactoringYear = vehicle.VehicleManufactoringYear;
            obj.VehicleModel = vehicle.VehicleModel;
            obj.VehicleModelYear = vehicle.VehicleModelYear;

            return Ok(_beneficiaryRepository.Update(id, obj));
        }
        #endregion Vehicle

        /// <summary>
        /// Soft Deletes a beneficiary
        /// </summary>
        /// <param name="id">BeneficiaryId to be deleted</param>
        /// <returns>Deleted beneficiary</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBeneficiary(Guid id)
        {
            var beneficiariesContracts = _contractsRepository.Get()
                .Where(cb => cb.BeneficiaryId == id)
                .ToList();

            if (beneficiariesContracts.Count > 0)
                return StatusCode(403);

            var obj = _beneficiaryRepository.Find(id);

            if (obj != null)
            {
                obj.IsDeleted = !obj.IsDeleted;
                return Ok(_beneficiaryRepository.Update(id, obj));
            }

            return NotFound(obj);
        }
    }
}
