using FluentValidation;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AWSCredentials
{
    public class EditAWSCredentialsValidator : AbstractValidator<Pages.AWSCredentials.EditModel>
    {
        public EditAWSCredentialsValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.AccessKeyId).NotNull().NotEmpty();
            RuleFor(c => c.SecretKey).NotNull().NotEmpty();
            RuleFor(c => c.RegionEndPoint).NotNull().NotEmpty();
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
        }
    }
}
