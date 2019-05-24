using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IReadOnlyRepository<Backend.Core.Models.Contract> _contractReadOnlyRepository;
        private readonly IWriteRepository<Backend.Core.Models.Contract> _contractWriteRepository;

        /// <summary>
        /// Contract Constructor
        /// </summary>
        /// <param name="contractReadOnlyRepository"></param>
        /// <param name="contractWriteRepository"></param>
        public ContractController(IReadOnlyRepository<Backend.Core.Models.Contract> contractReadOnlyRepository, IWriteRepository<Backend.Core.Models.Contract> contractWriteRepository)
        {
            _contractReadOnlyRepository = contractReadOnlyRepository;
            _contractWriteRepository = contractWriteRepository;
        }

        /// <summary>
        /// Gets all contracts registered
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Contracts()
        {
            return Ok(_contractReadOnlyRepository.Get().Where(c => !c.ContractDeleted));
        }

        /// <summary>
        /// Get a specific contract.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Contract(Guid id)
        {
            var obj = _contractReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Gets all deleted contracts.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Deleted")]
        public IActionResult DeletedContracts()
        {
            return Ok(_contractReadOnlyRepository.Get().Where(c => c.ContractDeleted));
        }

        /// <summary>
        /// Creates a new Contract in the database.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostContract([FromBody] Backend.Core.Models.Contract contract)
        {
            contract.ContractId = Guid.NewGuid();

            _contractWriteRepository.Add(contract);
            return Ok(contract);
        }

        /// <summary>
        /// Updates a contract in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContract(Guid id, [FromBody] Backend.Core.Models.Contract contract)
        {
            var obj = _contractReadOnlyRepository.Find(id);

            obj.ContractId = contract.ContractId;
            obj.ContractCategory = contract.ContractCategory;
            obj.ContractExpiryDate = contract.ContractExpiryDate;
            obj.ContractInitialDate = contract.ContractInitialDate;
            obj.ContractType = contract.ContractType;

            return Ok(_contractWriteRepository.Update(obj));
        }

        /// <summary>
        /// Deletes a contract
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(Guid id)
        {
            var obj = _contractReadOnlyRepository.Find(id);

            if (obj != null)
            {
                obj.ContractDeleted = !obj.ContractDeleted;
                return Ok(_contractWriteRepository.Update(obj));
            }

            return NotFound(obj);
        }
    }
}
