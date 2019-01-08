using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.DotNet;
using Docker.DotNet.BasicAuth;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class DockerRemoteService : IDockerRemoteService, IDisposable
    {
        private DockerClient _dockerClient;
        private CancellationTokenSource _cts;
        private readonly ILogger _logger;
        private readonly IRepository<DockerImage> _dockerImageService;
        private readonly IRepository<DockerHost> _dockerHostRepo;

        public DockerRemoteService(ILogger<DockerRemoteService> logger, IRepository<DockerImage> dockerImageService, IRepository<DockerHost> dockerHostRepo)
        {
            //_containerRepo = containerRepo;
            _logger = logger;
            _dockerImageService = dockerImageService;
            _dockerHostRepo = dockerHostRepo;
        }

        #region public

        public async Task<IList<ContainerListResponse>> ViewRunningContainers(DockerHost host)
        {
            Guard.Against.Null(host, nameof(host));

            return await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = false }, CancellationToken.None);
        }

        public async Task<bool> PingDockerHost(string hostname, int port)
        {
            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            CreateClientNoAuthentication(hostname, port);

            var versionResult = await _dockerClient.System.GetVersionAsync();

            return true;
        }

        public async Task<bool> PingDockerHost(string hostname, int port, string userId, string password)
        {
            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            Guard.Against.NullOrEmpty(password, nameof(password));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            CreateBasicHttpClient(hostname, port, userId, password);

            try
            {
                var versionResult = await _dockerClient.System.GetVersionAsync();

                return true;
            }
            catch (Exception ex)
            {
                //
                return false;
            }
        }

        public async Task<bool> DeployImageToHost(string hostName, int portNumber, Guid dockerImageId, string userName = null, string password = null)
        {
            Guard.Against.NullOrEmpty(hostName, nameof(hostName));
            Guard.Against.Null(portNumber, nameof(portNumber));
            Guard.Against.GuidNullOrDefault(dockerImageId, nameof(dockerImageId));

            var dockerImage = await _dockerImageService.GetByIdAsync(dockerImageId);

            if (dockerImage == null) Guard.Against.Null(dockerImage, nameof(dockerImage));

            if (userName == null || password == null)
                CreateClientNoAuthentication(hostName, portNumber);
            else
                CreateBasicHttpClient(hostName, portNumber, userName, password);

            await PullImageToHost(dockerImage);

            var containerId = await StartContainerAsync(dockerImage);

            if (containerId == null) throw new NullReferenceException("Container Id");

            return true;
        }

        public async Task<DockerHost> AddDockerHostAsync(DockerHost entity)
        {
            if (entity.AzureHost != null || entity.AWSHost != null)
            {
                return await _dockerHostRepo.AddAsync(entity);
            }

            bool pingDockerHost;

            if (entity.UseHttpAuthentication)
            {
                pingDockerHost = await PingDockerHost(entity.HostName, entity.PortNumber, entity.UserName, entity.Password);
            }
            else
            {
                pingDockerHost = await PingDockerHost(entity.HostName, entity.PortNumber);

            }

            if (pingDockerHost) return await _dockerHostRepo.AddAsync(entity);

            throw new Exception("Can't connect to Docker Host, check host details");
        }

        public async Task UpdateDockerHostAsync(DockerHost entity)
        {
            if (entity.Active == false)
            {
                await _dockerHostRepo.UpdateAsync(entity);
                return;
            }

            if (entity.AWSHost != null || entity.AWSHost != null)
            {
                await _dockerHostRepo.UpdateAsync(entity);
                return;
            }

            bool pingDockerHost;

            if (entity.UseHttpAuthentication)
            {
                pingDockerHost = await PingDockerHost(entity.HostName, entity.PortNumber, entity.UserName, entity.Password);
            }
            else
            {
                pingDockerHost = await PingDockerHost(entity.HostName, entity.PortNumber);
            }

            if (pingDockerHost)
                await _dockerHostRepo.UpdateAsync(entity);
            else
            {
                throw new Exception("Can't connect to Docker Host, check host details");
            }
        }

        #endregion

        #region private

        private async Task PullImageToHost(DockerImage image)
        {
            var parameters = new ImagesCreateParameters
            {
                FromImage = image.ImageName,
                Tag = image.ImageTag
            };

            var config = new AuthConfig();

            var progress = new Progress<JSONMessage>(stats => Console.WriteLine(stats.Status));

            await _dockerClient.Images.CreateImageAsync(parameters, config, progress, CancellationToken.None);
        }

        private async Task<string> StartContainerAsync(DockerImage image)
        {
            Guard.Against.Null(image, nameof(image));

            await RemoveContainersFromHost();

            await PullImageToHost(image);

            var portBindings = new Dictionary<string, IList<PortBinding>>();

            if (image.ImageType == ImageType.Application || image.ImageType == ImageType.Database)
            {
                if (image.ExternalPort == null)
                    throw new NullReferenceException("External Port must be exposed when launching web-application container.");

                if (image.InternalPort == null)
                    throw new NullReferenceException("Internal Port must be exposed when launching web-application container.");
            }

            var startParamters = new CreateContainerParameters()
            {
                Name = image.DockerFriendlyName,
                Image = image.FullImageTag,
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>()
                    {
                    {
                        image.InternalPort + "/tcp",
                        new PortBinding[]
                        {
                            new PortBinding
                            {
                                HostIP = "0.0.0.0",
                                HostPort = image.ExternalPort.ToString()
                            }
                        }
                    }
                }
                },
                ExposedPorts = new Dictionary<string, EmptyStruct>()
            };

            var environmentalVariablesList = new List<string>();

            foreach (var variable in image.Variables)
            {
                var variableString = variable.Name + "=" + variable.Value;
                environmentalVariablesList.Add(variableString);
            }

            startParamters.Env = environmentalVariablesList;
            startParamters.ExposedPorts.Add(image.InternalPort + "/tcp", default(EmptyStruct));

            var container = await _dockerClient.Containers.CreateContainerAsync(startParamters);

            bool task = await _dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters(), CancellationToken.None);

            if (task)
                return container.ID;

            throw new Exception("Error Starting Benchmark Container: " + container.ID);
        }

        private void CreateBasicHttpClient(string hostname, int port, string userId, string password)
        {
            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            Guard.Against.NullOrEmpty(password, nameof(password));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            _logger.LogInformation("Basic HTTP Authentication to DockerClient");

            var credentials = new BasicAuthCredentials(userId, password);
            var config = new DockerClientConfiguration(new Uri("tcp://" + hostname + ":" + port), credentials);
            _dockerClient = config.CreateClient();
        }

        private void CreateClientNoAuthentication(string hostname, int port)
        {
            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            _dockerClient = new DockerClientConfiguration(new Uri("tcp://" + hostname + ":" + port)).CreateClient();
        }

        private async Task RemoveContainersFromHost()
        {
            IList<ContainerListResponse> containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = true });

            foreach (var container in containers)
            {
                await _dockerClient.Containers.StopContainerAsync(container.ID, new ContainerStopParameters(), CancellationToken.None);
                await _dockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters(), CancellationToken.None);
            }
        }

        public void Dispose()
        {
            _dockerClient?.Dispose();
        }

        #endregion
    }
}
