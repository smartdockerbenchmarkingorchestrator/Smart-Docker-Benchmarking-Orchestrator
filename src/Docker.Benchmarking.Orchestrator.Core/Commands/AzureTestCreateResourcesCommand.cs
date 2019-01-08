// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-31-2018
// ***********************************************************************
// <copyright file="AzureTestCreateResourcesCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureTestCreateResourcesCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.String}" />
    public class AzureTestCreateResourcesCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTestCreateResourcesCommand"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="region">The region.</param>
        /// <param name="deploymentJson">The deployment json.</param>
        /// <param name="deploymentParameters">The deployment parameters.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        /// <param name="dockerImageId">The docker image identifier.</param>
        public AzureTestCreateResourcesCommand(string resourceName, string region, string deploymentJson, string deploymentParameters, Guid credentialsId, Guid dockerImageId = default(Guid))
        {
            ResourceName = resourceName;
            Region = region;
            DeploymentJson = deploymentJson;
            DeploymentParameters = deploymentParameters;
            CredentialsId = credentialsId;
            DockerImageId = dockerImageId;
        }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string ResourceName { get; }

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <value>The region.</value>
        public string Region { get; }

        /// <summary>
        /// Gets the deployment json.
        /// </summary>
        /// <value>The deployment json.</value>
        public string DeploymentJson { get; }

        /// <summary>
        /// Gets the deployment parameters.
        /// </summary>
        /// <value>The deployment parameters.</value>
        public string DeploymentParameters { get; }

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
