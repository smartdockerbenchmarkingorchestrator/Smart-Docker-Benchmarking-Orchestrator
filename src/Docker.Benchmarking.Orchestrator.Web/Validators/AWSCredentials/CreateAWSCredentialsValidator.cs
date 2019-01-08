using FluentValidation;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class CreateAWSCredentialsValidator : AbstractValidator<Pages.AWSCredentials.CreateModel>
    {
        public CreateAWSCredentialsValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.AccessKeyId).NotNull().NotEmpty();
            RuleFor(c => c.SecretKey).NotNull().NotEmpty();
            RuleFor(c => c.RegionEndPoint).NotNull().NotEmpty();
        }
    }
}
