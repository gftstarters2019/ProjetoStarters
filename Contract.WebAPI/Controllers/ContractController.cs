using Backend.Core.Domains;
using Backend.Core.Enums;
using Backend.Services.Services.Interfaces;
using Contract.WebAPI.Factories;
using Contract.WebAPI.ViewModels;
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
        private readonly IService<CompleteContractDomain> _contractService;

        /// <summary>
        /// ContractController constructor
        /// </summary>
        public ContractController(IService<CompleteContractDomain> contractService)
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
            return Ok(_contractService.GetAll().Select(con => FactoriesManager.ContractViewModel.Create(con)));//.ToList());
        }

        /// <summary>
        /// Gets the contracts' categories
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet("Categories")]
        public IActionResult Categories()
        {
            var categories = new Dictionary<int, string>();
            foreach (ContractCategory foo in Enum.GetValues(typeof(ContractCategory)))
            {
                categories.Add((int)foo, foo.ToString());
            }
            return Ok(categories);
        }

        /// <summary>
        /// Gets the contract types
        /// </summary>
        /// <returns>Types</returns>
        [HttpGet("Types")]
        public IActionResult Types()
        {
            var types = new Dictionary<int, string>();
            foreach (ContractType foo in Enum.GetValues(typeof(ContractType)))
            {
                types.Add((int)foo, foo.ToString());
            }
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
            var contractToAdd = FactoriesManager.CompleteContractDomain.Create(contract);

            try
            {
                var addedContract = _contractService.Save(contractToAdd);
                if (addedContract == null)
                    return StatusCode(403);

                return Ok(FactoriesManager.ContractViewModel.Create(addedContract));
            }
            catch (Exception e)
            {
                string errors = e.Message;

                return ValidationProblem(new ValidationProblemDetails()
                    {
                        Type = "Model Validation Error",
                        Detail = errors
                    }
                );
            }
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
            var contractToUpdate = FactoriesManager.CompleteContractDomain.Create(contractViewModel);
            var updatedContract = _contractService.Update(id, contractToUpdate);
            if (updatedContract == null)
                return StatusCode(403);
            
            return Ok(FactoriesManager.ContractViewModel.Create(updatedContract));
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
                Ok(id);

            return StatusCode(403);
        }
    }
}
