// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-30-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="AWSTestDeploymentScriptCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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

namespace Docker.Benchmarking.Orchestrator.Core.Commands.AWS
{
    /// <summary>
    /// Class AWSTestDeploymentScriptCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AWS.AWSTestDeploymentScriptCommand, System.String}" />
    public class AWSTestDeploymentScriptCommandHandler : IRequestHandler<AWSTestDeploymentScriptCommand, string>
    {
        /// <summary>
        /// The backgroung job client
        /// </summary>
        private readonly IBackgroundJobClient _backgroungJobClient;
        /// <summary>
        /// The aws service
        /// </summary>
        private readonly IAWSResourcesService _awsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AWSTestDeploymentScriptCommandHandler"/> class.
        /// </summary>
        /// <param name="awsService">The aws service.</param>
        /// <param name="backgroungJobClient">The backgroung job client.</param>
        public AWSTestDeploymentScriptCommandHandler(IAWSResourcesService awsService, IBackgroundJobClient backgroungJobClient)
        {
            _awsService = awsService;
            _backgroungJobClient = backgroungJobClient;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> Handle(AWSTestDeploymentScriptCommand request, CancellationToken cancellationToken)
        {
            var parentJobId = _backgroungJobClient.Enqueue<IAWSResourcesService>(c => c.CreateDockerVM(
                request.StackName, request.Json, request.Parameters, request.CredentialsId));

            if(request.DockerImageId != Guid.Empty)
                _backgroungJobClient.ContinueWith<IAWSResourcesService>(parentJobId, x => x.DeployImageToVM(request.StackName, request.CredentialsId, request.DockerImageId));

            return Task.FromResult(parentJobId);
        }
    }
}
