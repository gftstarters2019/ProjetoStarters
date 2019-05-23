using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contract.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IReadOnlyRepository<Address> _contractReadOnlyRepository;
        private readonly IWriteRepository<Address> _contractWriteRepository;

        public ContractController(IReadOnlyRepository<Address> contractReadOnlyRepository, IWriteRepository<Address> contractWriteRepository)
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
        public IActionResult PostContract([FromBody] Address contract)
        {
            //Implementar Validações
            _contractWriteRepository.Add(contract);
            return Ok(contract);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContract(Guid id, [FromBody] Address contract)
        {
            //Implementar Validações
            var obj = _contractReadOnlyRepository.Find(id);

            //obj.id = contract.id;

            return Ok(_contractWriteRepository.Update(obj));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContract(Guid id)
        {
            //Implementar Validações
            var obj = _contractReadOnlyRepository.Find(id);

            if (obj != null)
                return Ok(_contractWriteRepository.Remove(obj));

            return NotFound(obj);
        }
    }
}
