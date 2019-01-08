// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 08-29-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="AWSEstimatedCostsCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.AWS
{
    /// <summary>
    /// Class AWSEstimatedCostsCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.String}" />
    public class AWSEstimatedCostsCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSEstimatedCostsCommand"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="credentials">The credentials.</param>
        public AWSEstimatedCostsCommand(string template, List<Amazon.CloudFormation.Model.Parameter> parameters, Entities.AWSCredentials credentials)
        {
            Template = template;
            Parameters = parameters;
            Credentials = credentials;
        }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<Amazon.CloudFormation.Model.Parameter> Parameters { get; }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public Entities.AWSCredentials Credentials { get; }
    }
}
