// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-27-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="CreateResourcesForBenchmarkCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Hangfire;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class CreateResourcesForBenchmarkCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.CreateResourcesForBenchmarkCommand, System.String[]}" />
    public class CreateResourcesForBenchmarkCommandHandler : IRequestHandler<CreateResourcesForBenchmarkCommand, string[]>
    {
        /// <summary>
        /// The backgroung job client
        /// </summary>
        private readonly IBackgroundJobClient _backgroungJobClient;
        /// <summary>
        /// The azure resource service
        /// </summary>
        private readonly IAzureResourceService _azureResourceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateResourcesForBenchmarkCommandHandler"/> class.
        /// </summary>
        /// <param name="backgroungJobClient">The backgroung job client.</param>
        public CreateResourcesForBenchmarkCommandHandler(IBackgroundJobClient backgroungJobClient)
        {
            //_azureResourceService = azureResourceService;
            _backgroungJobClient = backgroungJobClient;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String[]&gt;.</returns>
        public Task<string[]> Handle(CreateResourcesForBenchmarkCommand request, CancellationToken cancellationToken)
        {
            var awsJobId = _backgroungJobClient.Enqueue<IAWSHostService>(c => c.CreateVMForBenchmark(request.Id));
            var azureJobId = _backgroungJobClient.Enqueue<IAzureResourceService>(c => c.CreateVMForBenchmark(request.Id));

            return Task.FromResult(new string[] { awsJobId, azureJobId });
        }
    }
}
