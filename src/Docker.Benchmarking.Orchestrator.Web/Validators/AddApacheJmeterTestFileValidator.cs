using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class AddApacheJmeterTestFileValidator : AbstractValidator<AddApacheJmeterTestFileViewModel>
    {
        public AddApacheJmeterTestFileValidator()
        {
            RuleFor(file => file.Name).NotNull();
            RuleFor(c => c.TestUpload).NotNull().NotEmpty().Must(testupload => testupload.IsValidXml());
        }
    }
}
