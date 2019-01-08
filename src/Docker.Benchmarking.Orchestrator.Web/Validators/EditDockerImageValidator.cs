using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class EditDockerImageValidator : AbstractValidator<EditDockerImageViewModel>
    {
        public EditDockerImageValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();

            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.ImageName).NotNull().NotEmpty();
            RuleFor(c => c.ImageTag).NotNull().NotEmpty();
            RuleFor(c => c.ImageType).NotNull().NotEmpty();

            RuleFor(c => c.PrivateRepositoryHost).NotNull().NotEmpty().When(c => c.PrivateRepository);
            RuleFor(c => c.PrivateRepositoryUsername).NotNull().NotEmpty().When(c => c.PrivateRepository);
            RuleFor(c => c.PrivateRepositoryPassword).NotNull().NotEmpty().When(c => c.PrivateRepository);

        }
    }
}
