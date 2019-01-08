using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class TestAzureResourcesValidator : AbstractValidator<TestAzureResourcesViewModel>
    {
        public TestAzureResourcesValidator()
        {
            RuleFor(c => c.ResourceName).NotNull().NotEmpty();
            RuleFor(c => c.AzureRegion).NotNull().NotEmpty();
            RuleFor(c => c.DeploymentJson).NotNull().NotEmpty();
            RuleFor(c => c.ParametersJson).NotNull().NotEmpty();
            RuleFor(c => c.CredentialsId).NotNull().NotEmpty();
        }
    }
}
