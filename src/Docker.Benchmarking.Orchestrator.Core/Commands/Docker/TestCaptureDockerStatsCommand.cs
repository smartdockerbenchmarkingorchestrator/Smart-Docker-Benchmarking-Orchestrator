// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-06-2018
//
// Last Modified By : ***********
// Last Modified On : 09-06-2018
// ***********************************************************************
// <copyright file="TestCaptureDockerStatsCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.Docker
{
    /// <summary>
    /// Class TestCaptureDockerStatsCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{Docker.Benchmarking.Orchestrator.Core.Entities.ContainerMetric}}" />
    public class TestCaptureDockerStatsCommand : IRequest<IEnumerable<ContainerMetric>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaptureDockerStatsCommand"/> class.
        /// </summary>
        public TestCaptureDockerStatsCommand()
        {

        }


    }
}
