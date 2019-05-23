using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Beneficiaries.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IReadOnlyRepository<Address> _beneficiaryReadOnlyRepository;
        private readonly IWriteRepository<Address> _beneficiaryWriteRepository;

        public BeneficiaryController(IReadOnlyRepository<Address> beneficiaryReadOnlyRepository, IWriteRepository<Address> beneficiaryWriteRepository)
        {
            _beneficiaryReadOnlyRepository = beneficiaryReadOnlyRepository;
            _beneficiaryWriteRepository = beneficiaryWriteRepository;
        }

        // GET: api/Beneficiary
        [HttpGet]
        public IActionResult Beneficiaries()
        {
            return Ok(_beneficiaryReadOnlyRepository.Get());
        }

        // GET: api/Beneficiary/5
        [HttpGet("{id}")]
        public IActionResult Beneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult PostBeneficiary([FromBody] Address beneficiary)
        {
            _beneficiaryWriteRepository.Add(beneficiary);

            return Ok(beneficiary);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBeneficiary(Guid id, [FromBody] Address beneficiary)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);

            //obj.id = beneficiary.id;

            return Ok(_beneficiaryWriteRepository.Update(obj));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBeneficiary(Guid id)
        {
            var obj = _beneficiaryReadOnlyRepository.Find(id);

            if (obj != null)
                return Ok(_beneficiaryWriteRepository.Remove(obj));

            return NotFound(obj);
        }
    }
}
