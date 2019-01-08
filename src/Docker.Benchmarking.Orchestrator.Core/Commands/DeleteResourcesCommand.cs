using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class DeleteResourcesCommand : IRequest
    {
        public DeleteResourcesCommand(Guid azureHostId)
        {
            AzureHostId = azureHostId;
        }

        public Guid AzureHostId { get; }
    }
}
