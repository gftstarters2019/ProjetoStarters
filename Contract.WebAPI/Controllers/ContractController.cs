using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public ContractController(IReadOnlyRepository<Backend.Core.Models.Contract> contractReadOnlyRepository, IWriteRepository<Backend.Core.Models.Contract> contractWriteRepository)
        {
            _contractReadOnlyRepository = contractReadOnlyRepository;
            _contractWriteRepository = contractWriteRepository;
        }

        // GET api/Contract
        [HttpGet]
        public IActionResult Contracts()
        {
            return Ok(_contractReadOnlyRepository.Get());
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
            //Implementar Validações
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
    }
}
