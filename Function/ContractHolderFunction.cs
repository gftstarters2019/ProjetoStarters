using System;
using System.Collections.Generic;
using System.Text;
using Backend.Core.Commands;
//using Backend.Core.Events;
using Backend.Infrastructure.ServiceBus.Contracts;
using Function.IoC;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Function
{
    public static class ContractHolderFunction
    {
        [FunctionName("CreateContractHolderFunction")]
        public static void CreateContractHolder([ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")]string createContractHolder, ILogger log, [Inject]IServiceBusClient busClient)
        {
            var contractHolder = JsonConvert.DeserializeObject<CreateContractHolder>(createContractHolder);
            //busClient.PublishMessageToTopic(new ContractHolderCreated(contractHolder.ContractHolder));
        }

        //[FunctionName("ContractHolderCreatedFunction")]
        //public static void ContractHolderCreated([ServiceBusTrigger("%ServiceBusTopicName%", "%ServiceBusSubscriptionName%", Connection = "ServiceBusConnectionString")]string contractHolderCreated, ILogger log)
        //{
        //    var contractHolder = JsonConvert.DeserializeObject<ContractHolderCreated>(contractHolderCreated);
        //    log.LogInformation($"C# ServiceBus queue trigger function processed message: {contractHolder.ContractHolder.IndividualName}");
        //}
    }
}
