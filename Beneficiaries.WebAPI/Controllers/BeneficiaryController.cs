using Backend.Core;
using Backend.Infrastructure.Repositories.Contracts;
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
        private readonly IReadOnlyRepository<APITeste> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<APITeste> _beneficiaryWriteRepository;

        public BeneficiaryController(IReadOnlyRepository<APITeste> beneficiaryReadOnlyRepository, IWriteRepository<APITeste> beneficiaryWriteRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
        }

        // GET: api/Beneficiary
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok("teste");
        }

        // GET: api/Beneficiary/5
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult PostOwner([FromBody] APITeste beneficiary)
        {
            _beneficiaryWriteRepository.Add(beneficiary);

            return Ok(beneficiary);
        }
    }
}
