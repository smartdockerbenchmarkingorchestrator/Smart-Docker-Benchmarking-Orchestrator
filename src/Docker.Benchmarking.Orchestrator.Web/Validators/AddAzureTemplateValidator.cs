using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class AddAzureTemplateValidator : AbstractValidator<AddAzureTemplateViewModel>
    {
        public AddAzureTemplateValidator()
        {
            RuleFor(template => template.Name).NotEmpty();
            RuleFor(template => template.VMSize).NotEmpty();
            RuleFor(template => template.vCPUs).NotEmpty().GreaterThan(0);
            RuleFor(template => template.Memory).NotEmpty().GreaterThan(0);
            RuleFor(template => template.DiskSize).NotEmpty().GreaterThan(0);
            RuleFor(template => template.Template).Must(d => d.IsValidJson()).NotEmpty();
            RuleFor(template => template.ParametersDefault).Must(d => d.IsValidJson()).NotEmpty();
            RuleFor(template => template.DeploymentType).NotEmpty();
            RuleFor(c => c.VMSizeType).NotEmpty();
        }
    }
}
