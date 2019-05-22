using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Contract.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IReadOnlyRepository<APITeste> _contractReadOnlyRepository;
        private readonly IWriteRepository<APITeste> _contractWriteRepository;

        public ContractController(IReadOnlyRepository<APITeste> contractReadOnlyRepository, IWriteRepository<APITeste> contractWriteRepository)
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
        public IActionResult PostContract([FromBody] APITeste contract)
        {
            //Implementar Validações
            _contractWriteRepository.Add(contract);
            return Ok(contract);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
