using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class BenchmarkExperimentService : IBenchmarkExperimentService
    {
        private readonly IRepository<BenchmarkExperiment> _service;
        public BenchmarkExperimentService(IRepository<BenchmarkExperiment> service)
        {
            _service = service;
        }
        public Task<IEnumerable<BenchmarkExperiment>> ExperimentsRequiringManualUpload()
        {
            return Task.FromResult(_service.FindBy(c => c.Started && !c.Completed && !c.JmeterResultsProcessed).AsEnumerable());
        }

        public async Task<bool> RecalculateMetricsForExperiment(Guid id)
        {
            Guard.Against.GuidNullOrDefault(id, nameof(id));
            var entity = await _service.GetByIdAsync(id);
            Guard.Against.Null(entity, nameof(entity));

            entity.BenchmarkStatsCalculation(entity.TestResults.ToList());
            entity.CreateBenchmarkStats(entity.ContainerMetrics.ToList());

            await _service.UpdateAsync(entity);

            return true;
        }
    }
}
