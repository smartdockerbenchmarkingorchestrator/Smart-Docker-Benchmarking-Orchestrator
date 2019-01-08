using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class EditBenchmarkExperimentValidatorTest
    {
        private EditBenchmarkExperimentValidator _validator;

        public EditBenchmarkExperimentValidatorTest()
        {
            _validator = new EditBenchmarkExperimentValidator();
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
        #region private
        private EditBenchmarkExperimentViewModel ValidModel()
        {
            return new EditBenchmarkExperimentViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
                Name = "Standard_F1_VM",
                ApacheTestFileId = Guid.NewGuid(),
                Application = Guid.NewGuid(),
                BenchmarkHost = Guid.NewGuid(),
                BenchmarkTimeLength = 60000,
                ConcurrentUsers = 60,
                CaptureContainerMetrics = true,
                Host = Guid.NewGuid(),
                TypeOfTest = Orchestrator.Core.Enums.TestType.Load
            };
        }

        private EditBenchmarkExperimentViewModel InvalidModel()
        {
            return new EditBenchmarkExperimentViewModel
            {
                Id = Guid.Empty,
                Name = string.Empty,
                ApacheTestFileId = Guid.Empty,
                Application = Guid.Empty,
                BenchmarkHost = Guid.Empty,
                BenchmarkTimeLength = 30000,
                ConcurrentUsers = 60,
                CaptureContainerMetrics = true,
                Host = Guid.Empty,
                TypeOfTest = Orchestrator.Core.Enums.TestType.Load
            };
        }

        #endregion
    }
}
