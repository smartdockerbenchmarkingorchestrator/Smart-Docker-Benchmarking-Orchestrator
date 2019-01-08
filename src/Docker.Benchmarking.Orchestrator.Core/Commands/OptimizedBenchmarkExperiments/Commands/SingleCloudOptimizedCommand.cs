using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Optimizer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands
{
    public class SingleCloudOptimizedCommand : IRequest<IEnumerable<OptimisedResult>>
    {
        public int ExperimentLengthInSeconds { get { return BenchmarkTimeLength / 1000; } }

        public int BenchmarkTimeLength { get; set; }

        public int ConcurrentUsers { get; set; }

        public CloudProvider HostCloudProvider { get; set; }
        public CloudProvider BenchmarkCloudProvier { get; set; }

        public decimal MaxCost { get; set; }
    }
}
