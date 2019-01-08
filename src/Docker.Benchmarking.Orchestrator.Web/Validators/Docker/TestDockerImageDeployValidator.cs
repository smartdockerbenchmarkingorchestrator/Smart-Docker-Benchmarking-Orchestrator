using Docker.Benchmarking.Orchestrator.Web.Pages.DockerHost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.Docker
{
    public class TestDockerImageDeployValidator : AbstractValidator<TestDeploy>
    {
        public TestDockerImageDeployValidator()
        {
            RuleFor(c => c.PortNumber).NotNull().NotEmpty();
            RuleFor(c => c.DockerHost).NotNull().NotEmpty();
            RuleFor(c => c.DockerImage).NotNull().NotEqual(Guid.Empty);

            RuleFor(c => c.UserName).NotNull().NotEmpty().When(c => !string.IsNullOrEmpty(c.Password));
            RuleFor(c => c.Password).NotNull().NotEmpty().When(c => !string.IsNullOrEmpty(c.UserName));
        }
    }
}
