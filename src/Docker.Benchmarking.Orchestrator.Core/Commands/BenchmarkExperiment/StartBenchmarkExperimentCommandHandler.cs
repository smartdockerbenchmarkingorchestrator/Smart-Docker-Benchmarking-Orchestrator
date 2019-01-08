// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="StartBenchmarkExperimentCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class StartBenchmarkExperimentCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.StartBenchmarkExperimentCommand, System.Boolean}" />
    public class StartBenchmarkExperimentCommandHandler : IRequestHandler<StartBenchmarkExperimentCommand, bool>
    {
        /// <summary>
        /// The benchmark orch service
        /// </summary>
        private readonly IBenchmarkOrchestratorService _benchmarkOrchService;
        /// <summary>
        /// Initializes a new instance of the <see cref="StartBenchmarkExperimentCommandHandler"/> class.
        /// </summary>
        /// <param name="benchmarkOrchService">The benchmark orch service.</param>
        public StartBenchmarkExperimentCommandHandler(IBenchmarkOrchestratorService benchmarkOrchService)
        {
            _benchmarkOrchService = benchmarkOrchService;
        }
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(StartBenchmarkExperimentCommand request, CancellationToken cancellationToken)
        {
            return await _benchmarkOrchService.StartBenchmarkingStep1(request.Id);
        }
    }
}
