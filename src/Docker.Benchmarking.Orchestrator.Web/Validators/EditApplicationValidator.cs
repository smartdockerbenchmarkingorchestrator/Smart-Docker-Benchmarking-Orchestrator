using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class EditApplicationValidator : AbstractValidator<EditApplicationViewModel>
    {
        public EditApplicationValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.BenchmarkingImage).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.ApplicationImage).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.ApplicationType).NotNull().NotEmpty();
            RuleFor(c => c.DelayToStartApplication).NotNull().GreaterThanOrEqualTo(60000);
        }
    }
}
