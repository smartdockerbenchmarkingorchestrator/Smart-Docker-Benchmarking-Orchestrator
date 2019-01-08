// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-29-2018
//
// Last Modified By : ***********
// Last Modified On : 08-29-2018
// ***********************************************************************
// <copyright file="AWSEstimatedCostsCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class AWSEstimatedCostsCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AWS.AWSEstimatedCostsCommand, System.String}" />
    public class AWSEstimatedCostsCommandHandler : IRequestHandler<AWSEstimatedCostsCommand, string>
    {
        /// <summary>
        /// The aws service
        /// </summary>
        private readonly IAWSResourcesService _awsService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSEstimatedCostsCommandHandler"/> class.
        /// </summary>
        /// <param name="awsService">The aws service.</param>
        public AWSEstimatedCostsCommandHandler(IAWSResourcesService awsService)
        {
            _awsService = awsService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> Handle(AWSEstimatedCostsCommand request, CancellationToken cancellationToken)
        {
            return await _awsService.EstimateCostsForTemplate(request.Template, request.Parameters, request.Credentials);
        }
    }
}
