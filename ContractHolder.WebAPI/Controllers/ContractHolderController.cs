using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ContractHolder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<Individual> _contractHolderWriteRepository;
        private readonly IReadOnlyRepository<SignedContract> _contractsReadOnlyRepository;

        /// <summary>
        /// ContractHolderController constructor
        /// </summary>
        public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository, IReadOnlyRepository<SignedContract> contractsReadOnlyRepository)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
            _contractsReadOnlyRepository = contractsReadOnlyRepository;
        }

        /// <summary>
        /// Gets all Contract Holders
        /// </summary>
        /// <returns>All Contract Holders</returns>
        [HttpGet]
        public IActionResult ContractHolders()
        {
            return Ok(_contractHolderReadOnlyRepository.Get());
        }

        /// <summary>
        /// Gets a single Contract Holder
        /// </summary>
        /// <param name="id">GUID of the chosen Contract Holder</param>
        /// <returns>Chosen Contract Holder</returns>
        [HttpGet("{id}")]
        public IActionResult ContractHolder(Guid id)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Contract Holder
        /// </summary>
        /// <param name="individual">Contract Hodler to be created</param>
        /// <returns>Created Contract Holder</returns>
        [HttpPost]
        public IActionResult PostContractHolder([FromBody] Individual individual)
        {
            _contractHolderWriteRepository.Add(individual);
            SendWelcomeEmail(individual);
            return Ok(individual);
        }

        /// <summary>
        /// Updates a Contract Holder
        /// </summary>
        /// <param name="id">GUID of the chosen Contract Holder</param>
        /// <param name="individual">Updated Contract Holder object</param>
        /// <returns>Updated Contract Holder</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContractHolder(Guid id, [FromBody] Individual individual)
        {
            //Implementar Validações
            var obj = _contractHolderReadOnlyRepository.Find(id);

            obj.IndividualId = individual.IndividualId;

            return Ok(_contractHolderWriteRepository.Update(obj));

        }

        /// <summary>
        /// Soft deletes a Contract Holder from the DB
        /// </summary>
        /// <param name="id">GUID of the Contract Holder</param>
        /// <returns>Deleted Contract Holder</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContractHolder(Guid id)
        {
            if (_contractsReadOnlyRepository.Get().Where(sc => sc.IndividualId == id).ToList().Count > 0)
                return Forbid();

            var contractHolder = _contractHolderReadOnlyRepository.Find(id);

            if (contractHolder != null)
            {
                contractHolder.IsDeleted = !contractHolder.IsDeleted;
                return Ok(_contractHolderWriteRepository.Update(contractHolder));
            }
            else return NotFound(contractHolder);
        }

        /// <summary>
        /// Sends welcome email to Contract Holder
        /// </summary>
        /// <param name="individual">Individual to send the email</param>
        public void SendWelcomeEmail(Individual individual)
        {
            new EmailService().SendEmail("Welcome!",
                $"Welcome {individual.IndividualName}!",
                individual.IndividualEmail);
        }
    }
}
