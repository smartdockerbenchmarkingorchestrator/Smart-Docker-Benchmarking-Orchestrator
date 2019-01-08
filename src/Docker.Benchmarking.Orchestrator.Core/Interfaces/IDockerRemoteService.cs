using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IDockerRemoteService
    {
        Task<IList<ContainerListResponse>> ViewRunningContainers(DockerHost host);

        Task<bool> PingDockerHost(string hostName, int portNumber);
        Task<bool> PingDockerHost(string hostName, int portNumber, string userName, string password);
        Task<bool> DeployImageToHost(string hostName, int portNumber, Guid dockerImage, string userName = null, string password = null);
        Task<DockerHost> AddDockerHostAsync(DockerHost entity);
        Task UpdateDockerHostAsync(DockerHost entity);

    }
}
