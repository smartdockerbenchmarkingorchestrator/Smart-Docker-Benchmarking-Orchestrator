// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="StartBenchmarkExperimentCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class StartBenchmarkExperimentCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class StartBenchmarkExperimentCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartBenchmarkExperimentCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public StartBenchmarkExperimentCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }
    }
}
