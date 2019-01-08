using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class JmeterTestFileForBenchmarkCommand : IRequest<string>
    {
        public JmeterTestFileForBenchmarkCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
