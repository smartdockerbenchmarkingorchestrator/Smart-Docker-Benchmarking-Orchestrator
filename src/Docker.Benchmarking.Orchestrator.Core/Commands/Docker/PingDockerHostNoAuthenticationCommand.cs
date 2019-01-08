// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="PingDockerHostNoAuthenticationCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class PingDockerHostNoAuthenticationCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class PingDockerHostNoAuthenticationCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PingDockerHostNoAuthenticationCommand"/> class.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="portNumber">The port number.</param>
        public PingDockerHostNoAuthenticationCommand(string hostName, int portNumber)
        {
            HostName = hostName;
            PortNumber = portNumber;
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName { get; }
        /// <summary>
        /// Gets the port number.
        /// </summary>
        /// <value>The port number.</value>
        public int PortNumber { get; }
    }
}
