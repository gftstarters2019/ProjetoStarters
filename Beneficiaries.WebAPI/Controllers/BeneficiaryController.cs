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
    /// Beneficiaries API2
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IReadOnlyRepository<Beneficiary> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<Beneficiary> _beneficiaryWriteRepository;
        private readonly IReadOnlyRepository<ContractBeneficiary> _contractsReadOnlyRepository;

        private readonly IReadOnlyRepository<Individual> _individualReadOnlyRepository;
        private readonly IWriteRepository<Individual> _individualWriteRepository;

        private readonly IReadOnlyRepository<Pet> _petReadOnlyRepository;
        private readonly IWriteRepository<Pet> _petWriteRepository;

        private readonly IReadOnlyRepository<MobileDevice> _mobileDeviceReadOnlyRepository;
        private readonly IWriteRepository<MobileDevice> _mobileWriteRepository;

        private readonly IReadOnlyRepository<RealtyViewModel> _realtyReadOnlyRepository;
        private readonly IWriteRepository<Realty> _realtyWriteRepository;

        private readonly IReadOnlyRepository<Vehicle> _vehicleReadOnlyRepository;
        private readonly IWriteRepository<Vehicle> _vehicleWriteRepository;

        /// <summary>
        /// BeneficiaryController constructor
        /// </summary>
        public BeneficiaryController(IReadOnlyRepository<Beneficiary> beneficiaryReadOnlyRepository,
            IWriteRepository<Beneficiary> beneficiaryWriteRepository,
            IReadOnlyRepository<ContractBeneficiary> contractsReadOnlyRepository,
            IReadOnlyRepository<Individual> individualReadOnlyRepository,
            IWriteRepository<Individual> individualWriteRepository,
            IReadOnlyRepository<Pet> petReadOnlyRepository,
            IWriteRepository<Pet> petWriteRepository,
            IReadOnlyRepository<MobileDevice> mobileDeviceReadOnlyRepository,
            IWriteRepository<MobileDevice> mobileDeviceWriteRepository,
            IReadOnlyRepository<RealtyViewModel> realtyReadOnlyRepository,
            IWriteRepository<Realty> realtyWriteRepository,
            IReadOnlyRepository<Vehicle> vehicleReadOnlyRepository,
            IWriteRepository<Vehicle> vehicleWriteRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
            _contractsReadOnlyRepository = contractsReadOnlyRepository;

            _individualReadOnlyRepository = individualReadOnlyRepository;
            _individualWriteRepository = individualWriteRepository;

            _petReadOnlyRepository = petReadOnlyRepository;
            _petWriteRepository = petWriteRepository;

            _mobileDeviceReadOnlyRepository = mobileDeviceReadOnlyRepository;
            _mobileWriteRepository = mobileDeviceWriteRepository;

            _realtyReadOnlyRepository = realtyReadOnlyRepository;
            _realtyWriteRepository = realtyWriteRepository;

            _vehicleReadOnlyRepository = vehicleReadOnlyRepository;
            _vehicleWriteRepository = vehicleWriteRepository;
        }

        /// <summary>
        /// Gets all beneficiaries registered.
        /// </summary>
        /// <returns>Beneficiaries registered</returns>
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get().Where(b => !b.IsDeleted));
        }

        /// <summary>
        /// Gets all deleted beneficiaries.
        /// </summary>
        /// <returns>Beneficiaries deleted</returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedBeneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get().Where(b => b.IsDeleted));
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
        /// Gets all Individuals beneficiaries.
        /// </summary>
        /// <returns>Individuals</returns>
        [HttpGet("Individuals")]
        public IActionResult GetIndividuals()
        {
            return Ok(_individualReadOnlyRepository.Get());
        }

        /// <summary>
        /// Creates a new Individual in the database
        /// </summary>
        /// <param name="individual">Individual without IDs</param>
        /// <returns>Created individual, with IDs</returns>
        [HttpPost("Individual")]
        public IActionResult PostIndividual([FromBody] Individual individual)
        {
            individual.BeneficiaryId = Guid.NewGuid();
            //individual.IndividualId = Guid.NewGuid();

            if (!IndividualValidations.IndividualIsValid(individual))
                return StatusCode(403);

            _individualWriteRepository.Add(individual);

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
            if (!IndividualValidations.IndividualIsValid(individual))
                return Forbid();

            var obj = (Individual)_beneficiaryReadOnlyRepository.Find(id);

            obj.IsDeleted = individual.IsDeleted;
            obj.IndividualBirthdate = individual.IndividualBirthdate;
            obj.IndividualCPF = individual.IndividualCPF;
            obj.IndividualEmail = individual.IndividualEmail;
            obj.IndividualName = individual.IndividualName;
            obj.IndividualRG = individual.IndividualRG;

            return Ok(_beneficiaryWriteRepository.Update(id, obj));
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
            return Ok(_mobileDeviceReadOnlyRepository.Get());
        }

        /// <summary>
        /// Creates a new Mobile Device in the database
        /// </summary>
        /// <param name="mobileDevice">Mobile Device without IDs</param>
        /// <returns>Created mobile device, with IDs</returns>
        [HttpPost("MobileDevice")]
        public IActionResult PostMobileDevice([FromBody] MobileDevice mobileDevice)
        {
            mobileDevice.BeneficiaryId = Guid.NewGuid();

            if (!MobileDeviceValidations.MobileDeviceIsValid(mobileDevice))
                return Forbid();

            _mobileWriteRepository.Add(mobileDevice);

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
            if (!MobileDeviceValidations.MobileDeviceIsValid(mobileDevice))
                return Forbid();

            var obj = (MobileDevice)_beneficiaryReadOnlyRepository.Find(id);

            obj.IsDeleted = mobileDevice.IsDeleted;
            obj.MobileDeviceBrand = mobileDevice.MobileDeviceBrand;
            obj.MobileDeviceInvoiceValue = mobileDevice.MobileDeviceInvoiceValue;
            obj.MobileDeviceManufactoringYear = mobileDevice.MobileDeviceManufactoringYear;
            obj.MobileDeviceModel = mobileDevice.MobileDeviceModel;
            obj.MobileDeviceSerialNumber = mobileDevice.MobileDeviceSerialNumber;
            obj.MobileDeviceType = mobileDevice.MobileDeviceType;

            return Ok(_beneficiaryWriteRepository.Update(id, obj));
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
            return Ok(_petReadOnlyRepository.Get());
        }

        /// <summary>
        /// Creates a new Pet in the database
        /// </summary>
        /// <param name="pet">Pet without IDs</param>
        /// <returns>Created pet, with IDs</returns>
        [HttpPost("Pet")]
        public IActionResult PostPet([FromBody] Pet pet)
        {
            pet.BeneficiaryId = Guid.NewGuid();
            //pet.PetId = Guid.NewGuid();

            //if (!PetIsValid(pet))
            //    return Forbid();
            _petWriteRepository.Add(pet);

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
            //if (!PetIsValid(pet))
            //    return Forbid();

            var obj = (Pet)_beneficiaryReadOnlyRepository.Find(id);

            obj.IsDeleted = pet.IsDeleted;
            obj.PetBirthdate = pet.PetBirthdate;
            obj.PetBreed = pet.PetBreed;
            obj.PetName = pet.PetName;
            obj.PetSpecies = pet.PetSpecies;

            return Ok(_beneficiaryWriteRepository.Update(id, obj));
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
            return Ok(_realtyReadOnlyRepository.Get());
        }

        /// <summary>
        /// Creates a new Realty in the database
        /// </summary>
        /// <param name="realty">Realty without IDs</param>
        /// <returns>Created realty, with IDs</returns>
        [HttpPost("Realty")]
        public IActionResult PostRealty([FromBody] Realty realty)
        {
            realty.BeneficiaryId = Guid.NewGuid();

            if (!RealtyValidations.RealtyIsValid(realty))
                return Forbid();

            _realtyWriteRepository.Add(realty);

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
            if (!RealtyValidations.RealtyIsValid(realty))
                return Forbid();

            var obj = (Realty)_beneficiaryReadOnlyRepository.Find(id);

            obj.IsDeleted = realty.IsDeleted;
            obj.RealtyConstructionDate = realty.RealtyConstructionDate;
            obj.RealtyMarketValue = realty.RealtyMarketValue;
            obj.RealtyMunicipalRegistration = realty.RealtyMunicipalRegistration;
            obj.RealtySaleValue = realty.RealtySaleValue;

            return Ok(_beneficiaryWriteRepository.Update(id, obj));
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
            return Ok(_vehicleReadOnlyRepository.Get());
        }

        /// <summary>
        /// Creates a new Vehicle in the database
        /// </summary>
        /// <param name="vehicle">Vehicle without IDs</param>
        /// <returns>Created vehicle, with IDs</returns>
        [HttpPost("Vehicle")]
        public IActionResult PostVehicle([FromBody] Vehicle vehicle)
        {
            vehicle.BeneficiaryId = Guid.NewGuid();

            if (!VehicleValidations.VehicleIsValid(vehicle))
                return Forbid();

            _vehicleWriteRepository.Add(vehicle);

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
            if (!VehicleValidations.VehicleIsValid(vehicle))
                return Forbid();

            var obj = (Vehicle)_beneficiaryReadOnlyRepository.Find(id);

            obj.VehicleBrand = vehicle.VehicleBrand;
            obj.VehicleChassisNumber = vehicle.VehicleChassisNumber;
            obj.VehicleColor = vehicle.VehicleColor;
            obj.VehicleCurrentFipeValue = vehicle.VehicleCurrentFipeValue;
            obj.VehicleCurrentMileage = vehicle.VehicleCurrentMileage;
            obj.VehicleDoneInspection = vehicle.VehicleDoneInspection;
            obj.VehicleManufactoringYear = vehicle.VehicleManufactoringYear;
            obj.VehicleModel = vehicle.VehicleModel;
            obj.VehicleModelYear = vehicle.VehicleModelYear;

            return Ok(_beneficiaryWriteRepository.Update(id, obj));
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
            var beneficiariesContracts = _contractsReadOnlyRepository.Get()
                .Where(cb => cb.BeneficiaryId == id)
                .ToList();

            if (beneficiariesContracts.Count > 0)
                return Forbid();

            var obj = _beneficiaryReadOnlyRepository.Find(id);

            if (obj != null)
            {
                obj.IsDeleted = !obj.IsDeleted;
                return Ok(_beneficiaryWriteRepository.Update(id, obj));
            }

            return NotFound(obj);
        }
    }
}
