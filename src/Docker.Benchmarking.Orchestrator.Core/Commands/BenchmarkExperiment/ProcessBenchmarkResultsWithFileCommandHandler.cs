// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 09-04-2018
// ***********************************************************************
// <copyright file="ProcessBenchmarkResultsWithFileCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class ProcessBenchmarkResultsWithFileCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.ProcessBenchmarkResultsWithFileCommand, System.Boolean}" />
    public class ProcessBenchmarkResultsWithFileCommandHandler : IRequestHandler<ProcessBenchmarkResultsWithFileCommand, bool>
    {
        /// <summary>
        /// The benchmark results service
        /// </summary>
        private readonly IBenchmarkResultsService _benchmarkResultsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessBenchmarkResultsWithFileCommandHandler"/> class.
        /// </summary>
        /// <param name="benchmarkResultsService">The benchmark results service.</param>
        public ProcessBenchmarkResultsWithFileCommandHandler(IBenchmarkResultsService benchmarkResultsService)
        {
            _benchmarkResultsService = benchmarkResultsService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(ProcessBenchmarkResultsWithFileCommand request, CancellationToken cancellationToken)
        {
            return await _benchmarkResultsService.ProcessResultsWithFile(request.FormFile, request.Experiment);
        }
    }
}
