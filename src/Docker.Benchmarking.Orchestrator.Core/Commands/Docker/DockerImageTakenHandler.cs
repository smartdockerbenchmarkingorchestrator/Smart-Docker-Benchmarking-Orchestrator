// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-24-2018
// ***********************************************************************
// <copyright file="DockerImageTakenHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class DockerImageTakenHandler.
    /// </summary>
    /// <seealso cref="MediatR.RequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.DockerImageTakenCommand, System.Boolean}" />
    public class DockerImageTakenHandler : RequestHandler<DockerImageTakenCommand, bool>
    {
        /// <summary>
        /// The docker image service
        /// </summary>
        private readonly IDockerImageService _dockerImageService;
        /// <summary>
        /// Initializes a new instance of the <see cref="DockerImageTakenHandler"/> class.
        /// </summary>
        /// <param name="dockerImageService">The docker image service.</param>
        public DockerImageTakenHandler(IDockerImageService dockerImageService)
        {
            _dockerImageService = dockerImageService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool Handle(DockerImageTakenCommand request)
        {
            return _dockerImageService.DockerImageNameTaken(request.ImageName);
        }
    }
}
