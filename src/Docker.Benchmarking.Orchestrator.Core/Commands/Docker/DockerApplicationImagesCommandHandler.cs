// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-28-2018
// ***********************************************************************
// <copyright file="DockerApplicationImagesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class DockerApplicationImagesCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.DockerApplicationImagesCommand, System.Collections.Generic.List{Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage}}" />
    public class DockerApplicationImagesCommandHandler : IRequestHandler<DockerApplicationImagesCommand, List<DockerImage>>
    {
        /// <summary>
        /// The repo
        /// </summary>
        private readonly IDockerImageService _repo;
        /// <summary>
        /// Initializes a new instance of the <see cref="DockerApplicationImagesCommandHandler"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public DockerApplicationImagesCommandHandler(IDockerImageService repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;DockerImage&gt;&gt;.</returns>
        public Task<List<DockerImage>> Handle(DockerApplicationImagesCommand request, CancellationToken cancellationToken)
        {
            //return null;
            return Task.FromResult(_repo.ApplicationImages());
        }
    }
}
