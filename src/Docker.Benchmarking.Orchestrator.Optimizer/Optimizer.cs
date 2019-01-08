using Docker.Benchmarking.Orchestrator.Optimizer.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Docker.Benchmarking.Orchestrator.Optimizer.Models;

namespace Docker.Benchmarking.Orchestrator.Optimizer
{
    public class Optimizer : IOptimizer
    {
        public List<OptimisedResult> SingleProvider(List<CloudServiceProvider> cloudListHost, CloudServiceProvider benchmarkHost, decimal maxCost, out decimal estimateCost)
        {
            estimateCost = 0;

            cloudListHost.RemoveAll(c => c.CloudProvider == benchmarkHost.CloudProvider);

            cloudListHost = cloudListHost.OrderBy(c => c.CostPerHour).ThenBy(c => c.VMSize).ToList();

            var optimizdList = new List<OptimisedResult>();

            foreach (var vm in cloudListHost)
            {
                var totalCost = vm.TotalCost + benchmarkHost.TotalCost;

                if ((estimateCost + totalCost) > maxCost)
                    break;

                optimizdList.Add(new OptimisedResult
                {
                    HostVM = vm,
                    BenchmarkVM = benchmarkHost
                });

                estimateCost += totalCost;            
            }

            return optimizdList;
        }

        public List<CloudServiceProvider> SortF(List<CloudServiceProvider> cloudListHost1, List<CloudServiceProvider> cloudListHost2, CloudServiceProvider benchmarkHost, decimal maxCost)
        {
            throw new NotImplementedException();
        }
    }
}
