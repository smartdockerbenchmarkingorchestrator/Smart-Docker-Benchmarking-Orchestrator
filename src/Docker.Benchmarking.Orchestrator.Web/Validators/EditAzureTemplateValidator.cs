using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class EditAzureTemplateValidator : AbstractValidator<EditAzureTemplateViewModel>
    {
        public EditAzureTemplateValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.VMSize).NotNull().NotEmpty();
            RuleFor(c => c.vCPUs).NotNull().GreaterThan(0);
            RuleFor(c => c.Memory).NotNull().GreaterThan(0);
            RuleFor(c => c.DiskSize).NotNull().GreaterThan(0);
            RuleFor(c => c.Template).Must(d => d.IsValidJson());
            RuleFor(c => c.ParametersDefault).Must(d => d.IsValidJson());
            RuleFor(c => c.DeploymentType).NotNull().NotEmpty();
        }
    }
}
