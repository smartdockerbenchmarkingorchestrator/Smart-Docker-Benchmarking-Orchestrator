using Ardalis.GuardClauses;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class AWSHostService : IAWSHostService
    {
        private readonly IAWSResourcesService _awsResourceService;
        private readonly IRepository<AWSHost> _awsHostRepo;
        private readonly IRepository<BenchmarkExperiment> _benchmarkRepo;
        private readonly IRepository<DockerHost> _dockerHostRepo;
        private readonly IMapper _mapper;

        public AWSHostService(IAWSResourcesService awsResourceService,
            IRepository<AWSHost> awsHostRepo,
            IRepository<BenchmarkExperiment> benchmarkRepo, IMapper mapper, IRepository<DockerHost> dockerHostRepo)
        {
            _awsResourceService = awsResourceService;
            _awsHostRepo = awsHostRepo;
            _benchmarkRepo = benchmarkRepo;
            _mapper = mapper;
            _dockerHostRepo = dockerHostRepo;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<bool> CreateVMForBenchmark(Guid benchmarkExperimentId)
        {
            var benchmark = await _benchmarkRepo.GetByIdAsync(benchmarkExperimentId);
            Guard.Against.Null(benchmark, nameof(benchmark));

            if (benchmark.Host.AWSHost != null)
            {
                var awsHost = benchmark.Host.AWSHost;

                var ipAddress = await SetupHost(awsHost);

                benchmark.Host.HostName = ipAddress;
                benchmark.Host.PortNumber = 2376;

                await _dockerHostRepo.UpdateAsync(benchmark.Host);
            }

            if (benchmark.BenchmarkHost.AWSHost != null)
            {
                var awsHost = benchmark.BenchmarkHost.AWSHost;

                var ipAddress = await SetupHost(awsHost);

                benchmark.Host.HostName = ipAddress;
                benchmark.Host.PortNumber = 2376;

                await _dockerHostRepo.UpdateAsync(benchmark.Host);
            }

            if (benchmark.DatabaseHost != null)
            {
                if (benchmark.DatabaseHost.AWSHost != null)
                {
                    var awsHost = benchmark.DatabaseHost.AWSHost;

                    var ipAddress = await SetupHost(awsHost);

                    benchmark.Host.HostName = ipAddress;
                    benchmark.Host.PortNumber = 2376;

                    await _dockerHostRepo.UpdateAsync(benchmark.Host);
                }
            }

            return true;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<bool> DeleteResourcesAfterExperiment(Guid hostId)
        {
            Guard.Against.GuidNullOrDefault(hostId, nameof(hostId));

            var host = await _awsHostRepo.GetByIdAsync(hostId);

            Guard.Against.Null(host, nameof(host));

            host.ResourceDestroyedAt = DateTimeOffset.UtcNow;
            host.ResourceDestroyed = false;

            await _awsHostRepo.UpdateAsync(host);

            return await _awsResourceService.DeleteStack(host.DockerHost.StackResourceName, host.Credentials.Id);
        }

        private async Task<string> SetupHost(AWSHost host)
        {
            Guard.Against.Null(host, nameof(host));

            //Flag for Application Layer
            host.ResourceCreationStarted = true;
            await _awsHostRepo.UpdateAsync(host);

            try
            {
                var stackDeleteRequest = await _awsResourceService.DeleteStack(host.DockerHost.StackResourceName, host.Credentials.Id);
            }
            catch (Exception ex)
            {
                //
            }

            await Task.Delay(120000);

            var parameters = new List<Amazon.CloudFormation.Model.Parameter>();
            _mapper.Map(host.Template.Parameters, parameters);

            var ipAddress = await _awsResourceService.CreateDockerVM(host.DockerHost.StackResourceName, host.Template.Template, parameters, host.Credentials.Id);

            host.IPAddress = ipAddress;
            host.ResourceCreatedAt = DateTimeOffset.Now;
            host.ResourcedCreated = true;

            await _awsHostRepo.UpdateAsync(host);

            return ipAddress;
        }
    }
}
