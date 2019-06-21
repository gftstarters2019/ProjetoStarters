using Backend.Application.ViewModels;
using Backend.Core.Enums;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.WebAPI.Controllers
{
    /// <summary>
    /// Contract API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Backend.Core.Models.Contract> _contractRepository;
        private readonly IRepository<Backend.Core.Models.SignedContract> _signedContractRepository;

        private readonly IRepository<ContractViewModel> _contractViewModelRepository;

        /// <summary>
        /// ContractController constructor
        /// </summary>
        public ContractController(IRepository<Backend.Core.Models.Contract> contractRepository,
                                  IRepository<Backend.Core.Models.SignedContract> signedContractRepository,
                                  IRepository<ContractViewModel> contractViewModelRepository)
        {
            _contractRepository = contractRepository;
            _signedContractRepository = signedContractRepository;
            _contractViewModelRepository = contractViewModelRepository;
        }

        /// <summary>
        /// Gets all Contracts
        /// </summary>
        /// <returns>All Contracts</returns>
        [HttpGet]
        public IActionResult Contracts()
        {
            return Ok(_contractViewModelRepository.Get());
        }

        /// <summary>
        /// Gets the contracts' categories
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet("Categories")]
        public IActionResult Categories()
        {
            var categories = new Dictionary<int, string>
            {
                { 0, "iron" },
                { 1, "bronze" },
                { 2, "silver" },
                { 3, "gold" },
                { 4, "platinum" },
                { 5, "diamond" }
            };
            return Ok(categories);
        }

        /// <summary>
        /// Gets the contract types
        /// </summary>
        /// <returns>Types</returns>
        [HttpGet("Types")]
        public IActionResult Types()
        {
            var types = new Dictionary<int, string>
            {
                { 0, "HealthPlan" },
                { 1, "AnimalHealthPlan" },
                { 2, "DentalPlan" },
                { 3, "LifeInsurance" },
                { 4, "RealStateInsurance" },
                { 5, "VehicleInsurance" },
                { 6, "MobileDeviceInsurance" }
            };
            return Ok(types);
        }

        /// <summary>
        /// Gets a specific Contract
        /// </summary>
        /// <param name="id">ID of the chosen Contract</param>
        /// <returns>Chosen Contract</returns>
        [HttpGet("{id}")]
        public IActionResult Contract(Guid id)
        {
            var obj = _contractRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Contract
        /// </summary>
        /// <param name="contract">Contract to be created</param>
        /// <returns>Created Contract</returns>
        [HttpPost]
        public IActionResult PostContract([FromBody] ContractViewModel contract)
        {
            if (!_contractViewModelRepository.Add(contract))
                return StatusCode(403);

            return Ok();
        }

        /// <summary>
        /// Updates a Contract
        /// </summary>
        /// <param name="id">GUID of the Contract to update</param>
        /// <param name="contractViewModel">ContractViewModel with updated values</param>
        /// <returns>Updated Contract</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContract(Guid id, [FromBody] ContractViewModel contractViewModel)
        {
            var updatedContract = _contractViewModelRepository.Update(id, contractViewModel);
            if (updatedContract == null)
                return StatusCode(403);
            return Ok(updatedContract);
        }

        /// <summary>
        /// Soft deletes a Contract
        /// </summary>
        /// <param name="id">GUID of the chosen Contract</param>
        /// <returns>Deleted Contract</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(Guid id)
        {
            if (_signedContractRepository.Get()
                .Where(sc => sc.ContractIndividualIsActive && sc.ContractId == id).ToList().Count > 0)
                return Forbid();

            var signedContract = _signedContractRepository.Find(id);

            if (signedContract != null)
            {
                var contract = _contractRepository.Find(signedContract.ContractId);
                if (!signedContract.ContractIndividualIsActive)
                {
                    contract.ContractDeleted = !contract.ContractDeleted;
                    return Ok(_contractRepository.Update(signedContract.ContractId, contract));
                }
                else
                {
                    return Forbid();
                }
            }

            return NotFound(signedContract);
        }

        #region Validations
        /// <summary>
        /// Verifies if Contract is valid
        /// </summary>
        /// <param name="contract">Contract to be verified</param>
        /// <returns>If Contract is valid</returns>
        public static bool ContractIsValid(Backend.Core.Models.Contract contract)
        {
            if (!Enum.IsDefined(typeof(ContractType), contract.ContractType) || !Enum.IsDefined(typeof(ContractCategory), contract.ContractCategory))
                return false;
            if (contract.ContractExpiryDate < DateTime.Now.Date)
                return false;

            return true;
        }
        #endregion Validations
    }
}
