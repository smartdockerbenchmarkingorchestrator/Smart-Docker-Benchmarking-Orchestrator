using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class EditBenchmarkExperimentValidator : AbstractValidator<EditBenchmarkExperimentViewModel>
    {
        public EditBenchmarkExperimentValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.Host).NotNull().NotEmpty().NotEqual(Guid.Empty).NotEqual(c => c.BenchmarkHost).When(c => c.Completed == false);
            RuleFor(c => c.Application).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.BenchmarkHost).NotNull().NotEmpty().NotEqual(Guid.Empty).NotEqual(c => c.Host).When(c => c.Completed == false);

            RuleFor(c => c.BenchmarkTimeLength).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(60000).LessThanOrEqualTo(6000000);

            RuleFor(c => c.ConcurrentUsers).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(experiment => experiment.ApdexTSeconds).NotNull()
                .GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);

            RuleFor(c => c.TypeOfTest).NotNull().NotEmpty();
        }
    }
}
