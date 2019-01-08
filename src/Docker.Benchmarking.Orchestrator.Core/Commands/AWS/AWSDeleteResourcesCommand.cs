// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-31-2018
//
// Last Modified By : ***********
// Last Modified On : 08-31-2018
// ***********************************************************************
// <copyright file="AWSDeleteResourcesCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.AWS
{
    /// <summary>
    /// Class AWSDeleteResourcesCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class AWSDeleteResourcesCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSDeleteResourcesCommand"/> class.
        /// </summary>
        /// <param name="stackName">Name of the stack.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        public AWSDeleteResourcesCommand(string stackName, Guid credentialsId)
        {
            StackName = stackName;
            CredentialsId = credentialsId;
        }

        /// <summary>
        /// Gets the name of the stack.
        /// </summary>
        /// <value>The name of the stack.</value>
        public string StackName { get; }
        /// <summary>
        /// Gets the credentials identifier.
        /// </summary>
        /// <value>The credentials identifier.</value>
        public Guid CredentialsId { get; }
    }
}
