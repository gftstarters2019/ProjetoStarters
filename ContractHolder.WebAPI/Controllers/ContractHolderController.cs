using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContractHolder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        //private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        //private readonly IWriteRepository<Individual> _contractHolderWriteRepository;

        //public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository)
        //{
        //    _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
        //    _contractHolderWriteRepository = contractHolderWriteRepository;
        //}

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        //[HttpPost]
        //public IActionResult PostContractHolder([FromBody] Individual individual)
        //{
        //    _contractHolderWriteRepository.Add(individual);
        //    return Ok(individual);
        //}
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
