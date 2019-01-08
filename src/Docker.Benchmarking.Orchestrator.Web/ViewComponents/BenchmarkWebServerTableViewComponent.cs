using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class BenchmarkWebServerTableViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BenchmarkExperiment> _benchmarkRepo;

        public BenchmarkWebServerTableViewComponent(IMapper mapper, IRepository<BenchmarkExperiment> benchmarkRepo)
        {
            _mapper = mapper;
            _benchmarkRepo = benchmarkRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        Guid applicationTestId)
        {
            var application = await _benchmarkRepo.GetByIdAsync(applicationTestId);

            if (application == null) throw new Exception();

            var benchmarkStartTime = application.StartedAt.UtcDateTime;

            var benchmarkEndTime = application.CompletedAt.UtcDateTime;

            var cpuMetrics = application.TestResults;

            var apiModel = _mapper.Map<IEnumerable<BenchmarkTestItemViewModel>>(cpuMetrics).OrderBy(c => c.Timestamp);

            return View(apiModel);
        }
    }
}
