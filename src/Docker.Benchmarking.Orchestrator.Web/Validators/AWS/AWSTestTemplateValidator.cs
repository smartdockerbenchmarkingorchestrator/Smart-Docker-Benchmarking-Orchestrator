using Docker.Benchmarking.Orchestrator.Web.Pages.AWS;
using FluentValidation;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AWS
{
    public class AWSTestTemplateValidator : AbstractValidator<TestTemplateModel>
    {
        public AWSTestTemplateValidator()
        {
            RuleFor(c => c.Template).Must(d => d.IsValidJson()).NotNull().NotEmpty();
            RuleFor(c => c.StackName).NotNull().NotEmpty();
            RuleFor(c => c.Credential).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.Parameters).NotNull().Must(c => c.Count > 0);
        }
    }
}
