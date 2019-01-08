using FluentValidation;
using System;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AWS
{
    public class AWSCreateTemplateValidator : AbstractValidator<Pages.AWS.CreateModel>
    {
        public AWSCreateTemplateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.InstanceName).NotNull().NotEmpty();
            RuleFor(c => c.Template).Must(d => d.IsValidJson()).NotNull().NotEmpty();
            RuleFor(c => c.Credential).NotNull().NotEmpty().NotEqual(Guid.Empty);

            RuleFor(c => c.vCPUs).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Memory).NotEmpty().GreaterThan(0);
            RuleFor(c => c.DiskSize).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Parameters).NotNull().Must(c => c.Count >= 1);
            RuleFor(c => c.PricePerHour).NotEmpty();
            RuleFor(c => c.VMSizeType).NotEmpty();
        }
    }
}
