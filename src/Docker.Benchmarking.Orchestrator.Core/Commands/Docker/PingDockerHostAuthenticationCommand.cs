// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-07-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="PingDockerHostAuthenticationCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class PingDockerHostAuthenticationCommand.
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class PingDockerHostAuthenticationCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PingDockerHostAuthenticationCommand"/> class.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="portNumber">The port number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public PingDockerHostAuthenticationCommand(string hostName, int portNumber, string userName, string password)
        {
            HostName = hostName;
            PortNumber = portNumber;
            UserName = userName;
            Password = password;
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
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; }
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; }
    }
}
