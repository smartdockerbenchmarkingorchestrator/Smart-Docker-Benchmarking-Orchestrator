// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="BenchmarkExperimentResultsUploadCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class BenchmarkExperimentResultsUploadCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.String}" />
    public class BenchmarkExperimentResultsUploadCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkExperimentResultsUploadCommand"/> class.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        public BenchmarkExperimentResultsUploadCommand(IFormFile formFile)
        {
            FormFile = formFile;
        }

        /// <summary>
        /// Gets the form file.
        /// </summary>
        /// <value>The form file.</value>
        public IFormFile FormFile { get; }
    }
}
