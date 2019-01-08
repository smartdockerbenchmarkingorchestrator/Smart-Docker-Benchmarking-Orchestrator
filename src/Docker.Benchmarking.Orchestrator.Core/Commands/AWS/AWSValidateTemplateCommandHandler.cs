// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="AWSValidateTemplateCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AWSValidateTemplateCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AWSValidateTemplateCommand, System.Collections.Generic.List{System.String}}" />
    public class AWSValidateTemplateCommandHandler : IRequestHandler<AWSValidateTemplateCommand, List<string>>
    {
        /// <summary>
        /// The aws service
        /// </summary>
        private readonly IAWSResourcesService _awsService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSValidateTemplateCommandHandler"/> class.
        /// </summary>
        /// <param name="awsService">The aws service.</param>
        public AWSValidateTemplateCommandHandler(IAWSResourcesService awsService)
        {
            _awsService = awsService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        public async Task<List<string>> Handle(AWSValidateTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _awsService.ValidateDeploymentScript(request.Template, request.Credentials);
        }
    }
}
