using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class ApacheTestFileEditValidator : AbstractValidator<ApacheTestFileEditModel>
    {
        public ApacheTestFileEditValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.DateTimeCreated).NotNull().NotEmpty();
            RuleFor(c => c.TestUpload).NotNull().NotEmpty().Must(testupload => testupload.IsValidXml());
        }
    }
}
