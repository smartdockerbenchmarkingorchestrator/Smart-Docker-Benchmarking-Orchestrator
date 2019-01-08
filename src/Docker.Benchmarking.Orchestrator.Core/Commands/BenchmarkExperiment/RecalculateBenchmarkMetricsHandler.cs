using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands.BenchmarkExperiment
{
    public class RecalculateBenchmarkMetricsHandler : IRequestHandler<RecalculateBenchmarkMetrics, bool>
    {
        private readonly IBenchmarkExperimentService _service;
        public RecalculateBenchmarkMetricsHandler(IBenchmarkExperimentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(RecalculateBenchmarkMetrics request, CancellationToken cancellationToken)
        {
            return await _service.RecalculateMetricsForExperiment(request.Id);
        }
    }
}
