using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands
{
    public class CreateOptimizedExperimentCommand : IRequest<string>
    {
        public int ExperimentLengthInSeconds { get { return BenchmarkTimeLength / 1000; } }

        public int ConcurrentUsers { get; set; }

        public decimal MaxCost { get; set; }
        public string Name { get; set; }
        public Guid Application { get; set; }
        public CloudProvider HostCloudProvider { get; set; }
        public CloudProvider BenchmarkCloudProvier { get; set; }

        public int BenchmarkTimeLength { get; set; }

        public string BenchmarkingName { get; set; }

        public string ExperimentReference { get; set; }

        public IEnumerable<BenchmarkExperimentVariable> Variables { get; set; }

        public double ApdexTSeconds { get; set; }

        public Guid ApacheTestFileId { get; set; }
    }
}
