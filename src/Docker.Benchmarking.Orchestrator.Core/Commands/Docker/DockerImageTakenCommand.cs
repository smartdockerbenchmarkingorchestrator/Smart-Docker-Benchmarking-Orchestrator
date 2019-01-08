// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-24-2018
// ***********************************************************************
// <copyright file="DockerImageTakenCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class DockerImageTakenCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class DockerImageTakenCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>The name of the image.</value>
        public string ImageName { get; set; }
    }
}
