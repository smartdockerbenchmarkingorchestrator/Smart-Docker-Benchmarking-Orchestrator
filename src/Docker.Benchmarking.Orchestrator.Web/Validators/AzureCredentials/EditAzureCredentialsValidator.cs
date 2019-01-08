using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AzureCredentials
{
    public class EditAzureCredentialsValidator : AbstractValidator<Pages.AzureCredentials.EditModel>
    {
        public EditAzureCredentialsValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.ClientId).NotNull().NotEmpty();
            RuleFor(c => c.SubscriptionId).NotNull().NotEmpty();
            RuleFor(c => c.Secret).NotNull().NotEmpty();
            RuleFor(c => c.TenantId).NotNull().NotEmpty();

            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
        }
    }
}
