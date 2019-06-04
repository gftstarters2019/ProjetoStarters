﻿using Backend.Core.Enums;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IReadOnlyRepository<Backend.Core.Models.Contract> _contractReadOnlyRepository;
        private readonly IWriteRepository<Backend.Core.Models.Contract> _contractWriteRepository;
        private readonly IReadOnlyRepository<Backend.Core.Models.SignedContract> _signedContractReadOnlyRepository;

        public ContractController(IReadOnlyRepository<Backend.Core.Models.Contract> contractReadOnlyRepository, IWriteRepository<Backend.Core.Models.Contract> contractWriteRepository, IReadOnlyRepository<Backend.Core.Models.SignedContract> signedContractReadOnlyRepository)
        {
            _contractReadOnlyRepository = contractReadOnlyRepository;
            _contractWriteRepository = contractWriteRepository;
            _signedContractReadOnlyRepository = signedContractReadOnlyRepository;
        }

        // GET api/Contract
        [HttpGet]
        public IActionResult Contracts()
        {
            return Ok(_contractReadOnlyRepository.Get());
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

        // GET api/Contract/5
        [HttpGet("{id}")]
        public IActionResult Contract(Guid id)
        {
            var obj = _contractReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult PostContract([FromBody] Backend.Core.Models.Contract contract)
        {
            if (!ContractIsValid(contract))
                return StatusCode(403); //Forbbiden

            _contractWriteRepository.Add(contract);
            return Ok(contract);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContract(Guid id, [FromBody] Backend.Core.Models.Contract contract)
        {
            //Implementar Validações
            var obj = _contractReadOnlyRepository.Find(id);

            obj.ContractId = contract.ContractId;

            return Ok(_contractWriteRepository.Update(obj));
        }

        /// <summary>
        /// Soft deletes a Contract
        /// </summary>
        /// <param name="id">GUID of the chosen Contract</param>
        /// <returns>Deleted Contract</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(Guid id)
        {
            if (_signedContractReadOnlyRepository.Get().Where(sc => sc.ContractIndividualIsActive).ToList().Count > 0)
                return Forbid();

            var contract = _contractReadOnlyRepository.Find(id);

            if (contract != null)
            {
                contract.ContractDeleted = !contract.ContractDeleted;
                return Ok(_contractWriteRepository.Update(contract));
            }

            return NotFound(contract);
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
