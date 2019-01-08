// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-25-2018
// ***********************************************************************
// <copyright file="ListDockerImagesHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class ListDockerImagesHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.ListDockerImagesCommand, System.Collections.Generic.IEnumerable{Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage}}" />
    public class ListDockerImagesHandler : IRequestHandler<ListDockerImagesCommand, IEnumerable<DockerImage>>
    {

        /// <summary>
        /// The docker image repository
        /// </summary>
        private readonly IRepository<DockerImage> _dockerImageRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDockerImagesHandler"/> class.
        /// </summary>
        /// <param name="dockerImageRepository">The docker image repository.</param>
        public ListDockerImagesHandler(IRepository<DockerImage> dockerImageRepository)
        {
            _dockerImageRepository = dockerImageRepository;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;DockerImage&gt;&gt;.</returns>
        public async Task<IEnumerable<DockerImage>> Handle(ListDockerImagesCommand request, CancellationToken cancellationToken)
        {
            return await _dockerImageRepository.ListAsync();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
