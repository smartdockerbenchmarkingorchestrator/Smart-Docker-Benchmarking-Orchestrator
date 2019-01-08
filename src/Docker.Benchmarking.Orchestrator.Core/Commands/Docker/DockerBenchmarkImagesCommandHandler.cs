// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="DockerBenchmarkImagesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class DockerBenchmarkImagesCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.DockerBenchmarkImagesCommand, System.Collections.Generic.List{Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage}}" />
    public class DockerBenchmarkImagesCommandHandler : IRequestHandler<DockerBenchmarkImagesCommand, List<DockerImage>>
    {
        /// <summary>
        /// The repo
        /// </summary>
        private readonly IDockerImageService _repo;
        /// <summary>
        /// Initializes a new instance of the <see cref="DockerBenchmarkImagesCommandHandler"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public DockerBenchmarkImagesCommandHandler(IDockerImageService repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;DockerImage&gt;&gt;.</returns>
        public Task<List<DockerImage>> Handle(DockerBenchmarkImagesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repo.BenchmarkingImages());
        }
    }
}
