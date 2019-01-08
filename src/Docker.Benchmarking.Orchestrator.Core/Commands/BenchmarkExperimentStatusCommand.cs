using Docker.Benchmarking.Orchestrator.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class BenchmarkExperimentStatusCommand : IRequest<ExperimentStatus>
    {
        public BenchmarkExperimentStatusCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
