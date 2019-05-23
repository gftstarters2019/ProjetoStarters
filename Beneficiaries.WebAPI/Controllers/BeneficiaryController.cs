using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Beneficiaries.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IReadOnlyRepository<Beneficiary> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<Beneficiary> _beneficiaryWriteRepository;

        public BeneficiaryController(IReadOnlyRepository<Beneficiary> beneficiaryReadOnlyRepository, IWriteRepository<Beneficiary> beneficiaryWriteRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
        }

        // GET: api/Beneficiary
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get());
        }

        // GET: api/Beneficiary/5
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        #region Individual
        [HttpPost("Individual")]
        public IActionResult PostIndividual([FromBody] Individual individual)
        {
            individual.BeneficiaryId = Guid.NewGuid();
            individual.IndividualId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(individual);

            return Ok(individual);
        }

        [HttpPut("Individual/{id}")]
        public IActionResult UpdateIndividual(Guid id, [FromBody] Individual individual)
        {
            var obj = (Individual) _beneficiaryReadOnlyRepository.Find(id);
            
            obj.IndividualBirthdate = individual.IndividualBirthdate;
            obj.IndividualCPF = individual.IndividualCPF;
            obj.IndividualDeleted = individual.IndividualDeleted;
            obj.IndividualEmail = individual.IndividualEmail;
            obj.IndividualName = individual.IndividualName;
            obj.IndividualRG = individual.IndividualRG;
            
            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Individual

        #region MobileDevice
        [HttpPost("MobileDevice")]
        public IActionResult PostMobileDevice([FromBody] MobileDevice mobileDevice)
        {
            mobileDevice.BeneficiaryId = Guid.NewGuid();
            mobileDevice.MobileDeviceId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(mobileDevice);

            return Ok(mobileDevice);
        }

        [HttpPut("MobileDevice/{id}")]
        public IActionResult UpdateMobileDevice(Guid id, [FromBody] MobileDevice mobileDevice)
        {
            var obj = (MobileDevice) _beneficiaryReadOnlyRepository.Find(id);

            obj.MobileDeviceBrand = mobileDevice.MobileDeviceBrand;
            obj.MobileDeviceDeleted = mobileDevice.MobileDeviceDeleted;
            obj.MobileDeviceInvoiceValue = mobileDevice.MobileDeviceInvoiceValue;
            obj.MobileDeviceManufactoringYear = mobileDevice.MobileDeviceManufactoringYear;
            obj.MobileDeviceModel = mobileDevice.MobileDeviceModel;
            obj.MobileDeviceSerialNumber = mobileDevice.MobileDeviceSerialNumber;
            obj.MobileDeviceType = mobileDevice.MobileDeviceType;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion MobileDevice

        #region Pet
        [HttpPost("Pet")]
        public IActionResult PostPet([FromBody] Pet pet)
        {
            pet.BeneficiaryId = Guid.NewGuid();
            pet.PetId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(pet);

            return Ok(pet);
        }

        [HttpPut("Pet/{id}")]
        public IActionResult UpdatePet(Guid id, [FromBody] Pet pet)
        {
            var obj = (Pet) _beneficiaryReadOnlyRepository.Find(id);

            obj.PetBirthdate = pet.PetBirthdate;
            obj.PetBreed = pet.PetBreed;
            obj.PetDeleted = pet.PetDeleted;
            obj.PetName = pet.PetName;
            obj.PetSpecies = pet.PetSpecies;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Pet

        #region Realty
        [HttpPost("Realty")]
        public IActionResult PostRealty([FromBody] Realty realty)
        {
            realty.BeneficiaryId = Guid.NewGuid();
            realty.RealtyId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(realty);

            return Ok(realty);
        }

        [HttpPut("Realty/{id}")]
        public IActionResult UpdateRealty(Guid id, [FromBody] Realty realty)
        {
            var obj = (Realty) _beneficiaryReadOnlyRepository.Find(id);

            obj.RealtyAddress = realty.RealtyAddress;
            obj.RealtyConstructionDate = realty.RealtyConstructionDate;
            obj.RealtyDeleted = realty.RealtyDeleted;
            obj.RealtyMarketValue = realty.RealtyMarketValue;
            obj.RealtyMunicipalRegistration = realty.RealtyMunicipalRegistration;
            obj.RealtySaleValue = realty.RealtySaleValue;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }
        #endregion Realty

        #region Vehicle
        [HttpPost("Vehicle")]
        public IActionResult PostVehicle([FromBody] Vehicle vehicle)
        {
            vehicle.BeneficiaryId = Guid.NewGuid();
            vehicle.VehicleId = Guid.NewGuid();

            _beneficiaryWriteRepository.Add(vehicle);

            return Ok(vehicle);
        }

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

        [HttpDelete("{id}")]
        public IActionResult DeleteIndividual(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);

            if (obj != null)
                return Ok(_beneficiaryWriteRepository.Remove(obj));

            return NotFound(obj);
        }
    }
}
