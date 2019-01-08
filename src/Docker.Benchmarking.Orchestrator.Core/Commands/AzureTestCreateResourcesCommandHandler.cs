// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-31-2018
// ***********************************************************************
// <copyright file="AzureTestCreateResourcesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Hangfire;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureTestCreateResourcesCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AzureTestCreateResourcesCommand, System.String}" />
    public class AzureTestCreateResourcesCommandHandler : IRequestHandler<AzureTestCreateResourcesCommand, string>
    {
        /// <summary>
        /// The backgroung job client
        /// </summary>
        private readonly IBackgroundJobClient _backgroungJobClient;

        /// <summary>
        /// The azure resource service
        /// </summary>
        private readonly IAzureResourceService _azureResourceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTestCreateResourcesCommandHandler"/> class.
        /// </summary>
        /// <param name="azureResourceService">The azure resource service.</param>
        /// <param name="backgroungJobClient">The backgroung job client.</param>
        public AzureTestCreateResourcesCommandHandler(IAzureResourceService azureResourceService, IBackgroundJobClient backgroungJobClient)
        {
            _azureResourceService = azureResourceService;
            _backgroungJobClient = backgroungJobClient;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> Handle(AzureTestCreateResourcesCommand request, CancellationToken cancellationToken)
        {
            var parentJobId = _backgroungJobClient.Enqueue<IAzureResourceService>(c => c.TestCreateResources(
                request.ResourceName, request.Region, request.DeploymentJson, request.DeploymentParameters, request.CredentialsId));

            if (request.DockerImageId != Guid.Empty)
                _backgroungJobClient.ContinueWith<IAzureResourceService>(parentJobId, x => x.DeployImageToAzureHost(request.ResourceName, request.CredentialsId, request.DockerImageId));

            return await Task.FromResult(parentJobId);
        }
    }
}
