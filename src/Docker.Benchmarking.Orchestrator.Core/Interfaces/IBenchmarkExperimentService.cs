using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IBenchmarkExperimentService
    {
        Task<IEnumerable<BenchmarkExperiment>> ExperimentsRequiringManualUpload();

        Task<bool> RecalculateMetricsForExperiment(Guid id);

    }
}
