using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Optimizer;
using Docker.Benchmarking.Orchestrator.Optimizer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands
{
    public class SingleCloudOptimizedCommandHandler : IRequestHandler<SingleCloudOptimizedCommand, IEnumerable<OptimisedResult>>
    {
        private readonly IOptimizer _optimizer;
        private readonly IMapper _mapper;

        private readonly IRepository<AWSCloudFormationTemplate> _awsTemplate;
        private readonly IRepository<AzureVMTemplate> _azureTemplate;

        public SingleCloudOptimizedCommandHandler(IOptimizer optimizer, 
            IMapper mapper, 
            IRepository<AWSCloudFormationTemplate> awsTemplate,
            IRepository<AzureVMTemplate> azureTemplate)
        {
            _optimizer = optimizer;
            _mapper = mapper;
            _awsTemplate = awsTemplate;
            _azureTemplate = azureTemplate;
        }
        public Task<IEnumerable<OptimisedResult>> Handle(SingleCloudOptimizedCommand request, CancellationToken cancellationToken)
        {
            var benchmarkVMSize = SetBenchmarkVMSize(request.ConcurrentUsers);

            CloudServiceProvider benchmarkProvider;

            if(request.BenchmarkCloudProvier == CloudProvider.AWS)
            {
                var vm = _awsTemplate.FindBy(c => c.VMSizeType == benchmarkVMSize && c.Active).OrderBy(c=> c.PricePerHour).FirstOrDefault();

                if (vm == null)
                    throw new Exception("No VM found for AWS");

                benchmarkProvider = new CloudServiceProvider
                {
                    CloudTemplateId = vm.Id,
                    CloudProvider = Optimizer.Enums.CloudProvider.AWS,
                    Name = vm.Name,
                    Time = request.ExperimentLengthInSeconds,
                    CostPerHour = vm.PricePerHour,
                    VMSize = (Optimizer.Enums.VMSize)benchmarkVMSize
                };

            }
            else 
            //if (request.BenchmarkProvider == CloudProvider.Azure)
            {
                var vm = _azureTemplate.FindBy(c => c.VMSizeType == benchmarkVMSize && c.Active).OrderBy(c => c.PricePerHour).FirstOrDefault();

                if (vm == null)
                    throw new Exception("No VM found for Azure");

                benchmarkProvider = new CloudServiceProvider
                {
                    CloudTemplateId = vm.Id,
                    CloudProvider = Optimizer.Enums.CloudProvider.Azure,
                    Name = vm.Name,
                    Time = request.ExperimentLengthInSeconds,
                    CostPerHour = vm.PricePerHour,
                    VMSize = (Optimizer.Enums.VMSize)benchmarkVMSize
                };
            }

            List<CloudServiceProvider> vmHosts = new List<CloudServiceProvider>();

            if(request.HostCloudProvider == CloudProvider.AWS)
            {
                var vms = _awsTemplate.FindBy(c => c.Active).OrderBy(c => c.PricePerHour);

                if (vms == null)
                    throw new Exception("No VMs found for AWS");

                foreach(var vm in vms)
                {
                    vmHosts.Add(new CloudServiceProvider
                    {
                        CloudTemplateId = vm.Id,
                        CloudProvider = Optimizer.Enums.CloudProvider.AWS,
                        CostPerHour = vm.PricePerHour,
                        Name = vm.Name,
                        Time = request.ExperimentLengthInSeconds,
                        VMSize = (Optimizer.Enums.VMSize)vm.VMSizeType
                    });
                }
            }

            if (request.HostCloudProvider == CloudProvider.Azure)
            {
                var vms = _azureTemplate.FindBy(c => c.Active).OrderBy(c => c.PricePerHour);

                if (vms == null)
                    throw new Exception("No VMs found for Azure");

                foreach (var vm in vms)
                {
                    vmHosts.Add(new CloudServiceProvider
                    {
                        CloudTemplateId = vm.Id,
                        CloudProvider = Optimizer.Enums.CloudProvider.Azure,
                        CostPerHour = vm.PricePerHour,
                        Name = vm.Name,
                        Time = request.ExperimentLengthInSeconds,
                        VMSize = (Optimizer.Enums.VMSize)vm.VMSizeType
                    });
                }
            }

            decimal estimateCost = 0;
            var optimizedList = _optimizer.SingleProvider(vmHosts, benchmarkProvider, request.MaxCost, out estimateCost);

            return Task.FromResult<IEnumerable<OptimisedResult>>(optimizedList);
        }

        private VMSize SetBenchmarkVMSize(int users)
        {
            if (users <= 10)
                return VMSize.Small;

            if (users <= 100)
                return VMSize.Medium;

            return VMSize.Large;
        }
    }
}
