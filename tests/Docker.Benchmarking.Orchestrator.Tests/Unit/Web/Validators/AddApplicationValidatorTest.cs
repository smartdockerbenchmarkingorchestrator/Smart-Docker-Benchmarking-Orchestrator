using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddApplicationValidatorTest
    {
        private AddApplicationValidator validator;

        public AddApplicationValidatorTest()
        {
            validator = new AddApplicationValidator();
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            validator.ShouldHaveValidationErrorFor(app => app.Name, null as string);
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(app => app.Name, "Test Application");
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified_model()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, ValidModel());
        }


        [Fact]
        public void TestValidModel()
        {
            var validate = validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]

        public void TestNotValidModel()
        {
            var validate = validator.Validate(NotValidModel());

            Assert.False(validate.IsValid);
        }

        public static AddApplicationViewModel ValidModel()
        {
            return new AddApplicationViewModel
            {
                Name = "Test Application",
                ApacheTestFileId = Guid.NewGuid(),
                ApplicationImage = Guid.NewGuid(),
                ApplicationType = Orchestrator.Core.Enums.ApplicationType.ECommerce,
                BenchmarkingImage = Guid.NewGuid(),
                DelayToStartApplication = 60000
            };
        }

        public static AddApplicationViewModel NotValidModel()
        {
            return new AddApplicationViewModel
            {
                Name = string.Empty,
                ApacheTestFileId = Guid.Empty,
                ApplicationImage = Guid.Empty,
                ApplicationType = Orchestrator.Core.Enums.ApplicationType.ECommerce,
                BenchmarkingImage = Guid.Empty,
                DelayToStartApplication = 30000
            };
        }
    }
}
