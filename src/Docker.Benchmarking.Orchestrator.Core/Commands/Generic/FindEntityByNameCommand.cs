using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class FindEntityByNameCommand<T> : IRequest<bool>
    {
        public FindEntityByNameCommand(string name, Guid id = default(Guid))
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }

        public Guid Id { get; }
    }
}
