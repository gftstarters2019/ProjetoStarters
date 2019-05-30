using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusSettings
    {
        string ConnectionString { get; }
        string QueueName { get; }
        IList<Type> Filters { get; }
    }
}
