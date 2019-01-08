using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Validators
{
    public class ManualBenchmarkResultsUploadValidator : AbstractValidator<ManualBenchmarkResultsUploadViewModel>
    {
        public ManualBenchmarkResultsUploadValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.File).NotNull().NotEmpty().Must(str => str.FileName.ToLower().EndsWith(".csv"));
        }
    }
}
