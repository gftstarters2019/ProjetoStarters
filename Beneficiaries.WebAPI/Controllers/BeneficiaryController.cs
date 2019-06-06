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
        private readonly IReadOnlyRepository<Beneficiary> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<Beneficiary> _beneficiaryWriteRepository;
        private readonly IReadOnlyRepository<ContractBeneficiary> _contractsReadOnlyRepository;

        private readonly IReadOnlyRepository<Individual> _individualReadOnlyRepository;
        private readonly IReadOnlyRepository<Pet> _petReadOnlyRepository;
        private readonly IReadOnlyRepository<MobileDevice> _mobileDeviceReadOnlyRepository;
        private readonly IReadOnlyRepository<Realty> _realtyReadOnlyRepository;
        private readonly IReadOnlyRepository<Vehicle> _vehicleReadOnlyRepository;

        /// <summary>
        /// BeneficiaryController constructor
        /// </summary>
        public BeneficiaryController(IReadOnlyRepository<Beneficiary> beneficiaryReadOnlyRepository,
            IWriteRepository<Beneficiary> beneficiaryWriteRepository,
            IReadOnlyRepository<ContractBeneficiary> contractsReadOnlyRepository,
            IReadOnlyRepository<Individual> individualReadOnlyRepository,
            IReadOnlyRepository<Pet> petReadOnlyRepository,
            IReadOnlyRepository<MobileDevice> mobileDeviceReadOnlyRepository,
            IReadOnlyRepository<Realty> realtyReadOnlyRepository,
            IReadOnlyRepository<Vehicle> vehicleReadOnlyRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
            _contractsReadOnlyRepository = contractsReadOnlyRepository;
            _individualReadOnlyRepository = individualReadOnlyRepository;
            _petReadOnlyRepository = petReadOnlyRepository;
            _mobileDeviceReadOnlyRepository = mobileDeviceReadOnlyRepository;
            _realtyReadOnlyRepository = realtyReadOnlyRepository;
            _vehicleReadOnlyRepository = vehicleReadOnlyRepository;
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

            if (!IndividualIsValid(individual))
                return Forbid();

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
            if (!IndividualIsValid(individual))
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
            //mobileDevice.MobileDeviceId = Guid.NewGuid();

            if (!MobileDeviceIsValid(mobileDevice))
                return Forbid();

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
            if (!MobileDeviceIsValid(mobileDevice))
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
            //realty.RealtyId = Guid.NewGuid();

            if (!RealtyIsValid(realty))
                return Forbid();

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
            if (!RealtyIsValid(realty))
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
            //vehicle.VehicleId = Guid.NewGuid();

            if (!VehicleIsValid(vehicle))
                return Forbid();

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
            if (!VehicleIsValid(vehicle))
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
            if (_contractsReadOnlyRepository.Get().Where(cb => cb.BeneficiaryId == id).ToList().Count > 0)
                return Forbid();

            var obj = _beneficiaryReadOnlyRepository.Find(id);

            if (obj != null)
            {
                obj.IsDeleted = !obj.IsDeleted;
                return Ok(_beneficiaryWriteRepository.Update(id, obj));
            }

            return NotFound(obj);
        }

        #region Validations
        /// <summary>
        /// Does all validations to see if Individual is valid.
        /// </summary>
        /// <param name="individual">Individual to be verified</param>
        /// <returns>If Individual is valid</returns>
        public static bool IndividualIsValid(Individual individual)
        {
            if (!CPFIsValid(individual.IndividualCPF))
                return false;

            if (!EmailIsValid(individual.IndividualEmail))
                return false;

            if (!DateIsValid(individual.IndividualBirthdate))
                return false;
            return true;
        }

        

        /// <summary>
        /// Verifies if Realty is valid
        /// </summary>
        /// <param name="realty">Realty to be verified</param>
        /// <returns>If Realty is valid</returns>
        public static bool RealtyIsValid(Realty realty)
        {
            if (realty.RealtyMarketValue < 0 && realty.RealtySaleValue < 0)
                return false;

            //if (!CEPIsValid(realty.RealtyAddress.AddressZipCode))
            //    return false;

            if (!DateIsValid(realty.RealtyConstructionDate))
                return false;

            return true;
        }

        /// <summary>
        /// Verifies if Vehicle is valid
        /// </summary>
        /// <param name="vehicle">Vehicle to be verified</param>
        /// <returns>If Vehicle is valid</returns>
        public static bool VehicleIsValid(Vehicle vehicle)
        {
            if (!DateIsValid(vehicle.VehicleManufactoringYear))
                return false;

            if (!DateIsValid(vehicle.VehicleModelYear))
                return false;

            if (vehicle.VehicleCurrentFipeValue < 0 && vehicle.VehicleCurrentMileage <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Verifies if Mobile Device is valid
        /// </summary>
        /// <param name="mobileDevice">Mobile Device to be verified</param>
        /// <returns>If Mobile Device is valid</returns>
        public static bool MobileDeviceIsValid(MobileDevice mobileDevice)
        {
            if (!DateIsValid(mobileDevice.MobileDeviceManufactoringYear))
                return false;

            if (mobileDevice.MobileDeviceInvoiceValue <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Algorithm to verify if a string is a CPF
        /// </summary>
        /// <param name="cpf">String to be verified</param>
        /// <returns>If the string is a CPF</returns>
        public static bool CPFIsValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Verifies if an email is valid.
        /// </summary>
        /// <param name="emailaddress">Email to be verified</param>
        /// <returns>If email is valid</returns>
        public static bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if date is not future date
        /// </summary>
        /// <returns>If date is valid</returns>
        public static bool DateIsValid(DateTime date)
        {
            return date != null ? date > DateTime.Today : false;
        }

        /// <summary>
        /// Verifies if CEP is valid
        /// </summary>
        /// <param name="cep">CEP to be verified</param>
        /// <returns>If CEP is valid</returns>
        public static bool CEPIsValid(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, "[0-9]{5}-[0-9]{3}");
        }
        #endregion Validations
    }
}
