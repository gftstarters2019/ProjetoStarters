using Backend.Core.Commands;
using Backend.Core.Events;
using Backend.Infrastructure.ServiceBus.Contracts;
using AzureFunction.IoC;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction
{
    public static class ContractHolderFunction
    {
        [FunctionName("SendEmailContractHolderFunction")]
        public static void SendEmailContractHolder([ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")] string sendEmail, ILogger log, [Inject]IServiceBusClient busClient)
        {
            var contractholder = JsonConvert.DeserializeObject<SendEmailContractHolder>(sendEmail);
            busClient.PublishMessageToTopic(new EmailSentContractHolder(contractholder.ContractHolderDomain));
        }

        [FunctionName("EmailSentContractHolderFunction")]
        public static void EmailSentContractHolder([ServiceBusTrigger("%ServiceBusTopicName%", "%ServiceBusSubscriptionName%", Connection = "ServiceBusConnectionString")]string emailSent, ILogger log)
        {
            var contractHolder = JsonConvert.DeserializeObject<EmailSentContractHolder>(emailSent);
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {contractHolder.ContractHolderDomain}");
        }
    }
}
