using FluentValidation;

namespace Docker.Benchmarking.Orchestrator.Web.Validators.AzureCredentials
{
    public class CreateAzureCredentialsValidator : AbstractValidator<Pages.AzureCredentials.CreateModel>
    {
        public CreateAzureCredentialsValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.ClientId).NotNull().NotEmpty();
            RuleFor(c => c.SubscriptionId).NotNull().NotEmpty();
            RuleFor(c => c.Secret).NotNull().NotEmpty();
            RuleFor(c => c.TenantId).NotNull().NotEmpty();
        }
    }
}
