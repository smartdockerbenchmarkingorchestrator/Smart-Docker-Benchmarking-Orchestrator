using Docker.Benchmarking.Orchestrator.Web.ViewModels.AWS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AWS
{
    public class AWSParameterValidator : AbstractValidator<AWSParameterViewModel>
    {
        public AWSParameterValidator()
        {
            RuleFor(c => c.Key).NotNull().NotEmpty();
            RuleFor(c => c.Value).NotNull().NotEmpty();
        }
    }
}
