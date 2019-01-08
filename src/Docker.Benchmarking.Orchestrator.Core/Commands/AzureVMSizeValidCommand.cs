// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-28-2018
// ***********************************************************************
// <copyright file="AzureVMSizeValidCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AzureVMSizeValidCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class AzureVMSizeValidCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureVMSizeValidCommand"/> class.
        /// </summary>
        /// <param name="vmSize">Size of the vm.</param>
        /// <param name="id">The identifier.</param>
        public AzureVMSizeValidCommand(string vmSize, Guid id = default(Guid))
        {
            VMSize = vmSize;
            Id = id;
        }
        /// <summary>
        /// Gets the size of the vm.
        /// </summary>
        /// <value>The size of the vm.</value>
        public string VMSize { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }
    }
}
