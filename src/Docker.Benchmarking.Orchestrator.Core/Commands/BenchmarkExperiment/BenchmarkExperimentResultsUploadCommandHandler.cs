// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="BenchmarkExperimentResultsUploadCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class BenchmarkExperimentResultsUploadCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.BenchmarkExperimentResultsUploadCommand, System.String}" />
    public class BenchmarkExperimentResultsUploadCommandHandler : IRequestHandler<BenchmarkExperimentResultsUploadCommand, string>
    {
        /// <summary>
        /// The benchmark results service
        /// </summary>
        private readonly IBenchmarkResultsService _benchmarkResultsService;
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkExperimentResultsUploadCommandHandler"/> class.
        /// </summary>
        /// <param name="benchmarkResultsService">The benchmark results service.</param>
        public BenchmarkExperimentResultsUploadCommandHandler(IBenchmarkResultsService benchmarkResultsService)
        {
            _benchmarkResultsService = benchmarkResultsService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> Handle(BenchmarkExperimentResultsUploadCommand request, CancellationToken cancellationToken)
        {
            return await _benchmarkResultsService.SaveUploadFile(request.FormFile);
        }
    }
}
