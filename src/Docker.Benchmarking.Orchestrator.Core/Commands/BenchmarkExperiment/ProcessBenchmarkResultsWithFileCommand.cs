// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-31-2018
// ***********************************************************************
// <copyright file="ProcessBenchmarkResultsWithFileCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class ProcessBenchmarkResultsWithFileCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class ProcessBenchmarkResultsWithFileCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessBenchmarkResultsWithFileCommand"/> class.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <param name="experimentId">The experiment identifier.</param>
        public ProcessBenchmarkResultsWithFileCommand(IFormFile formFile, Guid experimentId)
        {
            FormFile = formFile;
            Experiment = experimentId;
        }

        /// <summary>
        /// Gets the form file.
        /// </summary>
        /// <value>The form file.</value>
        public IFormFile FormFile { get; }
        /// <summary>
        /// Gets the experiment.
        /// </summary>
        /// <value>The experiment.</value>
        public Guid Experiment { get; }
    }
}
