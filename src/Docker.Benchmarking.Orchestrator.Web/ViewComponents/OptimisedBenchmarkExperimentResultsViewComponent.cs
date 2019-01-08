using Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Optimizer.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class OptimisedBenchmarkExperimentResultsViewComponent : ViewComponent
    {
        private readonly IMediator _mediatr;
        public OptimisedBenchmarkExperimentResultsViewComponent(IMediator mediatr)
        {
            _mediatr = mediatr;
        }


        public async Task<IViewComponentResult> InvokeAsync(
            CloudProvider hostProvider, 
            CloudProvider benchmarkProvider,
            int experimentLength,
            int concurrentUsers,
            decimal maxCost
            )
        {
            var command = new SingleCloudOptimizedCommand
            {
                BenchmarkCloudProvier = benchmarkProvider,
                HostCloudProvider = hostProvider,
                ConcurrentUsers = concurrentUsers,
                BenchmarkTimeLength = experimentLength,
                MaxCost = maxCost
            };

            var result = await _mediatr.Send(command);

            return View(result);
        }

    }
}
