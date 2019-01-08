
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class AddDockerHostValidator : AbstractValidator<AddDockerHostViewModel>
    {
        public AddDockerHostValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.PortNumber).NotNull().NotEmpty();
            RuleFor(c => c.CloudProvider).NotNull().NotEmpty();

            RuleFor(c => c.HostName).NotNull().NotEmpty();

            RuleFor(c => c.Location).NotNull().NotEmpty();
            RuleFor(c => c.vCPU).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(c => c.Memory).NotNull().NotEmpty().GreaterThan(0);

            RuleFor(m => m.AzureLocation).NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);
            RuleFor(m => m.AzureTemplate).NotNull().NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);
            RuleFor(m => m.AzureCredential).NotNull().NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.Azure);

            RuleFor(m => m.AWSCredential).NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.AWS);
            RuleFor(m => m.AWSTemplate).NotNull().NotEmpty().When(m => m.CloudProvider == Core.Enums.CloudProvider.AWS);
            RuleFor(m => m.HostType).NotNull();

            RuleFor(c => c.UserName).NotNull().NotEmpty().When(c => c.UseHttpAuthentication);
            RuleFor(c => c.Password).NotNull().NotEmpty().When(c => c.UseHttpAuthentication);
        }
    }
}
