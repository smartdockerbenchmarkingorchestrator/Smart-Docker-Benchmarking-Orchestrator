using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class CreateResourcesForBenchmarkCommand : IRequest<string[]>
    {
        public CreateResourcesForBenchmarkCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
