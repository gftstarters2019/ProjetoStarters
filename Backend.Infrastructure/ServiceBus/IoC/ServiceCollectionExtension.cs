using Backend.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.ServiceBus.IoC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServiceBus(this IServiceCollection services, IServiceBusSettings settings)
        {
            var managementClient = new ManagementClient(settings.ConnectionString);
            IServiceBusManagementClient serviceBusManagementClient = new ServiceBusManagementClient(managementClient);
            services.AddSingleton(managementClient);
            services.AddSingleton(serviceBusManagementClient);

            var testeQueueCliente = new QueueClient(settings.ConnectionString, settings.QueueName);
            testeQueueCliente.ServiceBusConnection.TransportType = TransportType.AmqpWebSockets;
            services.AddSingleton<IQueueClient>(testeQueueCliente);

            //services.AddSingleton<ISubscriptionClient>(new SubscriptionClient(settings.ConnectionString, settings.TopicName, settings.SubscriptionName));
            services.AddSingleton<IServiceBusClient, ServiceBusClient>();

            serviceBusManagementClient.CreateQueue(settings.QueueName).Wait();
            //serviceBusManagementClient.CreateSubscription(settings.SubscriptionName, settings.Filters).Wait();
            //_queueClient.ServiceBusConnection.TransportType = TransportType.AmqpWebSockets;
            return services;
        }
    }
}
