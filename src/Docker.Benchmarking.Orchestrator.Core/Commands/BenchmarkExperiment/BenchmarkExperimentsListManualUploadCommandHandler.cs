// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="BenchmarkExperimentsListManualUploadCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class BenchmarkExperimentsListManualUploadCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.BenchmarkExperimentsListManualUploadCommand, System.Collections.Generic.IEnumerable{Docker.Benchmarking.Orchestrator.Core.Entities.BenchmarkExperiment}}" />
    public class BenchmarkExperimentsListManualUploadCommandHandler : IRequestHandler<BenchmarkExperimentsListManualUploadCommand, IEnumerable<Entities.BenchmarkExperiment>>
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IBenchmarkExperimentService _service;
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkExperimentsListManualUploadCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BenchmarkExperimentsListManualUploadCommandHandler(IBenchmarkExperimentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;BenchmarkExperiment&gt;&gt;.</returns>
        public async Task<IEnumerable<Entities.BenchmarkExperiment>> Handle(BenchmarkExperimentsListManualUploadCommand request, CancellationToken cancellationToken)
        {
            return await _service.ExperimentsRequiringManualUpload();
        }
    }
}
