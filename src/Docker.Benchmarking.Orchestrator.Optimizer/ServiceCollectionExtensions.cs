using Microsoft.Extensions.DependencyInjection;
using System;

namespace Docker.Benchmarking.Orchestrator.Optimizer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOptimzer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IOptimizer, Optimizer>();
            // and a lot more Services

            return serviceCollection;
        }
    }
}
