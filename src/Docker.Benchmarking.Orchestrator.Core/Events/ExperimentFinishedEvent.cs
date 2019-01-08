using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Events
{
    public class ExperimentFinishedEvent : BaseDomainEvent
    {
        public ExperimentFinishedEvent(BenchmarkExperiment experiment)
        {
            Experiment = experiment;
        }
        public BenchmarkExperiment Experiment { get; }
    }
}
