using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum ExperimentStatus
    {
        Created = 1,
        CloudResourcesToCreate,
        Ready,
        Started,
        BenchmarkingCompleted,
        Completed
    }
}
