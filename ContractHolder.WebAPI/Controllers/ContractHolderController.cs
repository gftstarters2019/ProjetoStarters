using Backend.Core.Commands;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Infrastructure.ServiceBus.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ContractHolder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<Individual> _contractHolderWriteRepository;
        private readonly IReadOnlyRepository<SignedContract> _contractsReadOnlyRepository;
        private readonly IServiceBusClient _busClient;

        public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository, IReadOnlyRepository<SignedContract> contractsReadOnlyRepository, IServiceBusClient busClient)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
            _contractsReadOnlyRepository = contractsReadOnlyRepository;
            _busClient = busClient;
        }

        // GET api/ContractHolder
        [HttpGet]
        public IActionResult ContractHolders()
        {
            return Ok(_contractHolderReadOnlyRepository.Get());
        }

        // GET api/ContractHolder/5
        [HttpGet("{id}")]
        public IActionResult ContractHolder(Guid id)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult PostContractHolder([FromBody] Individual individual)
        {
            _contractHolderWriteRepository.Add(individual);
            _busClient.SendMessageToQueue(new CreateContractHolder(individual)).Wait();
            return Ok(individual);
        }

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
    }
}
