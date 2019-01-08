using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Optimizer.Models
{
    public class OptimisedResult
    {
        public CloudServiceProvider HostVM { get; set; }

        public CloudServiceProvider BenchmarkVM { get; set; }

        public decimal TotalCostForExperiment { get { return (HostVM.TotalCost + BenchmarkVM.TotalCost);  } }
    }
}
