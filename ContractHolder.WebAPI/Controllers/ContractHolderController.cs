using Backend.Core;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
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
        private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<Individual> _contractHolderWriteRepository;

        /// <summary>
        /// Contract Holder Constructor
        /// </summary>
        /// <param name="contractHolderReadOnlyRepository"></param>
        /// <param name="contractHolderWriteRepository"></param>
        public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
        }

        /// <summary>
        /// Gets all contract holders registered.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ContractHolders()
        {
            return Ok(_contractHolderReadOnlyRepository.Get().Where(ch => !ch.IndividualDeleted));
        }

        /// <summary>
        /// Get a specific contract holder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult ContractHolder(Guid id)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Gets all deleted contract holders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedContractHolders()
        {
            return Ok(_contractHolderReadOnlyRepository.Get().Where(b => b.IndividualDeleted));
        }

        /// <summary>
        /// Creates a new Individual in the database
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostContractHolder([FromBody] Individual individual)
        {
            individual.IndividualId = Guid.NewGuid();

            _contractHolderWriteRepository.Add(individual);
            return Ok(individual);
        }

        /// <summary>
        /// Updates an contract holder in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="individual"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContractHolder(Guid id, [FromBody] Individual individual)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);

            obj.IndividualId = individual.IndividualId;
            obj.IndividualName = individual.IndividualName;
            obj.IndividualCPF = individual.IndividualCPF;
            obj.IndividualEmail = individual.IndividualEmail;
            obj.IndividualRG = individual.IndividualRG;
            obj.IndividualBirthdate = individual.IndividualBirthdate;

            return Ok(_contractHolderWriteRepository.Update(obj));

        }

        /// <summary>
        /// Deletes a beneficiary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContractHolder(Guid id)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);

            if (obj != null)
            {
                obj.IndividualDeleted = !obj.IndividualDeleted;
                return Ok(_contractHolderWriteRepository.Update(obj));
            }        
            
            return NotFound(obj);
        }
    }
}
