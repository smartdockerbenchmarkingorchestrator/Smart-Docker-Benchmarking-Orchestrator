// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="StartBenchmarkExperimentStep2Command.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class StartBenchmarkExperimentStep2Command.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class StartBenchmarkExperimentStep2Command : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartBenchmarkExperimentStep2Command"/> class.
        /// </summary>
        /// <param name="experiment">The experiment.</param>
        public StartBenchmarkExperimentStep2Command(Entities.BenchmarkExperiment experiment)
        {
            Experiment = experiment;
        }

        /// <summary>
        /// Gets the experiment.
        /// </summary>
        /// <value>The experiment.</value>
        public Entities.BenchmarkExperiment Experiment { get; }
    }
}
