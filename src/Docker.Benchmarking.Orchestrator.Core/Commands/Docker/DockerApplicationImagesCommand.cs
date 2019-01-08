// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="DockerApplicationImagesCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class DockerApplicationImagesCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.List{Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage}}" />
    public class DockerApplicationImagesCommand : IRequest<List<DockerImage>>
    {

    }
}
