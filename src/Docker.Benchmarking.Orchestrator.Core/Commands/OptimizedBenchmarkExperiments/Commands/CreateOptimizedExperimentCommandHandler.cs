using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands
{
    public class CreateOptimizedExperimentCommandHandler : IRequestHandler<CreateOptimizedExperimentCommand, string>
    {
        private IRepository<DockerHost> _dockerHostRepo;
        private IRepository<Entities.BenchmarkExperiment> _benchmarkRepo;
        private IRepository<Entities.AWSCredentials> _awsCredsRepo;
        private IRepository<Entities.AzureCredientials> _azureCredsRepo;
        private IRepository<Entities.AzureVMTemplate> _azureVMRepo;
        private IRepository<Entities.AWSCloudFormationTemplate> _awsVMRepo;
        private IRepository<Entities.Application> _applicationRepo;
        private IRepository<Entities.ApacheJmeterTestFile> _apacheTestFileRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatr;
        public CreateOptimizedExperimentCommandHandler(
            IMediator mediatr,
            IRepository<DockerHost> dockerHostRepo,
            IRepository<Entities.BenchmarkExperiment> benchmarkRepo,
            IRepository<Entities.AWSCredentials> awsCredsRepo,
            IRepository<Entities.AzureCredientials> azureCredsRepo,
            IRepository<Entities.AzureVMTemplate> azureVMRepo,
            IRepository<Entities.AWSCloudFormationTemplate> awsVMRepo,
            IRepository<Entities.Application> applicationRepo,
            IRepository<Entities.ApacheJmeterTestFile> apacheTestFileRepo,
            IMapper mapper
            )
        {
            _mediatr = mediatr;
            _dockerHostRepo = dockerHostRepo;
            _benchmarkRepo = benchmarkRepo;
            _awsCredsRepo = awsCredsRepo;
            _azureCredsRepo = azureCredsRepo;
            _azureVMRepo = azureVMRepo;
            _awsVMRepo = awsVMRepo;
            _applicationRepo = applicationRepo;
            _apacheTestFileRepo = apacheTestFileRepo;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateOptimizedExperimentCommand request, CancellationToken cancellationToken)
        {
            // Use Optimizer

            var command = new SingleCloudOptimizedCommand
            {
                BenchmarkCloudProvier = request.BenchmarkCloudProvier,
                HostCloudProvider = request.HostCloudProvider,
                BenchmarkTimeLength = request.BenchmarkTimeLength,
                ConcurrentUsers = request.ConcurrentUsers,
                MaxCost = request.MaxCost
            };

            var optimizedResults = await _mediatr.Send(command);

            //loop through each experiment
            var experiments = new List<Core.Entities.BenchmarkExperiment>();
            foreach (var result in optimizedResults)
            {
                //create host vm

                var host = result.HostVM.CloudProvider == Optimizer.Enums.CloudProvider.AWS ? await CreateAWSHost(request, result.HostVM.CloudTemplateId) : await CreateAzureHost(request, result.HostVM.CloudTemplateId);

                var benchmarkHost = result.BenchmarkVM.CloudProvider == Optimizer.Enums.CloudProvider.AWS ? await CreateAWSHost(request, result.BenchmarkVM.CloudTemplateId) : await CreateAzureHost(request, result.BenchmarkVM.CloudTemplateId);

                var experiment = await CreateExperiment(host, benchmarkHost, request);

                experiments.Add(experiment);
                //create benchmark vm
            }
            //Create Experiments

            return await Task.FromResult(string.Empty);
        }

        private async Task<DockerHost> CreateAWSHost(CreateOptimizedExperimentCommand request, Guid templateId)
        {
            var template = await _awsVMRepo.GetByIdAsync(templateId);

            var dockerHost = new DockerHost
            {
                Name = request.Name,
                Location = "Ireland",
                CloudProvider = Enums.CloudProvider.AWS,
                HostName = "localhost",
                vCPU = template.vCPUs,
                Memory = template.Memory,
                PortNumber = 2376,
                Storage = template.DiskSize
            };

            var awsHost = new AWSHost
            {
                DockerHost = dockerHost,
                Credentials = _awsCredsRepo.GetAll().FirstOrDefault(),
                DestroyResourcesAfterBenchmark = true,
                IPAddress = "127.0.0.1",
                Template = template,
                Name = "AWSHost" + request.Name.Replace(" ", "", StringComparison.CurrentCulture) + DateTime.Now.Ticks
            };

            dockerHost.AWSHost = awsHost;

            await _dockerHostRepo.AddAsync(dockerHost);

            return dockerHost;
        }

        private async Task<DockerHost> CreateAzureHost(CreateOptimizedExperimentCommand request, Guid templateId)
        {
            var template = await _azureVMRepo.GetByIdAsync(templateId);

            var dockerHost = new DockerHost
            {
                Name = request.Name,
                Location = "Ireland",
                CloudProvider = Enums.CloudProvider.Azure,
                HostName = "localhost",
                vCPU = template.vCPUs,
                Memory = template.Memory,
                PortNumber = 2376,
                Storage = template.DiskSize
            };

            var azureRegion = Region.Values.Where(c => c.Name.ToLower() == "westeurope".ToLower()).FirstOrDefault();

            var creds = _azureCredsRepo.GetAll().FirstOrDefault();

            var azureHost = new AzureHost
            {
                DockerHost = dockerHost,
                AzureRegion = azureRegion.ToString(),
                DestroyResourcesAfterBenchmark = true,
                Credentials = creds,
                IPAddress = "127.0.0.1",
                Template = template,
                Name = "AzureHost" + request.Name.Replace(" ", "", StringComparison.CurrentCulture) + DateTime.Now.Ticks
            };

            dockerHost.AzureHost = azureHost;

            await _dockerHostRepo.AddAsync(dockerHost);

            return dockerHost;
        }

        private async Task<Entities.BenchmarkExperiment> CreateExperiment(DockerHost host, DockerHost benchmarkHost, CreateOptimizedExperimentCommand request)
        {
            var name = request.Name + DateTime.Now.Ticks;

            if(host.AWSHost != null)
            {
                request.Name += host.AWSHost.Template.VMSizeType;
            }
            else
            {
                request.Name += host.AzureHost.Template.VMSizeType;
            }

            var experiment = new Entities.BenchmarkExperiment
            {
                Name = name,
                ExperimentReference = request.ExperimentReference,
                BenchmarkingName = request.BenchmarkingName,
                Application = await _applicationRepo.GetByIdAsync(request.Application),
                ApdexTSeconds = request.ApdexTSeconds,
                ApacheJmeterTestId = request.ApacheTestFileId,
                TestFile = await _apacheTestFileRepo.GetByIdAsync(request.ApacheTestFileId),
                Variables = _mapper.Map<List<BenchmarkExperimentVariable>>(request.Variables),
                Host = host,
                BenchmarkHost = benchmarkHost
            };

            await _benchmarkRepo.AddAsync(experiment);

            return experiment;
        }
    }
}
