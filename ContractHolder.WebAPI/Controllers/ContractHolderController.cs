using Backend.Core.Commands;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Infrastructure.ServiceBus.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContractHolder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<Individual> _contractHolderWriteRepository;
        private readonly IServiceBusClient _busClient;

        public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository, IServiceBusClient busClient)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
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
            //Implementar Validações
            //_contractHolderWriteRepository.Add(individual);
            _busClient.SendMessageToQueue(new CreateContractHolder(individual));
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

        [HttpDelete("{id}")]
        public IActionResult DeleteContractHolder(Guid id)
        {
            //Implementar Validações
            var obj = _contractHolderReadOnlyRepository.Find(id);

            if (obj != null)
                return Ok(_contractHolderWriteRepository.Remove(obj));

            return NotFound(obj);
        }
    }
}
