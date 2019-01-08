using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IAWSHostService
    {
        Task<bool> DeleteResourcesAfterExperiment(Guid hostId);

        Task<bool> CreateVMForBenchmark(Guid benchmarkExperimentId);
    }
}
