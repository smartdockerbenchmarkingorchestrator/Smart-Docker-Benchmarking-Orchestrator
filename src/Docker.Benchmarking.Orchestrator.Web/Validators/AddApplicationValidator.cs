using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class AddApplicationValidator : AbstractValidator<AddApplicationViewModel>
    {
        public AddApplicationValidator()
        {
            RuleFor(file => file.Name).NotNull();
            RuleFor(file => file.BenchmarkingImage).NotNull().NotEqual(Guid.Empty);
            RuleFor(file => file.ApplicationImage).NotNull().NotEqual(Guid.Empty);
            RuleFor(file => file.DelayToStartApplication).NotNull().GreaterThanOrEqualTo(60000);
            RuleFor(file => file.ApplicationType).NotNull();
        }
    }
}
