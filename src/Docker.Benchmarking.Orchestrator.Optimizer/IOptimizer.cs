using Docker.Benchmarking.Orchestrator.Optimizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Optimizer
{
    public interface IOptimizer
    {
        List<OptimisedResult> SingleProvider(List<CloudServiceProvider> cloudListHost, CloudServiceProvider benchmarkHost, decimal maxCost, out decimal estimateCost);


        List<CloudServiceProvider> SortF(List<CloudServiceProvider> cloudListHost1, List<CloudServiceProvider> cloudListHost2, CloudServiceProvider benchmarkHost, decimal maxCost);

    }
}
