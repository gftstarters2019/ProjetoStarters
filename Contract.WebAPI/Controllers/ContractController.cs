using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Services.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Contract.WebAPI.Controllers
{
    /// <summary>
    /// Contract API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IService<CompleteContract> _contractService;

        /// <summary>
        /// ContractController constructor
        /// </summary>
        public ContractController(IService<CompleteContract> contractService)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// Gets all Contracts
        /// </summary>
        /// <returns>All Contracts</returns>
        [HttpGet]
        public IActionResult Contracts()
        {
            return Ok(_contractService.GetAll());
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
            var obj = _contractService.Get(id);
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
            if(_contractService.Save(new CompleteContract()) == null)
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
            var updatedContract = _contractService.Update(id, new CompleteContract());
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
            if (_contractService.Delete(id) != null)
                Ok();
            return StatusCode(403);
        }
    }
}
