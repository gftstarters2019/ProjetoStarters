﻿using Backend.Core.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction
{
    public static class ContractHolderFunction
    {
        [FunctionName("WelcomeEmailSentFunction")]
        public static void WelcomeEmailSent([ServiceBusTrigger("%ServiceBusTopicName%", "%ServiceBusSubscriptionName%", Connection = "ServiceBusConnectionString")]string emailSent, ILogger log)
        {
            var contractHolder = JsonConvert.DeserializeObject<WelcomeEmailSent>(emailSent);
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {contractHolder.Email}");
        }
    }
}
