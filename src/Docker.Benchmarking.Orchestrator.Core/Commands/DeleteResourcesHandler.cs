using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class DeleteResourcesHandler : AsyncRequestHandler<DeleteResourcesCommand>
    {
        public readonly IAzureResourceService _azureResourceService;
        public DeleteResourcesHandler(IAzureResourceService azureResourceService)
        {
            _azureResourceService = azureResourceService;
        }

        protected override async Task Handle(DeleteResourcesCommand request, CancellationToken cancellationToken)
        {
            await _azureResourceService.DeleteResources(request.AzureHostId);
        }
    }
}
