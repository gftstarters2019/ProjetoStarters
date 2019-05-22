using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beneficiaries.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        //private readonly IReadOnlyRepository<Beneficiary> _beneficiaryReadOnlyRepository;
        //private readonly IWriteRepository<Beneficiary> _beneficiaryWriteRepository;

        public BeneficiaryController()
        {

        }

        // GET: api/Beneficiary
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok();
        }

        // GET: api/Beneficiary/5
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            //var obj = _beneficiaryReadOnlyRepository.Find(id);
            //return Ok(obj);
            return Ok();
        }
    }
}
