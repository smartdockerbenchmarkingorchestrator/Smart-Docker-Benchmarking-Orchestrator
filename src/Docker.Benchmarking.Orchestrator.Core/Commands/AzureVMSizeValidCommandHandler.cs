// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-28-2018
// ***********************************************************************
// <copyright file="AzureVMSizeValidCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
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
    /// Class AzureVMSizeValidCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AzureVMSizeValidCommand, System.Boolean}" />
    public class AzureVMSizeValidCommandHandler : IRequestHandler<AzureVMSizeValidCommand, bool>
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IAzureVMService _service;
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureVMSizeValidCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public AzureVMSizeValidCommandHandler(IAzureVMService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(AzureVMSizeValidCommand request, CancellationToken cancellationToken)
        {
            return await _service.IsVMSizeUnique(request.VMSize, request.Id);
        }
    }
}
