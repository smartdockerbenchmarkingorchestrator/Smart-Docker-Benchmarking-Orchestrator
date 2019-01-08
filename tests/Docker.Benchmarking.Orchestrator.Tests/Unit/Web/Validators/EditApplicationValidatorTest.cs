using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class EditApplicationValidatorTest
    {
        private readonly EditApplicationValidator _validator;

        public EditApplicationValidatorTest()
        {
            _validator = new EditApplicationValidator();
        }

        [Fact]
        public void TestValidModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]

        public void TestNotValidModel()
        {
            var validate = _validator.Validate(NotValidModel());

            Assert.False(validate.IsValid);
        }

        #region private

        public static EditApplicationViewModel ValidModel()
        {
            return new EditApplicationViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Test Application",
                ApacheTestFileId = Guid.NewGuid(),
                ApplicationImage = Guid.NewGuid(),
                ApplicationType = Orchestrator.Core.Enums.ApplicationType.ECommerce,
                BenchmarkingImage = Guid.NewGuid(),
                DelayToStartApplication = 60000,
                DateTimeCreated = DateTime.UtcNow
            };
        }

        public static EditApplicationViewModel NotValidModel()
        {
            return new EditApplicationViewModel
            {
                Id = Guid.Empty,
                Name = string.Empty,
                ApacheTestFileId = Guid.Empty,
                ApplicationImage = Guid.Empty,
                ApplicationType = Orchestrator.Core.Enums.ApplicationType.ECommerce,
                BenchmarkingImage = Guid.Empty,
                DelayToStartApplication = 30000
            };
        }

        #endregion
    }
}
