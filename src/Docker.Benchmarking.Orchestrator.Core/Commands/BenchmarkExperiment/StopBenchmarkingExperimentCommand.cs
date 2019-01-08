// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="StopBenchmarkingExperimentCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class StopBenchmarkingExperimentCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class StopBenchmarkingExperimentCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopBenchmarkingExperimentCommand"/> class.
        /// </summary>
        /// <param name="experiment">The experiment.</param>
        public StopBenchmarkingExperimentCommand(Entities.BenchmarkExperiment experiment)
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
