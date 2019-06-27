using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Services.Services.Interfaces;
using Beneficiaries.WebAPI.Factories;
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
        private readonly IService<Backend.Core.Models.BeneficiaryEntity> _beneficiaryService;
        private readonly IService<IndividualDomain> _individualService;
        private readonly IService<MobileDeviceDomain> _mobileDeviceService;
        private readonly IService<PetDomain> _petService;
        private readonly IService<RealtyDomain> _realtyService;
        private readonly IService<VehicleDomain> _vehicleService;

        /// <summary>
        /// BeneficiaryController constructor
        /// </summary>
        public BeneficiaryController(IService<Backend.Core.Models.BeneficiaryEntity> beneficiaryService,
                                     IService<IndividualDomain> individualService,
                                     IService<MobileDeviceDomain> mobileDeviceService,
                                     IService<PetDomain> petService,
                                     IService<RealtyDomain> realtyService,
                                     IService<VehicleDomain> vehicleService)
        {
            _beneficiaryService = beneficiaryService;
            _individualService = individualService;
            _mobileDeviceService = mobileDeviceService;
            _petService = petService;
            _realtyService = realtyService;
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Gets all beneficiaries registered.
        /// </summary>
        /// <returns>Beneficiaries registered</returns>
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all deleted beneficiaries.
        /// </summary>
        /// <returns>Beneficiaries deleted</returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedBeneficiaries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a specific beneficiary.
        /// </summary>
        /// <param name="id">GUID value representing a BeneficiaryId</param>
        /// <returns>Chosen beneficiary</returns>
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            return Ok(_beneficiaryService.Get(id));
        }

        #region Individual
        /// <summary>
        /// Gets all Individuals beneficiaries.
        /// </summary>
        /// <returns>Individuals</returns>
        [HttpGet("Individuals")]
        public IActionResult GetIndividuals()
        {
            return Ok(_individualService.GetAll().Select(ind => FactoriesManager.IndividualViewModel.Create(ind)));
        }

        /// <summary>
        /// Creates a new Individual in the database
        /// </summary>
        /// <param name="individual">Individual without IDs</param>
        /// <returns>Created individual, with IDs</returns>
        [HttpPost("Individual")]
        public IActionResult PostIndividual([FromBody] IndividualEntity individual)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            return Ok(_mobileDeviceService.GetAll().Select(mob => FactoriesManager.MobileDeviceViewModel.Create(mob)));
        }

        /// <summary>
        /// Creates a new Mobile Device in the database
        /// </summary>
        /// <param name="mobileDevice">Mobile Device without IDs</param>
        /// <returns>Created mobile device, with IDs</returns>
        [HttpPost("MobileDevice")]
        public IActionResult PostMobileDevice([FromBody] MobileDeviceEntity mobileDevice)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            return Ok(_petService.GetAll().Select(pet => FactoriesManager.PetViewModel.Create(pet)));
        }

        /// <summary>
        /// Creates a new Pet in the database
        /// </summary>
        /// <param name="pet">Pet without IDs</param>
        /// <returns>Created pet, with IDs</returns>
        [HttpPost("Pet")]
        public IActionResult PostPet([FromBody] PetEntity pet)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            return Ok(_realtyService.GetAll().Select(real => FactoriesManager.RealtyViewModel.Create(real)).Where(real => real != null));
        }

        /// <summary>
        /// Creates a new Realty in the database
        /// </summary>
        /// <param name="realty">Realty without IDs</param>
        /// <returns>Created realty, with IDs</returns>
        [HttpPost("Realty")]
        public IActionResult PostRealty([FromBody] RealtyEntity realty)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            return Ok(_vehicleService.GetAll().Select(veh => FactoriesManager.VehicleViewModel.Create(veh)));
        }

        /// <summary>
        /// Creates a new Vehicle in the database
        /// </summary>
        /// <param name="vehicle">Vehicle without IDs</param>
        /// <returns>Created vehicle, with IDs</returns>
        [HttpPost("Vehicle")]
        public IActionResult PostVehicle([FromBody] VehicleEntity vehicle)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            var deletedBeneficiary = _beneficiaryService.Delete(id);
            if (deletedBeneficiary == null)
                return NotFound();
            return Ok();
        }
    }
}
