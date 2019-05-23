using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Function.IoC
{
    internal static class IoC
    {
        public static readonly ConcurrentDictionary<Guid, IServiceScope> Scopes =
            new ConcurrentDictionary<Guid, IServiceScope>();

        public static IServiceProvider ServiceProvider;
    }
}
