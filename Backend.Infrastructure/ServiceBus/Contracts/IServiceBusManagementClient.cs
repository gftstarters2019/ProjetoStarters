using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusManagementClient
    {
        Task CreateQueue(string queueName);
        Task DeleteQueue(string queueName);
        //Task CreateSubscription(string subscriptionName, IList<Type> filters = null);
        //Task DeleteSubscription(string subscriptionName);
    }
}
