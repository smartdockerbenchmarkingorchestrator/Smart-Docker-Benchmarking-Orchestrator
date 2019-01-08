// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-31-2018
//
// Last Modified By : ***********
// Last Modified On : 08-31-2018
// ***********************************************************************
// <copyright file="AWSDeleteResourcesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.AWS
{
    /// <summary>
    /// Class AWSDeleteResourcesCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AWS.AWSDeleteResourcesCommand, System.Boolean}" />
    public class AWSDeleteResourcesCommandHandler : IRequestHandler<AWSDeleteResourcesCommand, bool>
    {
        /// <summary>
        /// The aws resources service
        /// </summary>
        private readonly IAWSResourcesService _awsResourcesService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSDeleteResourcesCommandHandler"/> class.
        /// </summary>
        /// <param name="awsResourcesService">The aws resources service.</param>
        public AWSDeleteResourcesCommandHandler(IAWSResourcesService awsResourcesService)
        {
            _awsResourcesService = awsResourcesService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(AWSDeleteResourcesCommand request, CancellationToken cancellationToken)
        {
            return await _awsResourcesService.DeleteStack(request.StackName, request.CredentialsId);
        }
    }
}
