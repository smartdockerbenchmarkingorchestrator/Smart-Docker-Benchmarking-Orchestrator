// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="StopBenchmarkingExperimentCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class StopBenchmarkingExperimentCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.StopBenchmarkingExperimentCommand, System.Boolean}" />
    public class StopBenchmarkingExperimentCommandHandler : IRequestHandler<StopBenchmarkingExperimentCommand, bool>
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IBenchmarkOrchestratorService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopBenchmarkingExperimentCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public StopBenchmarkingExperimentCommandHandler(IBenchmarkOrchestratorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(StopBenchmarkingExperimentCommand request, CancellationToken cancellationToken)
        {
            return await _service.EndBenchmarkExperiment(request.Experiment);
        }
    }
}
