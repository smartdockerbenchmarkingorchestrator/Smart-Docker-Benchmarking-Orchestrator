using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class OptimisedBenchmarkExperimentRequestViewModelValidator : AbstractValidator<OptimisedBenchmarkExperimentRequestViewModel>
    {
        public OptimisedBenchmarkExperimentRequestViewModelValidator()
        {
            RuleFor(c => c.ApacheTestFileId).NotNull().NotEqual(Guid.Empty);
            RuleFor(c => c.Application).NotNull().NotEqual(Guid.Empty);

            RuleFor(experiment => experiment.Name).NotNull();
            RuleFor(experiment => experiment.HostCloudProvider).NotNull().NotEqual(c => c.BenchmarkCloudProvier);

            RuleFor(experiment => experiment.Application).NotNull().NotEqual(Guid.Empty);
            RuleFor(experiment => experiment.BenchmarkCloudProvier).NotNull()
                .NotEqual(c => c.HostCloudProvider);

            RuleFor(experiment => experiment.BenchmarkTimeLength).NotNull()
                .GreaterThanOrEqualTo(60000).LessThanOrEqualTo(6000000);
            RuleFor(experiment => experiment.ConcurrentUsers).NotNull()
                .GreaterThanOrEqualTo(1);
            RuleFor(experiment => experiment.ApdexTSeconds).NotNull()
                .GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);
        }
    }
}
