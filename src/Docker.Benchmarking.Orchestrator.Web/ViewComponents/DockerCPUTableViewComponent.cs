using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.APIModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class DockerCPUTableViewComponent : ViewComponent
    {
        private readonly IRepository<ContainerMetric> _containerMetricRepo;
        private readonly IMapper _mapper;
        public DockerCPUTableViewComponent(IRepository<ContainerMetric> containerMetricRepo, IMapper mapper)
        {
            _containerMetricRepo = containerMetricRepo;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(
        Guid applicationTestId)
        {
            var items = _containerMetricRepo.FindBy(c => c.BenchmarkExperiment.Id == applicationTestId);

            if (items.Count() == 0)
                return null;

            // ReSharper disable once PossibleNullReferenceException
            var application = items.FirstOrDefault(c => c.BenchmarkExperiment != null).BenchmarkExperiment;

            var containerTestStartTime = application.StartedAt.ToUniversalTime();

            var benchmarkEndTime = application.CompletedAt.ToUniversalTime();

            var containerMetrics = application.ContainerMetrics;

            var cpuMetrics = containerMetrics.Where(c => (c.DateTimeCreated.ToUniversalTime() >= containerTestStartTime && c.DateTimeCreated.ToUniversalTime() <= benchmarkEndTime)).ToList();

            var apiModel = _mapper.Map<IEnumerable<DockerStatsApiModel>>(cpuMetrics).OrderBy(c => c.DateTimeUtc);
            apiModel.OrderBy(c => c.DateTimeUtc);

            return View(apiModel);
        }
    }
}
