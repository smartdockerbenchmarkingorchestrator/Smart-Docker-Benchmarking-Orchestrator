using Docker.Benchmarking.Orchestrator.Core.Events;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Handlers
{
    public class ExperimentFinishedNotificationHandler : IHandle<ExperimentFinishedEvent>
    {
        private readonly IBenchmarkOrchestratorService _benchmarkService;
        public ExperimentFinishedNotificationHandler(IBenchmarkOrchestratorService benchmarkService)
        {
            _benchmarkService = benchmarkService;
        }

        public async Task Handle(ExperimentFinishedEvent domainEvent)
        {
            await _benchmarkService.EndBenchmarkExperiment(domainEvent.Experiment);
        }
    }
}
