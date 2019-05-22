using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContractHolder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IReadOnlyRepository<APITeste> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<APITeste> _contractHolderWriteRepository;

        public ContractHolderController(IReadOnlyRepository<APITeste> contractHolderReadOnlyRepository, IWriteRepository<APITeste> contractHolderWriteRepository)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
        }

        // GET api/ContractHolder
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ContractHolder/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/ContractHolder
        [HttpPost]
        public IActionResult PostContractHolder([FromBody] APITeste individual)
        {
            _contractHolderWriteRepository.Add(individual);
            return Ok(individual);
        }

        // PUT api/ContractHolder/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ContractHolder/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
