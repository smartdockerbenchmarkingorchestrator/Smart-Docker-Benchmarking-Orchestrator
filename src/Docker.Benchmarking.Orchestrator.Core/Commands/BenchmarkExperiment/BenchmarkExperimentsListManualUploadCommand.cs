// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="BenchmarkExperimentsListManualUploadCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class BenchmarkExperimentsListManualUploadCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{Docker.Benchmarking.Orchestrator.Core.Entities.BenchmarkExperiment}}" />
    public class BenchmarkExperimentsListManualUploadCommand : IRequest<IEnumerable<Entities.BenchmarkExperiment>>
    {

    }
}
