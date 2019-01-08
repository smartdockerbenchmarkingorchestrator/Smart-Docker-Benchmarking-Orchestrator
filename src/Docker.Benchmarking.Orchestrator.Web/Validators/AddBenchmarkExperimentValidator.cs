using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class AddBenchmarkExperimentValidator : AbstractValidator<AddBenchmarkExperimentViewModel>
    {
        public AddBenchmarkExperimentValidator()
        {
            RuleFor(experiment => experiment.Name).NotNull();
            RuleFor(experiment => experiment.Host).NotNull().NotEqual(Guid.Empty).NotEqual(c => c.BenchmarkHost);

            RuleFor(experiment => experiment.Application).NotNull().NotEqual(Guid.Empty);
            RuleFor(experiment => experiment.BenchmarkHost).NotNull()
                .NotEqual(Guid.Empty)
                .NotEqual(c => c.Host);

            RuleFor(experiment => experiment.BenchmarkTimeLength).NotNull()
                .GreaterThanOrEqualTo(60000).LessThanOrEqualTo(6000000);
            RuleFor(experiment => experiment.ConcurrentUsers).NotNull()
                .GreaterThanOrEqualTo(1);
            RuleFor(experiment => experiment.ApdexTSeconds).NotNull()
                .GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);

            RuleFor(experiment => experiment.TypeOfTest).NotNull();
        }
    }
}
