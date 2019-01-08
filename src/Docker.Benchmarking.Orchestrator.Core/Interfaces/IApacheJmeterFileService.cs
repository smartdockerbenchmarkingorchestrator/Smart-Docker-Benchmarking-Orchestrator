using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IApacheJmeterFileService
    {
        Task<string> JmeterTestFileForBenchmark(Guid benchmarkId);
    }
}
