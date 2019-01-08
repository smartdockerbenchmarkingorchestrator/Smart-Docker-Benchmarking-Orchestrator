using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class EditDockerHostValidator : AbstractValidator<EditDockerHostViewModel>
    {
        public EditDockerHostValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
            
            RuleFor(c => c.HostName).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.PortNumber).NotNull().NotEmpty();
            RuleFor(c => c.CloudProvider).NotNull().NotEmpty();

            RuleFor(c => c.Location).NotNull().NotEmpty();
            RuleFor(c => c.vCPU).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(c => c.Memory).NotNull().NotEmpty().GreaterThan(0);

            RuleFor(m => m.AzureLocation).NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);
            RuleFor(m => m.AzureTemplate).NotNull().NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);

            //RuleFor(m => m.AzureL).NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);

            RuleFor(c => c.UserName).NotNull().NotEmpty().When(c => c.UseHttpAuthentication);
            RuleFor(c => c.Password).NotNull().NotEmpty().When(c => c.UseHttpAuthentication);

            RuleFor(m => m.HostType).NotNull();
        }
    }
}
