// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 09-07-2018
// ***********************************************************************
// <copyright file="AWSValidateTemplateCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AWSValidateTemplateCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.List{System.String}}" />
    public class AWSValidateTemplateCommand : IRequest<List<string>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSValidateTemplateCommand"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="credentials">The credentials.</param>
        public AWSValidateTemplateCommand(string template, Core.Entities.AWSCredentials credentials)
        {
            Template = template;
            Credentials = credentials;
        }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public Core.Entities.AWSCredentials Credentials { get; }

    }
}
