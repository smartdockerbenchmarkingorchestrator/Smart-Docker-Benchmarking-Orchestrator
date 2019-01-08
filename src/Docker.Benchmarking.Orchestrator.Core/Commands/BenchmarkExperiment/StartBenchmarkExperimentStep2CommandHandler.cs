// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 09-06-2018
// ***********************************************************************
// <copyright file="StartBenchmarkExperimentStep2CommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class StartBenchmarkExperimentStep2CommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.StartBenchmarkExperimentStep2Command, System.Boolean}" />
    public class StartBenchmarkExperimentStep2CommandHandler : IRequestHandler<StartBenchmarkExperimentStep2Command, bool>
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IBenchmarkOrchestratorService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartBenchmarkExperimentStep2CommandHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public StartBenchmarkExperimentStep2CommandHandler(IBenchmarkOrchestratorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(StartBenchmarkExperimentStep2Command request, CancellationToken cancellationToken)
        {
            return await (_service.StartBenchmarkingStep2(request.Experiment));
        }
    }
}
