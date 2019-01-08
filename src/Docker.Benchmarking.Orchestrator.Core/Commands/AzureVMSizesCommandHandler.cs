// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="AzureVMSizesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureVMSizesCommandHandler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AzureVMSizesCommand, System.Collections.Generic.IEnumerable{Microsoft.Azure.Management.Compute.Fluent.Models.VirtualMachineSizeInner}}" />
    public class AzureVMSizesCommandHandler : IRequestHandler<AzureVMSizesCommand, IEnumerable<VirtualMachineSizeInner>>
    {
        /// <summary>
        /// The azure resource service
        /// </summary>
        private readonly IAzureResourceService _azureResourceService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureVMSizesCommandHandler"/> class.
        /// </summary>
        /// <param name="azureResourceService">The azure resource service.</param>
        public AzureVMSizesCommandHandler(IAzureResourceService azureResourceService)
        {
            _azureResourceService = azureResourceService;
        }
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;VirtualMachineSizeInner&gt;&gt;.</returns>
        public async Task<IEnumerable<VirtualMachineSizeInner>> Handle(AzureVMSizesCommand request, CancellationToken cancellationToken)
        {
            return await _azureResourceService.GetVMSizes(request.Location, request.CredentialsId);
        }
    }
}
