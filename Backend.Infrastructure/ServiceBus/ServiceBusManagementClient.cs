using Backend.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.ServiceBus
{
    internal class ServiceBusManagementClient : IServiceBusManagementClient
    {
        private readonly ManagementClient _managementClient;

        public ServiceBusManagementClient(ManagementClient managementClient)
        {
            _managementClient = managementClient;
        }

        public async Task CreateQueue(string queueName)
        {
            if (!await _managementClient.QueueExistsAsync(queueName))
            {
                // Configure Topic Settings.
                QueueDescription queueDescription = new QueueDescription(queueName)
                {
                    EnablePartitioning = true,
                    EnableDeadLetteringOnMessageExpiration = true,
                    MaxSizeInMB = 5 * 1024,
                    DefaultMessageTimeToLive = TimeSpan.FromMinutes(5)
                };
                await _managementClient.CreateQueueAsync(queueDescription);
            }
        }

        public async Task DeleteQueue(string queueName)
        {
            if (await _managementClient.QueueExistsAsync(queueName))
            {
                await _managementClient.DeleteQueueAsync(queueName);
            }
        }

        public async Task CreateTopic(string topicName)
        {
            if (!await _managementClient.TopicExistsAsync(topicName))
            {
                // Configure Topic Settings.
                TopicDescription topicDescription = new TopicDescription(topicName)
                {
                    EnablePartitioning = true,
                    MaxSizeInMB = 5 * 1024,
                    DefaultMessageTimeToLive = TimeSpan.FromMinutes(5)
                };
                await _managementClient.CreateTopicAsync(topicDescription);
            }
        }

        public async Task DeleteTopic(string topicName)
        {
            if (await _managementClient.TopicExistsAsync(topicName))
            {
                await _managementClient.DeleteTopicAsync(topicName);
            }
        }

        public async Task CreateSubscription(string topicName, string subscriptionName, IList<Type> filters = null)
        {
            if (!await _managementClient.SubscriptionExistsAsync(topicName, subscriptionName))
            {
                // Configure Topic Settings.
                SubscriptionDescription topicDescription = new SubscriptionDescription(topicName, subscriptionName)
                {
                    EnableDeadLetteringOnMessageExpiration = true,
                    DefaultMessageTimeToLive = TimeSpan.FromDays(10),
                    AutoDeleteOnIdle = TimeSpan.FromDays(1)

                };
                await _managementClient.CreateSubscriptionAsync(topicDescription);
            }

            if (filters != null && filters.Any())
            {
                IList<RuleDescription> rules = await _managementClient.GetRulesAsync(topicName, subscriptionName);

                if (rules.Any(x => x.Name == RuleDescription.DefaultRuleName))
                {
                    await _managementClient.DeleteRuleAsync(topicName, subscriptionName, RuleDescription.DefaultRuleName);
                }

                foreach (Type filter in filters)
                {
                    if (rules.Any(x => x.Name == filter.Name)) { continue; }

                    await _managementClient.CreateRuleAsync(topicName, subscriptionName,
                        new RuleDescription(filter.Name, new CorrelationFilter { Label = filter.FullName }));
                }
            }
        }

        public async Task DeleteSubscription(string topicName, string subscriptionName)
        {
            if (await _managementClient.SubscriptionExistsAsync(topicName, subscriptionName))
            {
                await _managementClient.DeleteSubscriptionAsync(topicName, subscriptionName);
            }
        }
    }
}
