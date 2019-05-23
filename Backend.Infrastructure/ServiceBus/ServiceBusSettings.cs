using Backend.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.ServiceBus
{
    public class ServiceBusSettings : IServiceBusSettings
    {
        public ServiceBusSettings(string connectionString, string queueName, string topicName, string subscriptionName, IList<Type> filters = null)
        {
            ConnectionString = connectionString;
            QueueName = queueName;
            TopicName = topicName;
            SubscriptionName = subscriptionName;
            Filters = filters ?? new List<Type>();
        }

        public string ConnectionString { get; }
        public string QueueName { get; }
        public string TopicName { get; }
        public string SubscriptionName { get; }
        public IList<Type> Filters { get; }
    }
}
