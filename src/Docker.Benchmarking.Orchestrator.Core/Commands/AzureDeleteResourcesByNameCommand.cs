// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-30-2018
// ***********************************************************************
// <copyright file="AzureDeleteResourcesByNameCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureDeleteResourcesByNameCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class AzureDeleteResourcesByNameCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDeleteResourcesByNameCommand"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        public AzureDeleteResourcesByNameCommand(string resourceName, Guid credentialsId)
        {
            ResourceName = resourceName;
            CredentialsId = credentialsId;
        }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string ResourceName { get; }
        /// <summary>
        /// Gets the credentials identifier.
        /// </summary>
        /// <value>The credentials identifier.</value>
        public Guid CredentialsId { get; }
    }
}
