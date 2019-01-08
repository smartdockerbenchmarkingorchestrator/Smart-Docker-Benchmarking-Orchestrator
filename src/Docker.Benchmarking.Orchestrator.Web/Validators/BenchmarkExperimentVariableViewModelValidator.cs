using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class BenchmarkExperimentVariableViewModelValidator : AbstractValidator<BenchmarkExperimentVariableViewModel>
    {
        public BenchmarkExperimentVariableViewModelValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.Value).NotNull().NotEmpty();
        }
    }
}
