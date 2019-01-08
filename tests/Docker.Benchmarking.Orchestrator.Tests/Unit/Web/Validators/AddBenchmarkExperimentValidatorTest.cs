using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddBenchmarkExperimentValidatorTest
    {
        private readonly AddBenchmarkExperimentValidator _validator;
        public AddBenchmarkExperimentValidatorTest()
        {
            _validator = new AddBenchmarkExperimentValidator();
        }

        [Fact]
        public void TestValidViewModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]
        public void TestInValidViewModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }

        private AddBenchmarkExperimentViewModel ValidModel()
        {
            return new AddBenchmarkExperimentViewModel
            {
                Name = "Standard_F1_VM",
                ApacheTestFileId = Guid.NewGuid(),
                Application = Guid.NewGuid(),
                BenchmarkHost = Guid.NewGuid(),
                BenchmarkTimeLength = 60000,
                ConcurrentUsers = 60,
                CaptureContainerMetrics = true,
                Host = Guid.NewGuid(),
                TypeOfTest = Orchestrator.Core.Enums.TestType.Load,
                ApdexTSeconds = 1.5
            };
        }

        private AddBenchmarkExperimentViewModel InvalidModel()
        {
            return new AddBenchmarkExperimentViewModel
            {
                Name = string.Empty,
                ApacheTestFileId = Guid.Empty,
                Application = Guid.Empty,
                BenchmarkHost = Guid.Empty,
                BenchmarkTimeLength = 30000,
                ConcurrentUsers = 60,
                CaptureContainerMetrics = true,
                Host = Guid.Empty,
                TypeOfTest = Orchestrator.Core.Enums.TestType.Load,
                ApdexTSeconds = 20
            };
        }
    }
}
