﻿using Backend.Core.Domains;
using Backend.Services.Services.Interfaces;
using ContractHolder.WebAPI.Factories;
using ContractHolder.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ContractHolder.WebAPI.Controllers
{
    /// <summary>
    /// Contract Holder API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IService<ContractHolderDomain> _contractHolderService;

        /// <summary>
        /// ContractHolderController constructor
        /// </summary>
        public ContractHolderController(IService<ContractHolderDomain> contractHolderService)
        {
            _contractHolderService = contractHolderService;
        }

        /// <summary>
        /// Gets all Contract Holders
        /// </summary>
        /// <returns>All Contract Holders</returns>
        [HttpGet]
        public IActionResult ContractHolders()
        {
            return Ok(_contractHolderService.GetAll().Select(ch => FactoriesManager.ContractHolderViewModel.Create(ch)));
        }

        /// <summary>
        /// Gets a single Contract Holder
        /// </summary>
        /// <param name="id">GUID of the chosen Contract Holder</param>
        /// <returns>Chosen Contract Holder</returns>
        [HttpGet("{id}")]
        public IActionResult ContractHolder(Guid id)
        {
            var obj = _contractHolderService.Get(id);

            if (obj == null)
                return NotFound();

            return Ok(FactoriesManager.ContractHolderViewModel.Create(obj));
        }

        /// <summary>
        /// Creates a new Contract Holder
        /// </summary>
        /// <param name="contractHolderViewModel">Contract Holder to be created</param>
        /// <returns>Created Contract Holder</returns>
        [HttpPost]
        public IActionResult PostContractHolder([FromBody] ContractHolderViewModel contractHolderViewModel)
        {           
            try
            {
                var addedContractHolder = _contractHolderService.Save(FactoriesManager.ContractHolderDomain.Create(contractHolderViewModel));
                if (addedContractHolder == null)
                    return StatusCode(403);
                return Ok(FactoriesManager.ContractHolderViewModel.Create(addedContractHolder));
            }
            catch (Exception e)
            {
                string errors = e.Message;

                return ValidationProblem(new ValidationProblemDetails()
                {
                    Type = "Validation Error",
                    Detail = errors
                }
                );
            }      
        }

        /// <summary>
        /// Updates a Contract Holder
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContractHolder(Guid id, [FromBody] ContractHolderViewModel vm)
        {
            try
            {
                var updatedContractHolder = _contractHolderService.Update(id, FactoriesManager.ContractHolderDomain.Create(vm));

                if (updatedContractHolder == null)
                    return StatusCode(403);

                return Ok(FactoriesManager.ContractHolderViewModel.Create(updatedContractHolder));
            }
            catch (Exception e)
            {
                string errors = e.Message;

                return ValidationProblem(new ValidationProblemDetails()
                {
                    Type = "Validation Error",
                    Detail = errors
                });
            }
        }

        /// <summary>
        /// Soft deletes a Contract Holder from the DB
        /// </summary>
        /// <param name="id">GUID of the Contract Holder</param>
        /// <returns>Deleted Contract Holder</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContractHolder(Guid id)
        {
            var contractHolder = _contractHolderService.Delete(id);

            if (contractHolder != null)
                return Ok(FactoriesManager.ContractHolderViewModel.Create(contractHolder));

            return StatusCode(403);
        }
    }
}
