// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-30-2018
// ***********************************************************************
// <copyright file="AzureDeleteResourcesByNameCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureDeleteResourcesByNameCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AzureDeleteResourcesByNameCommand, System.Boolean}" />
    public class AzureDeleteResourcesByNameCommandHandler : IRequestHandler<AzureDeleteResourcesByNameCommand, bool>
    {
        /// <summary>
        /// The azure resource service
        /// </summary>
        private readonly IAzureResourceService _azureResourceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDeleteResourcesByNameCommandHandler"/> class.
        /// </summary>
        /// <param name="azureResourceService">The azure resource service.</param>
        public AzureDeleteResourcesByNameCommandHandler(IAzureResourceService azureResourceService)
        {
            _azureResourceService = azureResourceService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(AzureDeleteResourcesByNameCommand request, CancellationToken cancellationToken)
        {
            await _azureResourceService.DeleteResourcesByResourceName(request.ResourceName, request.CredentialsId);
            return true;
        }
    }
}
