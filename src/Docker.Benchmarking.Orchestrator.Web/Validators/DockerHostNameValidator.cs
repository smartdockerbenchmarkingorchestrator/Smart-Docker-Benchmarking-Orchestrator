using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    //https://www.jerriepelser.com/blog/remote-client-side-validation-with-fluentvalidation/
    public class DockerHostNameValidator : PropertyValidator
    {
        private readonly IRepository<DockerHost> _dockerHostRepo;
        public DockerHostNameValidator(IRepository<DockerHost> dockerHostRepo) : base("Hostname is already registered")
        {
            _dockerHostRepo = dockerHostRepo;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string hostName = context.PropertyValue as string;

            return _dockerHostRepo.FindBy(c => c.Name.ToLower() == hostName.ToLower()).Any();
        }
    }
}
