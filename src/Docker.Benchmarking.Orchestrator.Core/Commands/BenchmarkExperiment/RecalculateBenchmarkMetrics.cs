using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.BenchmarkExperiment
{
    public class RecalculateBenchmarkMetrics : IRequest<bool>
    {
        public RecalculateBenchmarkMetrics(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
