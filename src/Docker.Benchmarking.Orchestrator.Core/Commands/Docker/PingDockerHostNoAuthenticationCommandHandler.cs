// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="PingDockerHostNoAuthenticationCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class PingDockerHostNoAuthenticationCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.PingDockerHostNoAuthenticationCommand, System.Boolean}" />
    public class PingDockerHostNoAuthenticationCommandHandler : IRequestHandler<PingDockerHostNoAuthenticationCommand, bool>
    {
        /// <summary>
        /// The docker remote service
        /// </summary>
        private readonly IDockerRemoteService _dockerRemoteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingDockerHostNoAuthenticationCommandHandler"/> class.
        /// </summary>
        /// <param name="dockerRemoteService">The docker remote service.</param>
        public PingDockerHostNoAuthenticationCommandHandler(IDockerRemoteService dockerRemoteService)
        {
            _dockerRemoteService = dockerRemoteService;
        }
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(PingDockerHostNoAuthenticationCommand request, CancellationToken cancellationToken)
        {
            return await _dockerRemoteService.PingDockerHost(request.HostName, request.PortNumber);
        }
    }
}
