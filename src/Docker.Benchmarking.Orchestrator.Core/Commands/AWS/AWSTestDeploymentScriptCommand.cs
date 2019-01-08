// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-30-2018
//
// Last Modified By : ***********
// Last Modified On : 08-30-2018
// ***********************************************************************
// <copyright file="AWSTestDeploymentScriptCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.CloudFormation.Model;
using MediatR;
using System;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.AWS
{
    /// <summary>
    /// Class AWSTestDeploymentScriptCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.String}" />
    public class AWSTestDeploymentScriptCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSTestDeploymentScriptCommand"/> class.
        /// </summary>
        /// <param name="stackName">Name of the stack.</param>
        /// <param name="json">The json.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        /// <param name="dockerImageId">The docker image identifier.</param>
        public AWSTestDeploymentScriptCommand(string stackName, string json, List<Parameter> parameters, Guid credentialsId, Guid dockerImageId = default(Guid))
        {
            StackName = stackName;
            Json = json;
            Parameters = parameters;
            CredentialsId = credentialsId;
            DockerImageId = dockerImageId;
        }

        /// <summary>
        /// Gets the name of the stack.
        /// </summary>
        /// <value>The name of the stack.</value>
        public string StackName { get; }
        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <value>The json.</value>
        public string Json { get; }
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<Parameter> Parameters { get; }
        /// <summary>
        /// Gets the credentials identifier.
        /// </summary>
        /// <value>The credentials identifier.</value>
        public Guid CredentialsId { get; }
        /// <summary>
        /// Gets the docker image identifier.
        /// </summary>
        /// <value>The docker image identifier.</value>
        public Guid DockerImageId { get; }
    }
}
