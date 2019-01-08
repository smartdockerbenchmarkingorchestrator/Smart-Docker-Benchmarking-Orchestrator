using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Handlers
{
    public class BenchmarkExperimentStatusHandler : IRequestHandler<BenchmarkExperimentStatusCommand, ExperimentStatus>
    {
        private readonly IRepository<BenchmarkExperiment> _experimentRepo;
        public BenchmarkExperimentStatusHandler(IRepository<BenchmarkExperiment> experimentRepo)
        {
            _experimentRepo = experimentRepo;
        }

        public async Task<ExperimentStatus> Handle(BenchmarkExperimentStatusCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

           var experiment = await _experimentRepo.GetByIdAsync(request.Id);

            if (experiment.Started) return ExperimentStatus.Started;

            if (experiment.Completed) return ExperimentStatus.Completed;

            return ExperimentStatus.Ready;
        }
    }
}
