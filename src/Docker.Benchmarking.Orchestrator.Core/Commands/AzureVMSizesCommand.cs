// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-30-2018
// ***********************************************************************
// <copyright file="AzureVMSizesCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using System;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureVMSizesCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{Microsoft.Azure.Management.Compute.Fluent.Models.VirtualMachineSizeInner}}" />
    public class AzureVMSizesCommand : IRequest<IEnumerable<VirtualMachineSizeInner>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureVMSizesCommand"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        public AzureVMSizesCommand(string location, Guid credentialsId)
        {
            Location = location;
            CredentialsId = credentialsId;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; }
        /// <summary>
        /// Gets the credentials identifier.
        /// </summary>
        /// <value>The credentials identifier.</value>
        public Guid CredentialsId { get; }
    }
}
